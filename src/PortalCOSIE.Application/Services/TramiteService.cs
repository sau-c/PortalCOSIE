using PortalCOSIE.Application.DTO.Tramites;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application
{
    public class TramiteService : ITramiteService
    {
        private readonly ITramiteRepository _tramiteRepo;
        private readonly IDocumentoRepository _docRepo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IUnitOfWork _unitOfWork;

        public TramiteService(
            ITramiteRepository tramiteRepo,
            IDocumentoRepository docRepo,
            IUsuarioRepository usuarioRepo,
            IUnitOfWork unitOfWork)
        {
            _tramiteRepo = tramiteRepo;
            _docRepo = docRepo;
            _usuarioRepo = usuarioRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Tramite>> ListarTodos()
        {
            return await _tramiteRepo.ListarConDatosCompletos();
        }
        public async Task SolicitarCTCE(SolicitudCtceDTO dto, string userId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var alumno = await _usuarioRepo.BuscarPorIdentityId(userId);

                var unidadesReprobadasEntities = new List<UnidadReprobada>();

                foreach (var itemDto in dto.UnidadesReprobadas)
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
                var tramite = new DetalleCTCE(
                    alumno.Id,
                    TipoTramite.DictamenInterno.Id,
                    dto.Situacion,
                    dto.TieneDictamenesAnteriores,
                    unidadesReprobadasEntities
                );

                await _tramiteRepo.AddAsync(tramite);
                await _unitOfWork.SaveChangesAsync(); // Ahora tramite.Id tiene valor

                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        public async Task<Tramite?> BuscarPorId(int id)
        {
            return await _tramiteRepo.GetByIdAsync(id);
        }
    }
}