using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Application.Features.Usuarios.Queries.ObtenerUsuarioPorIdentityId
{
    public class ObtenerUsuarioPorIdentityIdHandler : IRequestHandler<ObtenerUsuarioPorIdentityIdQuery, Usuario>
    {
        private readonly IUsuarioRepository _usuarioRepo;
        public ObtenerUsuarioPorIdentityIdHandler(IUsuarioRepository usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }
        public async Task<Usuario> Handle(ObtenerUsuarioPorIdentityIdQuery query)
        {
            if (query.IdentityUserId == null)
                throw new ApplicationException("No se puede buscar un Id nulo");
            return await _usuarioRepo.BuscarUsuario(query.IdentityUserId);
        }
    }
}
