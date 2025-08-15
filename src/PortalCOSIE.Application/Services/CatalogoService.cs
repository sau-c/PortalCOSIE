using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Application.DTO.Catalogo;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Services
{
    public class CatalogoService : ICatalogoService
    {
        private readonly IGenericRepo<Carrera> _carreraRepository;
        private readonly IGenericRepo<PlanEstudio> _planEstudioRepository;
        
        public CatalogoService(IGenericRepo<Carrera> carreraRepository, IGenericRepo<PlanEstudio> planEstudioRepository)
        {
            _carreraRepository = carreraRepository;
            _planEstudioRepository = planEstudioRepository;
        }
        
        public async Task<IEnumerable<CarreraDTO>> ListarCarrerasAsync()
        {
            var carreras = await _carreraRepository.GetAllAsync();
            return carreras.Select(c => new CarreraDTO { Id = c.Id, Nombre = c.Nombre });
        }
        public async Task<IEnumerable<PlanEstudioDTO>> ListarPlanesEstudioAsync()
        {
            var carreras = await _planEstudioRepository.GetAllAsync();
            return carreras.Select(c => new PlanEstudioDTO{ Id = c.Id, Nombre = c.Nombre });
        }
    }
}
