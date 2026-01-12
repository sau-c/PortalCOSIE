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

            // Validar que los documentos con estado ConErrores o Incorrecto no vengan vacios del command:
            if (tramite.Documentos.Any(d =>
                (d.EstadoDocumentoId == EstadoDocumento.ConErrores.Id ||
                 d.EstadoDocumentoId == EstadoDocumento.Incorrecto.Id) &&
                ((d.TipoDocumentoId == TipoDocumento.Identificacion.Id && (command.Identificacion == null || command.Identificacion.Contenido == null || command.Identificacion.Contenido.Length == 0)) ||
                 (d.TipoDocumentoId == TipoDocumento.BoletaGlobal.Id && (command.BoletaGlobal == null || command.BoletaGlobal.Contenido == null || command.BoletaGlobal.Contenido.Length == 0)) ||
                 (d.TipoDocumentoId == TipoDocumento.CartaExposicionMotivos.Id && (command.CartaExposicionMotivos == null || command.CartaExposicionMotivos.Contenido == null || command.CartaExposicionMotivos.Contenido.Length == 0)) ||
                 (d.TipoDocumentoId == TipoDocumento.Probatorios.Id && (command.Probatorios == null || command.Probatorios.Contenido == null || command.Probatorios.Contenido.Length == 0))
                )))
                return Result<string>.Failure("Debes proporcionar los archivos para todos los documentos que requieren corrección.");

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                // 2. Reemplazar Documentos
                // Solo procesamos los documentos que NO vengan nulos en el comando.
                // Si son nulos, conservamos los que ya tenía el trámite.
                await ProcesarReemplazoAsync(tramite, command.Identificacion, TipoDocumento.Identificacion, command.LlaveKey, command.CertificadoCer, command.PasswordKey);
                await ProcesarReemplazoAsync(tramite, command.BoletaGlobal, TipoDocumento.BoletaGlobal, command.LlaveKey, command.CertificadoCer, command.PasswordKey);
                await ProcesarReemplazoAsync(tramite, command.CartaExposicionMotivos, TipoDocumento.CartaExposicionMotivos, command.LlaveKey, command.CertificadoCer, command.PasswordKey);
                await ProcesarReemplazoAsync(tramite, command.Probatorios, TipoDocumento.Probatorios, command.LlaveKey, command.CertificadoCer, command.PasswordKey);

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

        private async Task ProcesarReemplazoAsync(TramiteCTCE tramite, ArchivoDTO? nuevoArchivo, TipoDocumento tipo, Stream key, Stream cer, string pass)
        {
            // Si el DTO es nulo o no tiene contenido, significa que el usuario NO cambió este archivo.
            // No hacemos nada y conservamos el anterior.
            if (nuevoArchivo == null || nuevoArchivo.Contenido == null || nuevoArchivo.Contenido.Length == 0)
                return;

            // 1. Buscar el documento existente en la lista del trámite
            var documentoExistente = tramite.Documentos.FirstOrDefault(d => d.TipoDocumentoId == tipo.Id);

            if (!documentoExistente.PermiteCorreccion())
                // Seguridad extra: Solo se pueden reemplazar documentos que estén en estado invalido
                throw new ApplicationException($"El documento {documentoExistente.Nombre} no está en estado " +
                    $"{EstadoDocumento.ConErrores.Nombre} o {EstadoDocumento.Incorrecto.Nombre} y no puede ser corregido.");

            // Copiar a memoria para poder usarlo varias veces
            byte[] archivoBytes;
            using (var ms = new MemoryStream())
            {
                await nuevoArchivo.Contenido.CopyToAsync(ms);
                archivoBytes = ms.ToArray();
            }

            // Firmar con MemoryStream
            byte[] firma;
            using (var streamFirma = new MemoryStream(archivoBytes))
            {
                firma = _criptoService.FirmarDocumento(streamFirma, cer, key, pass);
            }

            // Subir a Azure con otro MemoryStream
            string nuevoBlobPath;
            using (var streamSubida = new MemoryStream(archivoBytes))
            {
                nuevoBlobPath = await _storageService.UploadAsync(streamSubida, nuevoArchivo.Nombre);
            }
            // 3. Actualizar la entidad Documento existente
            documentoExistente.ActualizarDocumento(nuevoArchivo.Nombre, nuevoBlobPath, firma);
        }
    }
}