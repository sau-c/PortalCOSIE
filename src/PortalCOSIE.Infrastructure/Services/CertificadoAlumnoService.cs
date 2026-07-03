using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Application;
using PortalCOSIE.Application.Services;
using PortalCOSIE.Application.Services.Crypto;
using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Interfaces;
using PortalCOSIE.Infrastructure.Persistence;

namespace PortalCOSIE.Infrastructure.Services
{
    public class CertificadoAlumnoService : ICertificadoAlumnoService
    {
        private readonly AppDbContext _context;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly ICertificadoParserService _certificadoParser;
        private readonly IUnitOfWork _unitOfWork;

        public CertificadoAlumnoService(
            AppDbContext context,
            IUsuarioRepository usuarioRepo,
            ICertificadoParserService certificadoParser,
            IUnitOfWork unitOfWork)
        {
            _context = context;
            _usuarioRepo = usuarioRepo;
            _certificadoParser = certificadoParser;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> AsignarCertificadoAsync(string identityUserId, Stream certificadoCer)
        {
            var parse = _certificadoParser.ParseCer(certificadoCer);
            if (!parse.Succeeded)
                return Result<string>.Failure(parse.Errors.ToArray());

            var certificadoNuevo = parse.Value;
            var alumno = await _usuarioRepo.BuscarUsuarioConCertificado(identityUserId);
            if (alumno == null)
                return Result<string>.Failure("Alumno no encontrado.");

            var asignadoAOtro = await _context.Set<Usuario>()
                .AnyAsync(u => u.CertificadoId == certificadoNuevo.Id && u.IdentityUserId != identityUserId);
            if (asignadoAOtro)
                return Result<string>.Failure("El certificado ya está asignado a otro usuario.");

            var certificadoExistente = await _context.Set<Certificado>()
                .FirstOrDefaultAsync(c => c.Id == certificadoNuevo.Id);

            Certificado certificadoFinal;
            if (certificadoExistente == null)
            {
                await _context.Set<Certificado>().AddAsync(certificadoNuevo);
                certificadoFinal = certificadoNuevo;
            }
            else
            {
                if (certificadoExistente.IsDeleted)
                    certificadoExistente.Restore();
                certificadoFinal = certificadoExistente;
            }

            alumno.AsignarCertificado(certificadoFinal);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Certificado registrado correctamente.");
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
    }
}