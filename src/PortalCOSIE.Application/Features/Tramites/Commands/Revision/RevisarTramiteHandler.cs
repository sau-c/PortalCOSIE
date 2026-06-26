using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.Tramites.Commands.Revision
{
    public class RevisarTramiteHandler : IRequestHandler<RevisarTramiteCommand, Result<string>>
    {
        private readonly ITramiteRepository _tramiteRepo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IUnitOfWork _unitOfWork;
        public RevisarTramiteHandler(
            ITramiteRepository tramiteRepo,
            IUsuarioRepository usuarioRepo,
            IUnitOfWork unitOfWork)
        {
            _tramiteRepo = tramiteRepo;
            _usuarioRepo = usuarioRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(RevisarTramiteCommand command)
        {
            var tramite = await _tramiteRepo.ObtenerTramiteCTCEPorIdParaRevision(command.TramiteId);

            if (tramite == null)
                return Result<string>.Failure("Trámite no encontrado.");
            if (tramite.EstadoTramiteId != EstadoTramite.EnRevision.Id)
                return Result<string>.Failure("El estado actual del trámite no permite revisión.");

            var usuario = await _usuarioRepo.BuscarUsuario(command.IdentityUserId);
            if (usuario is null)
                return Result<string>.Failure("Usuario no encontrado.");
            if (!tramite.PuedeSerAtendidoPor(usuario.Id))
                return Result<string>.Failure("No tienes permisos para atender este trámite.");

            foreach (var docDto in command.Documentos)
            {
                if (docDto.EstadoDocumentoId == 0) continue;
                // 1. Buscamos el documento dentro del Agregado
                // Esto asegura que solo modificamos documentos que pertenecen a ESTE trámite.
                var documento = tramite.Documentos.FirstOrDefault(d => d.Id == docDto.Id);
                if (documento != null)
                {
                    // 2. Delegamos la lógica de negocio a la entidad
                    documento.CambiarEstado(Enumeration.FromValue<EstadoDocumento>(docDto.EstadoDocumentoId));
                    documento.AgregarObservaciones(docDto.Observaciones);
                }
            }

            // 3. Actualizamos observaciones generales del trámite
            tramite.AgregarObservaciones(command.Observaciones);

            // 4. Verificar si el trámite cambia de estado
            // Ejemplo: Si todos los documentos están "Validados", el trámite avanza automáticamente.
            tramite.VerificarEstadoTramite();

            await _unitOfWork.SaveChangesAsync();
            return Result<string>.Success("Revisión guardada correctamente.");
        }
    }
}
