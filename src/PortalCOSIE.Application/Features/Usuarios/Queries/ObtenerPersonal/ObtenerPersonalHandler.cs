using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Application.Features.Usuarios.Queries.ObtenerPersonal
{
    public class ObtenerPersonalHandler : IRequestHandler<ObtenerPersonalQuery, Personal>
    {
        private readonly IUsuarioRepository _usuarioRepo;
        public ObtenerPersonalHandler(IUsuarioRepository usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }
        public async Task<Personal> Handle(ObtenerPersonalQuery query)
        {
            var personal = await _usuarioRepo.BuscarPersonal(query.IdentityUserId);

            if (personal == null)
                throw new ApplicationException("No se encontro el personal");
            return personal;
        }
    }
}
