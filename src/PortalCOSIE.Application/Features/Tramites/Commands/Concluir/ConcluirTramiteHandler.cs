using PortalCOSIE.Application.Features.Tramites.Services;
using PortalCOSIE.Application.Notifications;
using PortalCOSIE.Application.Services;
using PortalCOSIE.Application.Services.Notificacion;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Entities.Tramites.CTCE;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.Tramites.Commands.Concluir
{
    public class ConcluirTramiteHandler : IRequestHandler<ConcluirTramiteCommand, Result<string>>
    {
        private readonly ITramiteRepository _tramiteRepo;
        private readonly ISecurityService _security;
        private readonly ICertificadoAdminService _certificadoAdmin;
        private readonly ProcesadorDocumentoFirmado _procesadorDocumento;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITramiteNotificationService _notificaciones;

        public ConcluirTramiteHandler(
            ITramiteRepository tramiteRepo,
            ISecurityService security,
            ICertificadoAdminService certificadoAdmin,
            ProcesadorDocumentoFirmado procesadorDocumento,
            IUnitOfWork unitOfWork,
            ITramiteNotificationService notificaciones)
        {
            _tramiteRepo = tramiteRepo;
            _security = security;
            _certificadoAdmin = certificadoAdmin;
            _procesadorDocumento = procesadorDocumento;
            _unitOfWork = unitOfWork;
            _notificaciones = notificaciones;
        }

        public async Task<Result<string>> Handle(ConcluirTramiteCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.TokenAcuse))
                return Result<string>.Failure("El token de preparación del acuse es obligatorio.");
            if (command.Acuse == null || command.Acuse.Contenido == null || command.Acuse.Contenido.Length == 0)
                return Result<string>.Failure("El acuse es obligatorio para concluir el trámite.");

            if (!await _security.TieneRolAsync(command.IdentityUserId, "Administrador"))
                return Result<string>.Failure("Solo un administrador puede concluir el trámite.");

            TramiteCTCE tramite = await _tramiteRepo.ObtenerTramiteCTCEPorIdParaRevision(command.TramiteId);
            if (tramite is null)
                return Result<string>.Failure("Trámite no encontrado.");
            if (tramite.EstadoTramiteId != EstadoTramite.EsperandoAcuse.Id)
                return Result<string>.Failure("El trámite no se encuentra en un estado válido para ser concluido.");
            if (!tramite.TieneTokenAcusePendiente(command.TokenAcuse))
                return Result<string>.Failure("El acuse no fue preparado correctamente o el token expiró.");

            var certificado = await _certificadoAdmin.ObtenerCertificadoAcuseParaFirmaAsync();
            if (certificado is null)
                return Result<string>.Failure("No hay un certificado de acuse registrado. Genera uno en Mi cuenta.");

            var estado = TramiteEstadoSnapshot.Desde(tramite);

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var documento = await _procesadorDocumento.CrearAcusePadesAsync(
                    command.Acuse,
                    tramite.Id,
                    certificado,
                    command.TokenAcuse.Trim());

                tramite.AgregarDocumento(documento);
                tramite.ConsumirTokenAcusePendiente();
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