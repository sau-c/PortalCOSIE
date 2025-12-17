using PortalCOSIE.Application.DTO.Tramites;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities.Calendario;
using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application
{
    public class TramiteService : ITramiteService
    {
        private readonly ITramiteRepository _tramiteRepo;
        private readonly IBaseRepository<PeriodoConfig, int> _periodoRepo;
        private readonly IDocumentoQueryService _documentoService;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IUnitOfWork _unitOfWork;

        public TramiteService(
            ITramiteRepository tramiteRepo,
            IBaseRepository<PeriodoConfig, int> periodoRepo,
            IDocumentoQueryService documentoService,
            IUsuarioRepository usuarioRepo,
            IUnitOfWork unitOfWork)
        {
            _tramiteRepo = tramiteRepo;
            _documentoService = documentoService;
            _periodoRepo = periodoRepo;
            _usuarioRepo = usuarioRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Tramite>> ListarTodos(string rol, string identityUserId)
        {
            var usuario = await _usuarioRepo.BuscarUsuario(identityUserId);
            // 1. Definir parámetros para el repositorio
            int? filtroAlumno = null;
            int? filtroPersonal = null;

            // 2. Aplicar lógica según el rol
            switch (rol)
            {
                case "Alumno":
                    filtroAlumno = usuario.Id;
                    break;

                case "Personal":
                    filtroPersonal = usuario.Id;
                    break;

                case "Administrador":
                    // Admin no aplica filtros, ve todo (se quedan en null)
                    break;
            }

            // 3. Llamar al repositorio con los filtros calculados
            return await _tramiteRepo.ListarConDatosCompletos(filtroAlumno, filtroPersonal);
        }
        public async Task SolicitarCTCE(SolicitudCtceDTO dto, string userId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var alumno = await _usuarioRepo.BuscarUsuario(userId);
                var periodoConfig = await _periodoRepo.GetByIdAsync(1);
                string periodo = $"{periodoConfig.AnioActual}/{periodoConfig.PeriodoActual}";
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
                var tramite = new TramiteCTCE(
                    alumno.Id,
                    TipoTramite.DictamenInterno.Id,
                    periodo,
                    dto.Peticion,
                    dto.TieneDictamenesAnteriores,
                    unidadesReprobadasEntities,
                    ToDocumeto(dto.Identificacion, TipoDocumento.Identificacion),
                    ToDocumeto(dto.BoletaGlobal, TipoDocumento.BoletaGlobal),
                    ToDocumeto(dto.CartaExposicionMotivos, TipoDocumento.CartaExposicionMotivos),
                    ToDocumeto(dto.Probatorios, TipoDocumento.Probatorios)
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
        public async Task<TramiteCTCE?> BuscarTramiteCTCEPorId(int tramiteId, string identityUserId)
        {
            var personal = await _usuarioRepo.BuscarPersonal(identityUserId);
            var tramite = await _tramiteRepo.BuscarTramiteCTCEPorId(tramiteId);

            if (tramite.PersonalId != personal.Id)
                throw new ApplicationException("No puedes acceder a este tramite");

            return tramite;
        }
        public async Task AsignarPersonal(int tramiteId, string identityUserId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var personal = await _usuarioRepo.BuscarPersonal(identityUserId);
                var tramite = await _tramiteRepo.GetByIdAsync(tramiteId);
                if (tramite.PersonalId != null)
                    throw new ApplicationException("El trámite ya tiene personal asignado.");
                tramite.AsignarPersonal(personal.Id);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        public async Task<ArchivoDescargaDTO> ObtenerDocumentoPorId(int documentoId, string identityUserId)
        {
            // Primero, obtener el trámiteId del documento
            var tramiteId = await _tramiteRepo.ObtenerTramiteIdPorDocumentoId(documentoId);

            return await _documentoService.ObtenerBytesParaDescarga(tramiteId, documentoId, identityUserId);
        }
    }
}