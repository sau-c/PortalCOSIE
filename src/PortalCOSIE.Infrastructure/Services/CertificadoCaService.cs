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

public class CertificadoCaService : ICertificadoCaService
{
    private readonly AppDbContext _context;
    private readonly ICertificadoParserService _parser;
    private readonly ICaEmisionService _caEmision;
    private readonly IUnitOfWork _unitOfWork;

    public CertificadoCaService(
        AppDbContext context,
        ICertificadoParserService parser,
        ICaEmisionService caEmision,
        IUnitOfWork unitOfWork)
    {
        _context = context;
        _parser = parser;
        _caEmision = caEmision;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> RegistrarCaAsync(Stream cerStream)
    {
        var parse = _parser.ParseCer(cerStream, TipoCertificado.Ca);
        if (!parse.Succeeded)
            return Result<string>.Failure(parse.Errors.ToArray());

        var certificado = parse.Value;
        var anteriores = await _context.Set<Certificado>()
            .Where(c => c.Tipo == TipoCertificado.Ca && !c.IsDeleted)
            .ToListAsync();

        foreach (var anterior in anteriores)
        {
            if (anterior.Id != certificado.Id)
                anterior.SoftDelete();
        }

        var existente = await _context.Set<Certificado>().FindAsync(certificado.Id);
        if (existente == null)
            await _context.Set<Certificado>().AddAsync(certificado);
        else if (existente.IsDeleted)
            existente.Restore();

        await _unitOfWork.SaveChangesAsync();
        return Result<string>.Success("Certificado de la CA registrado correctamente.");
    }

    public async Task<CertificadoResumenDTO?> ObtenerCaAsync()
    {
        var ca = await _context.Set<Certificado>()
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Tipo == TipoCertificado.Ca && !c.IsDeleted);

        if (ca == null)
            return null;

        var ahora = DateTime.UtcNow;
        var puedeEmitir = false;
        try
        {
            puedeEmitir = await _caEmision.PuedeEmitirAsync();
        }
        catch (Exception)
        {
            puedeEmitir = false;
        }

        return new CertificadoResumenDTO(
            ca.Id,
            ca.Sujeto,
            ca.NumeroSerie,
            ca.VigenteDesde,
            ca.VigenteHasta,
            ca.EstaVigenteEn(ahora),
            puedeEmitir);
    }

    public async Task<byte[]?> ObtenerCaCertificadoDerAsync()
    {
        var ca = await _context.Set<Certificado>()
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Tipo == TipoCertificado.Ca && !c.IsDeleted);

        return ca?.CertificadoDer;
    }
}