using PortalCOSIE.Application.Features.Tramites.Commands.Corregir;
using PortalCOSIE.Application.Features.Tramites.DTO;
using PortalCOSIE.Application.Features.Tramites.Services;
using PortalCOSIE.Application.Notifications;
using PortalCOSIE.Application.Services.Notificacion;
using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Entities.Tramites.CTCE;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.Tramites.Commands.CorregirCTCE
{
    public class CorregirTramiteCTCEHandler : IRequestHandler<CorregirTramiteCommand, Result<string>>
    {
        private readonly ITramiteRepository _tramiteRepo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly ProcesadorDocumentoFirmado _procesadorDocumento;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITramiteNotificationService _notificaciones;

        public CorregirTramiteCTCEHandler(
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

        public async Task<Result<string>> Handle(CorregirTramiteCommand command)
        {
            TramiteCTCE tramite = await _tramiteRepo.ObtenerTramiteCTCEPorIdParaRevision(command.TramiteId);
            if (tramite == null)
                return Result<string>.Failure("El trámite especificado no existe.");
            if (tramite.EstadoTramiteId != EstadoTramite.DocumentosPendientes.Id)
                return Result<string>.Failure("El estado actual del trámite no permite corrección.");

            var usuario = await _usuarioRepo.BuscarUsuarioConCertificado(command.IdentityUserId);
            if (usuario == null || !tramite.PerteneceAAlumno(usuario.Id))
                return Result<string>.Failure("No tienes permisos para atender este trámite.");
            if (usuario.Certificado == null)
                return Result<string>.Failure("No tienes un certificado registrado para firmar documentos.");

            if (tramite.Documentos.Any(d =>
                (d.EstadoDocumentoId == EstadoDocumento.ConErrores.Id ||
                 d.EstadoDocumentoId == EstadoDocumento.Incorrecto.Id) &&
                !TieneDocumentoCorregido(command, d.TipoDocumentoId)))
                return Result<string>.Failure("Debes proporcionar los archivos para todos los documentos que requieren corrección.");

            var estado = TramiteEstadoSnapshot.Desde(tramite);

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                await ProcesarReemplazoAsync(tramite, command.Identificacion, TipoDocumento.Identificacion, usuario.Certificado);
                await ProcesarReemplazoAsync(tramite, command.BoletaGlobal, TipoDocumento.BoletaGlobal, usuario.Certificado);
                await ProcesarReemplazoAsync(tramite, command.CartaExposicionMotivos, TipoDocumento.CartaExposicionMotivos, usuario.Certificado);
                await ProcesarReemplazoAsync(tramite, command.Probatorios, TipoDocumento.Probatorios, usuario.Certificado);

                tramite.VerificarEstadoTramite();

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                await estado.NotificarSiCambioAsync(_notificaciones, tramite);

                return Result<string>.Success("La corrección se ha enviado con éxito.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return Result<string>.Failure(ex.Message);
            }
        }

        private async Task ProcesarReemplazoAsync(
            TramiteCTCE tramite,
            DocumentoFirmadoDTO? documento,
            TipoDocumento tipo,
            Certificado certificado)
        {
            if (documento == null || documento.Contenido == null || documento.Contenido.Length == 0)
                return;

            var documentoExistente = tramite.Documentos.FirstOrDefault(d => d.TipoDocumentoId == tipo.Id)
                ?? throw new InvalidOperationException($"No se encontró el documento {tipo.Nombre} en el trámite.");

            await _procesadorDocumento.ReemplazarAsync(documentoExistente, documento, tipo, certificado);
        }

        private static bool TieneDocumentoCorregido(CorregirTramiteCommand command, int tipoDocumentoId)
        {
            DocumentoFirmadoDTO? documento = tipoDocumentoId switch
            {
                var id when id == TipoDocumento.Identificacion.Id => command.Identificacion,
                var id when id == TipoDocumento.BoletaGlobal.Id => command.BoletaGlobal,
                var id when id == TipoDocumento.CartaExposicionMotivos.Id => command.CartaExposicionMotivos,
                var id when id == TipoDocumento.Probatorios.Id => command.Probatorios,
                _ => null
            };

            return documento?.Contenido != null && documento.Contenido.Length > 0;
        }
    }
}