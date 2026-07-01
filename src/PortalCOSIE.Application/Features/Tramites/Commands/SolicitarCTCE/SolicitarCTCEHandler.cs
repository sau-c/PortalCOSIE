using PortalCOSIE.Application.Features.Tramites.Services;
using PortalCOSIE.Application.Notifications;
using PortalCOSIE.Application.Services.Notificacion;
using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Domain.Entities.PeriodosConfig;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Entities.Tramites.CTCE;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.Tramites.Commands.SolicitarCTCE
{
    public class SolicitarCTCEHandler : IRequestHandler<SolicitarCTCECommand, Result<string>>
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IBaseRepository<PeriodoConfig, int> _periodoRepo;
        private readonly ITramiteRepository _tramiteRepo;
        private readonly ProcesadorDocumentoFirmado _procesadorDocumento;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITramiteNotificationService _notificaciones;

        public SolicitarCTCEHandler(
            IUsuarioRepository usuarioRepo,
            IBaseRepository<PeriodoConfig, int> periodoRepo,
            ITramiteRepository tramiteRepo,
            ProcesadorDocumentoFirmado procesadorDocumento,
            IUnitOfWork unitOfWork,
            ITramiteNotificationService notificaciones)
        {
            _usuarioRepo = usuarioRepo;
            _periodoRepo = periodoRepo;
            _tramiteRepo = tramiteRepo;
            _procesadorDocumento = procesadorDocumento;
            _unitOfWork = unitOfWork;
            _notificaciones = notificaciones;
        }

        public async Task<Result<string>> Handle(SolicitarCTCECommand command)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                if (command.BoletaGlobal == null || command.Identificacion == null
                    || command.CartaExposicionMotivos == null || command.Probatorios == null)
                    return Result<string>.Failure("Todos los documentos son obligatorios para solicitar.");

                var alumno = await _usuarioRepo.BuscarUsuarioConCertificado(command.IdentityUserId);
                if (alumno?.Certificado == null)
                    return Result<string>.Failure("No tienes un certificado registrado para firmar documentos.");

                var periodoConfig = await _periodoRepo.GetByIdAsync(1);
                string periodo = $"{periodoConfig.AnioActual}/{periodoConfig.PeriodoActual}";

                var unidadesReprobadasEntities = command.UnidadesReprobadas
                    .Select(u => new UnidadReprobada(u.UnidadAprendizajeId, u.PeriodoCurso, u.PeriodoRecurse))
                    .ToList();

                var certificado = alumno.Certificado;
                var tramite = new TramiteCTCE(
                    alumno.Id,
                    TipoTramite.DictamenInterno.Id,
                    periodo,
                    command.Peticion,
                    command.TieneDictamenesAnteriores,
                    unidadesReprobadasEntities,
                    await _procesadorDocumento.CrearAsync(command.Identificacion, TipoDocumento.Identificacion, 0, EstadoDocumento.EnRevision.Id, certificado),
                    await _procesadorDocumento.CrearAsync(command.BoletaGlobal, TipoDocumento.BoletaGlobal, 0, EstadoDocumento.EnRevision.Id, certificado),
                    await _procesadorDocumento.CrearAsync(command.CartaExposicionMotivos, TipoDocumento.CartaExposicionMotivos, 0, EstadoDocumento.EnRevision.Id, certificado),
                    await _procesadorDocumento.CrearAsync(command.Probatorios, TipoDocumento.Probatorios, 0, EstadoDocumento.EnRevision.Id, certificado)
                );

                await _tramiteRepo.AddAsync(tramite);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                await TramiteEstadoSnapshot.Inicial().NotificarSiCambioAsync(_notificaciones, tramite);
                return Result<string>.Success("Solicitud enviada con éxito.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return Result<string>.Failure(ex.Message);
            }
        }
    }
}