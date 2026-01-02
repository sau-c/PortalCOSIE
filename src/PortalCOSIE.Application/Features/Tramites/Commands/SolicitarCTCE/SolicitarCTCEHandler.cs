using PortalCOSIE.Application.Features.Tramites.DTO;
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
        private readonly IUnitOfWork _unitOfWork;

        public SolicitarCTCEHandler(
            IUsuarioRepository usuarioRepo,
            IBaseRepository<PeriodoConfig, int> periodoRepo,
            ITramiteRepository tramiteRepo,
            IUnitOfWork unitOfWork
            )
        {
            _usuarioRepo = usuarioRepo;
            _periodoRepo = periodoRepo;
            _tramiteRepo = tramiteRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(SolicitarCTCECommand command)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var alumno = await _usuarioRepo.BuscarUsuario(command.IdentityUserId);
                var periodoConfig = await _periodoRepo.GetByIdAsync(1);
                string periodo = $"{periodoConfig.AnioActual}/{periodoConfig.PeriodoActual}";
                var unidadesReprobadasEntities = new List<UnidadReprobada>();

                foreach (var itemDto in command.solicitud.UnidadesReprobadas)
                {
                    // Validación opcional: verificar si pertenece a la carrera (Nota: esto hace N queries a la BD)
                    var nuevaUnidadEntity = new UnidadReprobada(
                        itemDto.UnidadId,
                        itemDto.PeriodoCursado,
                        itemDto.PeriodoRecursado
                    );

                    unidadesReprobadasEntities.Add(nuevaUnidadEntity);
                }

                // 2. Crear el Trámite
                var tramite = new TramiteCTCE(
                    alumno.Id,
                    TipoTramite.DictamenInterno.Id,
                    periodo,
                    command.solicitud.Peticion,
                    command.solicitud.TieneDictamenesAnteriores,
                    unidadesReprobadasEntities,
                    ToDocumeto(command.solicitud.Identificacion, TipoDocumento.Identificacion),
                    ToDocumeto(command.solicitud.BoletaGlobal, TipoDocumento.BoletaGlobal),
                    ToDocumeto(command.solicitud.CartaExposicionMotivos, TipoDocumento.CartaExposicionMotivos),
                    ToDocumeto(command.solicitud.Probatorios, TipoDocumento.Probatorios)
                );

                await _tramiteRepo.AddAsync(tramite);
                await _unitOfWork.SaveChangesAsync(); // Ahora tramite.Id tiene valor
                await _unitOfWork.CommitTransactionAsync();
                return Result<string>.Success("Solicitud enviada con éxito.");
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        private Documento ToDocumeto(ArchivoDescargaDTO DocumentoDto, TipoDocumento tipo)
        {
            return new Documento(
                    DocumentoDto.Nombre,
                    0, // Se pone 0, EF Core asignará el ID real al guardar
                    tipo.Id,
                    EstadoDocumento.EnRevision.Id,
                    DocumentoDto.Contenido
                );
        }
    }
}
