using PortalCOSIE.Application.Features.Tramites.DTO;
using PortalCOSIE.Application.Services.Crypto;
using PortalCOSIE.Application.Services.Storage;
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
        private readonly IStorageService _storageService;
        private readonly ICriptoService _criptoService;
        private readonly IUnitOfWork _unitOfWork;

        public SolicitarCTCEHandler(
            IUsuarioRepository usuarioRepo,
            IBaseRepository<PeriodoConfig, int> periodoRepo,
            ITramiteRepository tramiteRepo,
            IStorageService storageService,
            ICriptoService criptoService,
            IUnitOfWork unitOfWork
            )
        {
            _usuarioRepo = usuarioRepo;
            _periodoRepo = periodoRepo;
            _tramiteRepo = tramiteRepo;
            _storageService = storageService;
            _criptoService = criptoService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(SolicitarCTCECommand command)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                if (command.BoletaGlobal == null || command.Identificacion == null || command.CartaExposicionMotivos == null || command.Probatorios == null)
                    throw new Exception("Todos los documentos son obligatorios para solicitar.");

                var alumno = await _usuarioRepo.BuscarUsuario(command.IdentityUserId);
                var periodoConfig = await _periodoRepo.GetByIdAsync(1);
                string periodo = $"{periodoConfig.AnioActual}/{periodoConfig.PeriodoActual}";

                // 1. Unidades reprobadas - convertir DTOs a entidades
                var unidadesReprobadasEntities = command.UnidadesReprobadas
                .Select(u => new UnidadReprobada(u.UnidadAprendizajeId, u.PeriodoCurso, u.PeriodoRecurse))
                .ToList();

                // 2. Crear el Trámite
                // Aquí se procesan los documentos para subirlos al storageService
                // y se crean las entidades Documento asociadas
                var tramite = new TramiteCTCE(
                    alumno.Id,
                    TipoTramite.DictamenInterno.Id,
                    periodo,
                    command.Peticion,
                    command.TieneDictamenesAnteriores,
                    unidadesReprobadasEntities,
                    await ProcesarDocumentoAsync(command.Identificacion, TipoDocumento.Identificacion),
                    await ProcesarDocumentoAsync(command.BoletaGlobal, TipoDocumento.BoletaGlobal),
                    await ProcesarDocumentoAsync(command.CartaExposicionMotivos, TipoDocumento.CartaExposicionMotivos),
                    await ProcesarDocumentoAsync(command.Probatorios, TipoDocumento.Probatorios)
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

        private async Task<Documento> ProcesarDocumentoAsync(ArchivoDTO documentoDto, TipoDocumento tipo)
        {
            // Validar si viene nulo (por seguridad)
            if (documentoDto == null || documentoDto.Contenido == null)
                throw new Exception($"El documento {tipo.Nombre} es requerido.");

            // 2. Subir a Azure al container "pruebas" (o el que tenga configurado el servicio)
            // El servicio debe devolver el nombre único o la ruta (ej: "guid-archivo.pdf")
            string blobPath = await _storageService.UploadAsync(documentoDto.Contenido, documentoDto.Nombre);

            // 3. Crear la Entidad
            return new Documento(
                    documentoDto.Nombre,
                    blobPath,
                    0,
                    tipo.Id,
                    _criptoService.CalcularHash(documentoDto.Contenido)
                );
        }
    }
}
