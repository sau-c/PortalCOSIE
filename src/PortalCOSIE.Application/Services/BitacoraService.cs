using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Interfaces
{
    public class BitacoraService : IBitacoraService
    {
        private readonly IBaseRepository<Bitacora> _bitacoraRepo;
        private readonly IUnitOfWork _unitOfWork;
        public BitacoraService(
            IBaseRepository<Bitacora> bitacoraRepo,
            IUnitOfWork unitOfWork
            )
        {
            _bitacoraRepo = bitacoraRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Bitacora>> ListarBitacoraAsync()
        {
            return await _bitacoraRepo.GetAllAsync(false);
        }

        public async Task RegistrarAsync(string identityUserId, string accion, string entidad, string? entidadId, string ip, string userAgent)
        {
            var registro = new Bitacora (
                identityUserId,
                accion,
                entidad,
                entidadId,
                ip,
                userAgent
            );

            await _bitacoraRepo.AddAsync(registro);
            await _unitOfWork.SaveChangesAsync();
        }
    }

}