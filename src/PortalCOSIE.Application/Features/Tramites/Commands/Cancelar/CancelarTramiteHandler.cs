using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.Tramites.Commands.Cancelar
{
    public class CancelarTramiteHandler : IRequestHandler<CancelarTramiteCommand, Result<string>>
    {
        private readonly ITramiteRepository _tramiteRepo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IUnitOfWork _unitOfWork;
        public CancelarTramiteHandler(
            ITramiteRepository tramiteRepo,
            IUsuarioRepository usuarioRepo,
            IUnitOfWork unitOfWork
            )
        {
            _tramiteRepo = tramiteRepo;
            _usuarioRepo = usuarioRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(CancelarTramiteCommand command)
        {
            var tramite = await _tramiteRepo.ObtenerTramiteCTCEPorIdParaRevision(command.tramiteId);
            if (tramite is null)
                return Result<string>.Failure("Trámite no encontrado.");

            var usuario = await _usuarioRepo.BuscarUsuario(command.IdentityUserId);
            if (usuario is null)
                return Result<string>.Failure("Usuario no encontrado.");
            if (!tramite.PuedeSerAtendidoPor(usuario.Id))
                return Result<string>.Failure("No tienes permisos para atender este trámite.");

            tramite.AgregarObservaciones(command.observaciones);
            tramite.CambiarEstado(EstadoTramite.Cancelado);
            foreach(var documento in tramite.Documentos)
            {
                if (!documento.EstadoDocumento.EsFinal())
                    documento.CambiarEstado(EstadoDocumento.Validado);
            }
            await _unitOfWork.SaveChangesAsync();
            return Result<string>.Success("Trámite cancelado.");
        }
    }
}
