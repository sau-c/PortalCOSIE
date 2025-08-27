using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Application.DTO.Catalogo;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Services
{
    public class CatalogoService : ICatalogoService
    {
        private readonly IGenericRepo<Carrera> _carreraRepository;
        
        public CatalogoService(IGenericRepo<Carrera> carreraRepository)
        {
            _carreraRepository = carreraRepository;
        }
        
        public async Task<IEnumerable<CarreraDTO>> ListarCarrerasAsync()
        {
            var carreras = await _carreraRepository.GetAllAsync();
            return carreras.Select(c => new CarreraDTO { Id = c.Id, Nombre = c.Nombre });
        }
    }
}
