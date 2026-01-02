using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Entities.Tramites.CTCE;
using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Application.Features.Tramites.Queries.ObtenerTramiteCTCEPorId
{
    public class ObtenerTramiteCTCEPorIdHandler : IRequestHandler<ObtenerTramiteCTCEPorIdQuery, TramiteCTCE?>
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly ITramiteRepository _tramiteRepo;
        public ObtenerTramiteCTCEPorIdHandler(
            IUsuarioRepository usuarioRepo,
            ITramiteRepository tramiteRepo
            )
        {
            _usuarioRepo = usuarioRepo;
            _tramiteRepo = tramiteRepo;
        }
        public async Task<TramiteCTCE?> Handle(ObtenerTramiteCTCEPorIdQuery command)
        {
            var personal = await _usuarioRepo.BuscarPersonal(command.IdentityUserId);
            var tramite = await _tramiteRepo.BuscarTramiteCTCEPorId(command.TramiteId);

            if (tramite.PersonalId != personal.Id)
                throw new ApplicationException("No puedes acceder a este tramite");

            return tramite;
        }
    }
}
