using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Application.Features.Tramites.Queries.ListarTramites
{
    public class ListarTramitesHandler : IRequestHandler<ListarTramitesQuery, IEnumerable<Tramite>>
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly ITramiteRepository _tramiteRepo;
        public ListarTramitesHandler(
            IUsuarioRepository usuarioRepo,
            ITramiteRepository tramiteRepo
            )
        {
            _usuarioRepo = usuarioRepo;
            _tramiteRepo = tramiteRepo;
        }
        public async Task<IEnumerable<Tramite>> Handle(ListarTramitesQuery query)
        {
            var usuario = await _usuarioRepo.BuscarUsuario(query.IdentityUserId);
            // 1. Definir parámetros para el repositorio
            int? filtroAlumno = null;
            int? filtroPersonal = null;

            // 2. Aplicar lógica según el rol
            switch (query.rol)
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
    }
}
