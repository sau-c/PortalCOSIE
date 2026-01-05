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
            // 1. Recuperar
            var tramite = await _tramiteRepo.BuscarTramiteCTCEPorId(command.TramiteId);
            if (tramite == null) throw new ApplicationException("Trámite no encontrado");

            // 2. Validar según Rol
            if (command.Rol == "Administrador") return tramite;

            if (command.Rol == "Alumno")
            {
                var alumno = await _usuarioRepo.BuscarUsuario(command.IdentityUserId);
                if (!tramite.PerteneceAAlumno(alumno.Id))
                    throw new ApplicationException("No es tienes acceso a este trámite");
                return tramite;
            }

            if (command.Rol == "Personal")
            {
                var personal = await _usuarioRepo.BuscarPersonal(command.IdentityUserId);
                if (!tramite.PuedeSerAtendidoPor(personal.Id))
                    throw new ApplicationException("No es tienes acceso a este trámite");
                return tramite;
            }
            throw new ApplicationException("Rol desconocido");
        }
    }
}
