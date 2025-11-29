using PortalCOSIE.Domain.Entities.Bitacoras;

namespace PortalCOSIE.Application.Interfaces
{
    public class BitacoraService : IBitacoraService
    {
        private readonly IBitacoraRepository _bitacoraRepo;
        public BitacoraService(
            IBitacoraRepository bitacoraRepo
            )
        {
            _bitacoraRepo = bitacoraRepo;
        }

        public async Task<IEnumerable<EntradaBitacora>> ListarBitacoraAsync()
        {
            return await _bitacoraRepo.ListarConCorreo();
        }
    }

}