using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Interfaces
{
    public class BitacoraService : IBitacoraService
    {
        private readonly IBaseRepository<EntradaBitacora> _bitacoraRepo;
        private readonly IUnitOfWork _unitOfWork;
        public BitacoraService(
            IBaseRepository<EntradaBitacora> bitacoraRepo,
            IUnitOfWork unitOfWork
            )
        {
            _bitacoraRepo = bitacoraRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EntradaBitacora>> ListarBitacoraAsync()
        {
            return await _bitacoraRepo.GetAllAsync(false);
        }
    }

}