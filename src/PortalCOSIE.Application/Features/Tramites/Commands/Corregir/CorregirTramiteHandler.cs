using PortalCOSIE.Application.Features.Tramites.Commands.Corregir;
using PortalCOSIE.Application.Features.Tramites.DTO;
using PortalCOSIE.Application.Services.Crypto;
using PortalCOSIE.Application.Services.Storage;
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
        private readonly IStorageService _storageService;
        private readonly ICriptoService _criptoService;
        private readonly IUnitOfWork _unitOfWork;

        public CorregirTramiteCTCEHandler(
            ITramiteRepository tramiteRepo,
            IUsuarioRepository usuarioRepo,
            IStorageService storageService,
            ICriptoService criptoService,
            IUnitOfWork unitOfWork
            )
        {
            _tramiteRepo = tramiteRepo;
            _usuarioRepo = usuarioRepo;
            _storageService = storageService;
            _criptoService = criptoService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(CorregirTramiteCommand command)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                // 1. Recuperar el trámite existente (incluyendo sus documentos)
                TramiteCTCE tramite = await _tramiteRepo.ObtenerTramiteCTCEPorIdParaRevision(command.TramiteId);
                if (tramite == null)
                    return Result<string>.Failure("El trámite especificado no existe.");
                if (tramite.EstadoTramiteId != EstadoTramite.DocumentosPendientes.Id)
                    return Result<string>.Failure("El estado actual del trámite no permite corrección.");

                // Validar que el trámite pertenezca al alumno (seguridad básica)
                Usuario usuario = await _usuarioRepo.BuscarUsuario(command.IdentityUserId);
                if (!tramite.PerteneceAAlumno(usuario.Id))
                    return Result<string>.Failure("No tienes permisos para corregir este trámite.");

                // 2. Reemplazar Documentos
                // Solo procesamos los documentos que NO vengan nulos en el comando.
                // Si son nulos, conservamos los que ya tenía el trámite.
                await ProcesarReemplazoAsync(tramite, command.Identificacion, TipoDocumento.Identificacion);
                await ProcesarReemplazoAsync(tramite, command.BoletaGlobal, TipoDocumento.BoletaGlobal);
                await ProcesarReemplazoAsync(tramite, command.CartaExposicionMotivos, TipoDocumento.CartaExposicionMotivos);
                await ProcesarReemplazoAsync(tramite, command.Probatorios, TipoDocumento.Probatorios);

                // 4. Cambiar estado
                tramite.VerificarEstadoTramite();

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                return Result<string>.Success("La corrección se ha enviado con éxito.");
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        private async Task ProcesarReemplazoAsync(TramiteCTCE tramite, ArchivoDTO? nuevoArchivo, TipoDocumento tipo)
        {
            // Si el DTO es nulo o no tiene contenido, significa que el usuario NO cambió este archivo.
            // No hacemos nada y conservamos el anterior.
            if (nuevoArchivo == null || nuevoArchivo.Contenido == null || nuevoArchivo.Contenido.Length == 0)
                return;

            // 1. Buscar el documento existente en la lista del trámite
            var documentoExistente = tramite.Documentos.FirstOrDefault(d => d.TipoDocumentoId == tipo.Id);

            if (!documentoExistente.PermiteCorreccion())
                // Seguridad extra: Solo se pueden reemplazar documentos que estén en estado "Rechazado"
                throw new ApplicationException($"El documento {documentoExistente.Nombre} no está en estado " +
                    $"{EstadoDocumento.ConErrores.Nombre} o {EstadoDocumento.Incorrecto.Nombre} y no puede ser corregido.");
            
            // 2. Subir el NUEVO archivo a Azure
            string nuevoBlobPath = await _storageService.UploadAsync(nuevoArchivo.Contenido, nuevoArchivo.Nombre);
            byte[]? nuevoHash = _criptoService.CalcularHash(nuevoArchivo.Contenido);

            // 3. Actualizar la entidad Documento existente
            documentoExistente.ActualizarDocumento(nuevoArchivo.Nombre, nuevoBlobPath, nuevoHash);
        }
    }
}