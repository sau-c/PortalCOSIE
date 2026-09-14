using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Application;
using PortalCOSIE.Application.Features.Certificados.DTO;
using PortalCOSIE.Application.Services;
using PortalCOSIE.Application.Services.Crypto;
using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Enums;
using PortalCOSIE.Domain.Interfaces;
using PortalCOSIE.Infrastructure.Persistence;

namespace PortalCOSIE.Infrastructure.Services;

public class CertificadoAlumnoService : ICertificadoAlumnoService
{
    private readonly AppDbContext _context;
    private readonly IUsuarioRepository _usuarioRepo;
    private readonly ICaEmisionService _caEmision;
    private readonly IUnitOfWork _unitOfWork;

    public CertificadoAlumnoService(
        AppDbContext context,
        IUsuarioRepository usuarioRepo,
        ICaEmisionService caEmision,
        IUnitOfWork unitOfWork)
    {
        _context = context;
        _usuarioRepo = usuarioRepo;
        _caEmision = caEmision;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> EmitirDesdeCsrAsync(string identityUserId, byte[] csrDer)
    {
        if (csrDer.Length == 0)
            return Result<string>.Failure("La solicitud CSR es obligatoria.");

        if (!await _caEmision.PuedeEmitirAsync())
            return Result<string>.Failure("La autoridad certificadora no está lista para emitir certificados.");

        var alumno = await _usuarioRepo.BuscarAlumnoConCarrera(identityUserId);
        if (alumno == null)
            return Result<string>.Failure("Alumno no encontrado.");

        var sujeto = ConstruirSujeto(alumno);
        var emitir = await _caEmision.EmitirDesdeCsrAsync(csrDer, TipoCertificado.Alumno, sujeto);
        if (!emitir.Succeeded)
            return Result<string>.Failure(emitir.Errors.ToArray());

        var certificadoNuevo = emitir.Value;

        var asignadoAOtro = await _context.Set<Usuario>()
            .AnyAsync(u => u.CertificadoId == certificadoNuevo.Id && u.IdentityUserId != identityUserId);
        if (asignadoAOtro)
            return Result<string>.Failure("El certificado ya está asignado a otro usuario.");

        if (!string.IsNullOrEmpty(alumno.CertificadoId))
        {
            var remover = await RemoverCertificadoAsync(identityUserId);
            if (!remover.Succeeded)
                return remover;
            alumno = await _usuarioRepo.BuscarAlumnoConCarrera(identityUserId);
        }

        var existente = await _context.Set<Certificado>().FindAsync(certificadoNuevo.Id);
        Certificado certificadoFinal;
        if (existente == null)
        {
            await _context.Set<Certificado>().AddAsync(certificadoNuevo);
            certificadoFinal = certificadoNuevo;
        }
        else
        {
            if (existente.IsDeleted)
                existente.Restore();
            certificadoFinal = existente;
        }

        alumno!.AsignarCertificado(certificadoFinal);
        await _unitOfWork.SaveChangesAsync();

        return Result<string>.Success("Certificado emitido y registrado. Guarda tu archivo .key en un lugar seguro.");
    }

    public async Task<CertificadoResumenDTO?> ObtenerResumenAsync(string identityUserId)
    {
        var alumno = await _usuarioRepo.BuscarUsuarioConCertificado(identityUserId);
        if (alumno?.Certificado == null || alumno.Certificado.Tipo != TipoCertificado.Alumno)
            return null;

        var cert = alumno.Certificado;
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

    public async Task<Result<string>> RemoverCertificadoAsync(string identityUserId)
    {
        var alumno = await _usuarioRepo.BuscarUsuarioConCertificado(identityUserId);
        if (alumno == null)
            return Result<string>.Failure("Alumno no encontrado.");

        if (string.IsNullOrEmpty(alumno.CertificadoId) || alumno.Certificado == null)
            return Result<string>.Success("El alumno no tenía certificado registrado.");

        var certificadoId = alumno.CertificadoId;
        alumno.RemoverCertificado();

        var tieneFirmas = await _context.Set<FirmaElectronica>()
            .AnyAsync(f => f.CertificadoId == certificadoId);

        if (!tieneFirmas)
        {
            var certificado = await _context.Set<Certificado>().FindAsync(certificadoId);
            if (certificado != null)
                _context.Set<Certificado>().Remove(certificado);
        }
        else
        {
            var certificado = await _context.Set<Certificado>().FindAsync(certificadoId);
            if (certificado != null && !certificado.IsDeleted)
                certificado.SoftDelete();
        }

        await _unitOfWork.SaveChangesAsync();
        return Result<string>.Success("Certificado eliminado del sistema.");
    }

    private static string ConstruirSujeto(Alumno alumno)
    {
        var cn = $"{alumno.Nombre} {alumno.ApellidoPaterno} {alumno.ApellidoMaterno}".Trim();
        return $"CN={cn},OU=Alumno,serialNumber={alumno.NumeroBoleta},O=PortalCOSIE";
    }
}