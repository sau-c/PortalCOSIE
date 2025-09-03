using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Services
{
    public class CatalogoService : ICatalogoService
    {
        private readonly IGenericRepo<Carrera> _carreraRepository;
        private readonly IGenericRepo<UnidadAprendizaje> _unidadRepository;
        
        public CatalogoService(IGenericRepo<Carrera> carreraRepository, IGenericRepo<UnidadAprendizaje> unidadRepository)
        {
            _carreraRepository = carreraRepository;
            _unidadRepository = unidadRepository;
        }
        
        public async Task<IEnumerable<Carrera>> ListarCarrerasAsync()
        {
            var carreras = await _carreraRepository.GetAllAsync();
            return carreras.Select(c => new Carrera { Id = c.Id, Nombre = c.Nombre });
        }

        public async Task<IEnumerable<UnidadAprendizaje>> ListarUnidadesAprendizajeAsync()
        {
            var unidades = await _unidadRepository.GetAllAsync();
            return unidades.Select(c => new UnidadAprendizaje { Id = c.Id, Nombre = c.Nombre, Semestre = c.Semestre });
        }
    }
}
