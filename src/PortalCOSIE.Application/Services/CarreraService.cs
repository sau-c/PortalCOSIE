using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Interfaces;

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
            return await _carreraRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Carrera>> ListarTodoCarrerasAsync()
        {
            return await _carreraRepository.GetAllAsync(true);
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
                unidad.SoftDelete();
                await _unitOfWork.SaveChangesAsync();
            }
        }
        public async Task CrearCarreraAsync(string nombre)
        {
            await _unitOfWork.GenericRepo<Carrera>().AddAsync(new Carrera(nombre));
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task EditarCarreraAsync(int id, string nombre)
        {
            var carrera = await _carreraRepository.GetByIdAsync(id);

            if (carrera == null)
                throw new Exception("Carrera no encontrada");

            carrera.ActualizarNombre(nombre);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task ToggleCarrrera(int id)
        {
            var carrera = await _carreraRepository.GetByIdAsync(id, true);
            if (carrera.IsDeleted)
                carrera.Restore();
            else
                carrera.SoftDelete();
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
