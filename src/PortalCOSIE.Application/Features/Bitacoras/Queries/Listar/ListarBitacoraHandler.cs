using PortalCOSIE.Domain.Entities.EntradaBitacoras;

namespace PortalCOSIE.Application.Features.Bitacoras.Queries.Listar
{
    public class ListarBitacoraHandler : IRequestHandler<ListarBitacoraQuery, IEnumerable<EntradaBitacora>>
    {
        private readonly IBitacoraRepository _bitacoraRepo;
        public ListarBitacoraHandler(IBitacoraRepository bitacoraRepo)
            => _bitacoraRepo = bitacoraRepo;

        public async Task<IEnumerable<EntradaBitacora>> Handle(ListarBitacoraQuery query)
            => await _bitacoraRepo.ListarConCorreo();
    }
}
