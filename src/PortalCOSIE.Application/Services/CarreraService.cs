using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Interfaces;
using System.Linq;

namespace PortalCOSIE.Application.Services
{
    public class CarreraService : ICarreraService
    {
        private readonly IGenericRepo<Carrera> _carreraRepository;
        private readonly IGenericRepo<UnidadAprendizaje> _unidadRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CarreraService(IGenericRepo<Carrera> carreraRepository, IGenericRepo<UnidadAprendizaje> unidadRepository, IUnitOfWork unitOfWork)
        {
            _carreraRepository = carreraRepository;
            _unidadRepository = unidadRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Carrera>> ListarCarrerasAsync()
        {
            var carreras = await _carreraRepository.GetAllAsync();
            return carreras.Select(c => new Carrera { Id = c.Id, Nombre = c.Nombre });
        }

        public async Task<Carrera?> ListarCarreraConUnidadesAsync(string carrera)
        {
            var unidades = await _carreraRepository.GetFirstOrDefaultAsync(
                u => u.Nombre == carrera,
                u => u.UnidadesAprendizaje
            );

            return unidades;
        }

        public async Task<IEnumerable<Carrera?>> ListarUnidadesAsync(string carrera)
        {
            var unidades = await _carreraRepository.GetListAsync(
                u => u.Nombre == carrera,
                u => u.UnidadesAprendizaje
            );

            return unidades;
        }

        public async Task EliminarUnidad(int id)
        {
            var unidad = await _unidadRepository.GetByIdAsync(id);
            if (unidad != null)
            {
                await _unitOfWork.GenericRepo<UnidadAprendizaje>().DeleteAsync(unidad);
                await _unitOfWork.CompleteAsync();
            }
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
    }
}
