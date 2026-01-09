using PortalCOSIE.Application.Features.Tramites.Commands.Corregir;
using PortalCOSIE.Application.Features.Tramites.DTO;
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
                
                string blobPath = await _storageService.UploadAsync(
                    command.Archivo.Contenido,
                    command.Archivo.Nombre
                    );

                var documento = new Documento(
                    command.Archivo.Nombre,
                    blobPath,
                    tramite.Id,
                    TipoDocumento.DictamenCTCE.Id,
                    _criptoService.CalcularHash(command.Archivo.Contenido)
                );

                documento.CambiarEstado(EstadoDocumento.Validado);
                tramite.AgregarDocumento(documento);
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
