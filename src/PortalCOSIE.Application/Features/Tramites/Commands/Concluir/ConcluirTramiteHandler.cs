using PortalCOSIE.Application.Features.Tramites.Services;
using PortalCOSIE.Application.Notifications;
using PortalCOSIE.Application.Services.Notificacion;
using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Entities.Tramites.CTCE;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.Tramites.Commands.Concluir
{
    public class ConcluirTramiteHandler : IRequestHandler<ConcluirTramiteCommand, Result<string>>
    {
        private readonly ITramiteRepository _tramiteRepo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly ProcesadorDocumentoFirmado _procesadorDocumento;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITramiteNotificationService _notificaciones;

        public ConcluirTramiteHandler(
            ITramiteRepository tramiteRepo,
            IUsuarioRepository usuarioRepo,
            ProcesadorDocumentoFirmado procesadorDocumento,
            IUnitOfWork unitOfWork,
            ITramiteNotificationService notificaciones)
        {
            _tramiteRepo = tramiteRepo;
            _usuarioRepo = usuarioRepo;
            _procesadorDocumento = procesadorDocumento;
            _unitOfWork = unitOfWork;
            _notificaciones = notificaciones;
        }

        public async Task<Result<string>> Handle(ConcluirTramiteCommand command)
        {
            if (command.Acuse == null || command.Acuse.Contenido == null || command.Acuse.Contenido.Length == 0)
                return Result<string>.Failure("El acuse es obligatorio para concluir el trámite.");

            TramiteCTCE tramite = await _tramiteRepo.ObtenerTramiteCTCEPorIdParaRevision(command.TramiteId);
            if (tramite is null)
                return Result<string>.Failure("Trámite no encontrado.");
            if (tramite.EstadoTramiteId != EstadoTramite.EsperandoAcuse.Id)
                return Result<string>.Failure("El trámite no se encuentra en un estado válido para ser concluido.");

            var usuario = await _usuarioRepo.BuscarUsuarioConCertificado(command.IdentityUserId);
            if (usuario is null)
                return Result<string>.Failure("Usuario no encontrado.");
            if (!tramite.PuedeSerAtendidoPor(usuario.Id))
                return Result<string>.Failure("No tienes permisos para atender este trámite.");
            if (usuario.Certificado == null)
                return Result<string>.Failure("No tienes un certificado registrado para firmar documentos.");

            var estado = TramiteEstadoSnapshot.Desde(tramite);

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var documento = await _procesadorDocumento.CrearAsync(
                    command.Acuse,
                    TipoDocumento.DictamenCTCE,
                    tramite.Id,
                    EstadoDocumento.Validado.Id,
                    usuario.Certificado);

                tramite.AgregarDocumento(documento);
                tramite.VerificarEstadoTramite();

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                await estado.NotificarSiCambioAsync(_notificaciones, tramite);
                return Result<string>.Success("Trámite concluido exitosamente.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return Result<string>.Failure(ex.Message);
            }
        }
    }
}