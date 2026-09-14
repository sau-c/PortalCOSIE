using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Application;
using PortalCOSIE.Application.Features.Certificados.DTO;
using PortalCOSIE.Application.Services;
using PortalCOSIE.Application.Services.Crypto;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Enums;
using PortalCOSIE.Domain.Interfaces;
using PortalCOSIE.Infrastructure.Persistence;

namespace PortalCOSIE.Infrastructure.Services;

public class CertificadoAdminService : ICertificadoAdminService
{
    private readonly AppDbContext _context;
    private readonly ICaEmisionService _caEmision;
    private readonly ISecurityService _security;
    private readonly IUnitOfWork _unitOfWork;

    public CertificadoAdminService(
        AppDbContext context,
        ICaEmisionService caEmision,
        ISecurityService security,
        IUnitOfWork unitOfWork)
    {
        _context = context;
        _caEmision = caEmision;
        _security = security;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> EmitirAcuseDesdeCsrAsync(string identityUserId, byte[] csrDer)
    {
        if (csrDer.Length == 0)
            return Result<string>.Failure("La solicitud CSR es obligatoria.");

        if (!await _security.TieneRolAsync(identityUserId, "Administrador"))
            return Result<string>.Failure("Solo un administrador puede emitir el certificado de acuse.");

        if (!await _caEmision.PuedeEmitirAsync())
            return Result<string>.Failure("La autoridad certificadora no está lista para emitir certificados.");

        var sujeto = "CN=PortalCOSIE - Firma de acuse,OU=Gestión escolar,O=UPIITA";
        var emitir = await _caEmision.EmitirDesdeCsrAsync(csrDer, TipoCertificado.AdminAcuse, sujeto);
        if (!emitir.Succeeded)
            return Result<string>.Failure(emitir.Errors.ToArray());

        var certificadoNuevo = emitir.Value;
        var anteriores = await _context.Set<Certificado>()
            .Where(c => c.Tipo == TipoCertificado.AdminAcuse && !c.IsDeleted)
            .ToListAsync();

        foreach (var anterior in anteriores)
        {
            if (anterior.Id != certificadoNuevo.Id)
                anterior.SoftDelete();
        }

        var existente = await _context.Set<Certificado>().FindAsync(certificadoNuevo.Id);
        if (existente == null)
            await _context.Set<Certificado>().AddAsync(certificadoNuevo);
        else if (existente.IsDeleted)
            existente.Restore();

        await _unitOfWork.SaveChangesAsync();
        return Result<string>.Success("Certificado de acuse emitido. Guarda tu archivo .key en un lugar seguro.");
    }

    public async Task<CertificadoResumenDTO?> ObtenerCertificadoAcuseAsync()
    {
        var cert = await ObtenerCertificadoAcuseParaFirmaAsync();
        if (cert == null)
            return null;

        var ahora = DateTime.UtcNow;
        return new CertificadoResumenDTO(
            cert.Id,
            cert.Sujeto,
            cert.NumeroSerie,
            cert.VigenteDesde,
            cert.VigenteHasta,
            cert.EstaVigenteEn(ahora),
            false);
    }

    public Task<Certificado?> ObtenerCertificadoAcuseParaFirmaAsync()
        => _context.Set<Certificado>()
            .FirstOrDefaultAsync(c => c.Tipo == TipoCertificado.AdminAcuse && !c.IsDeleted);
}