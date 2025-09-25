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

        public IEnumerable<object> ListarPeriodos()
        {
            var periodos = new List<object>();
            int periodoActual = DateTime.Now.Year + 1;
            
            for (int año = 2010; año <= periodoActual; año++)
            {
                periodos.Add(new { Id = $"{año}1", Periodo = $"{año}/1" });
                periodos.Add(new { Id = $"{año}2", Periodo = $"{año}/2" });
            }

            return periodos;
        }

        public async Task<IEnumerable<UnidadAprendizaje>> ListarUnidadesAprendizajeAsync(int carreraId)
        {
            var unidades = await _unidadRepository.GetAllAsync();
            return unidades;
        }
    }
}
