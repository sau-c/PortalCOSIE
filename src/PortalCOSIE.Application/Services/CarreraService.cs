using PortalCOSIE.Application.DTO.Carreras;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities.Carreras;
using PortalCOSIE.Domain.Enums;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Services
{
    public class CarreraService : ICarreraService
    {
        private readonly ICarreraRepository _carreraRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CarreraService(
            ICarreraRepository carreraRepo,
            IUnitOfWork unitOfWork)
        {
            _carreraRepo = carreraRepo;
            _unitOfWork = unitOfWork;
        }
        
        #region CARRERA
        public async Task<IEnumerable<Carrera>> ListarActivasAsync()
        {
            return await _carreraRepo.GetAllAsync(true);
        }
        public async Task<IEnumerable<Carrera>> ListarAsync()
        {
            return await _carreraRepo.GetAllAsync(false);
        }
        public async Task<Carrera?> ListarConUnidadesAsync(string carrera)
        {
            return await _carreraRepo.ObtenerCarreraConUnidadesAsync(carrera);
        }
        public async Task CrearCarreraAsync(string nombre)
        {
            await _carreraRepo.AddAsync(new Carrera(nombre));
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task EditarCarreraAsync(int id, string nombre)
        {
            var carrera = await _carreraRepo.GetByIdAsync(id);

            if (carrera == null)
                throw new ApplicationException("Carrera no encontrada");

            carrera.ActualizarNombre(nombre);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task ToggleCarrrera(int id)
        {
            var carrera = await _carreraRepo.GetByIdAsync(id);
            if (carrera.IsDeleted)
                carrera.Restore();
            else
                carrera.SoftDelete();
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region UNIDADES
        public async Task<IEnumerable<UnidadAprendizaje>> ListarUnidadesAsync(string carrera)
        {
            return await _carreraRepo.ListarUnidadesPorCarreraAsync(carrera);
        }
        public async Task CrearUnidadAsync(UnidadAprendizajeDTO dto)
        {
            var carrera = await _carreraRepo.GetByIdAsync(dto.carreraId);
            carrera.AgregarUnidad(dto.id, dto.nombre, dto.semestre);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task EditarUnidadAsync(UnidadAprendizajeDTO dto)
        {
            var carrera = await _carreraRepo.ObtenerCarreraConUnidadesAsync(dto.nombre);
            if (carrera == null)
                throw new ApplicationException("Carrera no encontrada");
            
            var unidad = carrera.UnidadesAprendizaje
                .FirstOrDefault(u => u.Id == dto.id);
            
            if (unidad == null)
                throw new ApplicationException("Unidad de aprendizaje no encontrada");

            unidad.SetNombre(dto.nombre);
            unidad.SetSemestre(dto.semestre);

            await _unitOfWork.SaveChangesAsync();
        }
        public async Task ToggleUnidad(string carreraNombre, string id)
        {
            var carrera = await _carreraRepo.ObtenerCarreraConUnidadesAsync(carreraNombre);
            if (carrera == null)
                throw new ApplicationException("Carrera no encontrada");

            var unidad = carrera.UnidadesAprendizaje
                .FirstOrDefault(u => u.Id == id);

            if (unidad.IsDeleted)
                unidad.Restore();
            else
                unidad.SoftDelete();
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion
    }
}
