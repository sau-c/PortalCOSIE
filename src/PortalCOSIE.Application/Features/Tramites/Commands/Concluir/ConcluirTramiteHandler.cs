using PortalCOSIE.Application.Services.Crypto;
using PortalCOSIE.Application.Services.Storage;
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
        private readonly IStorageService _storageService;
        private readonly ICriptoService _criptoService;
        private readonly IUnitOfWork _unitOfWork;

        public ConcluirTramiteHandler(
            ITramiteRepository tramiteRepo,
            IUsuarioRepository usuarioRepo,
            IStorageService storageService,
            ICriptoService criptoService,
            IUnitOfWork unitOfWork)
        {
            _tramiteRepo = tramiteRepo;
            _usuarioRepo = usuarioRepo;
            _storageService = storageService;
            _criptoService = criptoService;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<string>> Handle(ConcluirTramiteCommand command)
        {
            if (command.Archivo == null || command.Archivo.Contenido == null || command.Archivo.Contenido.Length == 0)
                return Result<string>.Failure("El acuse es obligatorio para concluir el trámite.");
            
            TramiteCTCE tramite = await _tramiteRepo.ObtenerTramiteCTCEPorIdParaRevision(command.TramiteId);
            if (tramite is null)
                return Result<string>.Failure("Trámite no encontrado.");
            if (tramite.EstadoTramiteId != EstadoTramite.EsperandoAcuse.Id)
                return Result<string>.Failure("El trámite no se encuentra en un estado válido para ser concluido.");

            var usuario = await _usuarioRepo.BuscarUsuario(command.IdentityUserId);
            if (usuario is null)
                return Result<string>.Failure("Usuario no encontrado.");
            if (!tramite.PuedeSerAtendidoPor(usuario.Id))
                return Result<string>.Failure("El usuario no tiene permisos para atender este trámite.");
            
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                // 1️ Leer todo a memoria
                byte[] archivoBytes;
                using (var ms = new MemoryStream())
                {
                    await command.Archivo.Contenido.CopyToAsync(ms);
                    archivoBytes = ms.ToArray();
                }

                // 2️ Firmar usando MemoryStream
                byte[] firma;
                using (var streamFirma = new MemoryStream(archivoBytes))
                {
                    firma = _criptoService.FirmarDocumento(
                        streamFirma,
                        command.CertificadoCer,
                        command.LlaveKey,
                        command.PasswordKey
                    );
                }

                // 3️ Subir a Azure usando otro MemoryStream
                using (var streamSubida = new MemoryStream(archivoBytes))
                {
                    string blobPath = await _storageService.UploadAsync(
                        streamSubida,
                        command.Archivo.Nombre
                    );

                    // Guardar documento y firmar en la BD
                    var documento = new Documento(
                        command.Archivo.Nombre,
                        blobPath,
                        tramite.Id,
                        EstadoDocumento.Validado.Id,
                        TipoDocumento.DictamenCTCE.Id,
                        firma
                    );

                    tramite.AgregarDocumento(documento);
                }

                tramite.VerificarEstadoTramite();
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return Result<string>.Success("Trámite concluido exitosamente.");
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

    }
}
