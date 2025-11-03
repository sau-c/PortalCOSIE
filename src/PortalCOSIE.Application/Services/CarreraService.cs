using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities.Carreras;
using PortalCOSIE.Domain.Enums;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Services
{
    public class CarreraService : ICarreraService
    {
        private readonly IBaseRepository<Carrera> _carreraRepo;
        private readonly IBaseRepository<UnidadAprendizaje> _unidadRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CarreraService(
            IBaseRepository<Carrera> carreraRepo,
            IBaseRepository<UnidadAprendizaje> unidadRepo,
            IUnitOfWork unitOfWork)
        {
            _carreraRepo = carreraRepo;
            _unidadRepo = unidadRepo;
            _unitOfWork = unitOfWork;
        }
        #region CARRERA
        public async Task<IEnumerable<Carrera>> ListarActivasAsync()
        {
            return await _carreraRepo.GetAllWhereAsync(c => c.IsDeleted == false);
        }
        public async Task<IEnumerable<Carrera>> ListarAsync()
        {
            return await _carreraRepo.GetAllAsync();
        }
        public async Task<Carrera?> ListarConUnidadesAsync(string carrera)
        {
            var unidades = await _carreraRepo.GetFirstOrDefaultWhereAsync(
                u => u.Nombre == carrera,
                u => u.UnidadesAprendizaje
            );
            return unidades;
        }
        public async Task CrearCarreraAsync(string nombre)
        {
            await _unitOfWork.BaseRepo<Carrera>().AddAsync(new Carrera(nombre));
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task EditarCarreraAsync(int id, string nombre)
        {
            var carrera = await _carreraRepo.GetByIdAsync(id);

            if (carrera == null)
                throw new Exception("Carrera no encontrada");

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
        public async Task<IEnumerable<UnidadAprendizaje>> ListarUnidadesAsync(string nombreCarrera)
        {
            var unidades = await _unidadRepo.GetAllWhereAsync(
                u => u.Nombre == nombreCarrera
            );

            return unidades;
        }

        public async Task EliminarUnidad(int unidadId)
        {
            var unidad =  await _unidadRepo.GetByIdAsync(unidadId);
            if (unidad == null)
                throw new ApplicationException("La unidad de aprendizaje no existe.");

            unidad.SoftDelete();
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task CrearUnidadAsync(string nombre, int carreraId, Semestre semestre)
        {
            await _unitOfWork.BaseRepo<UnidadAprendizaje>().AddAsync(
                new UnidadAprendizaje(nombre, carreraId, semestre)
                );
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task EditarUnidadAsync(int id, string nombre, Semestre semestre)
        {
            var unidad = await _unitOfWork.BaseRepo<UnidadAprendizaje>().GetByIdAsync(id);
            if (unidad == null)
                throw new ApplicationException("Unidad no encontrada");
            unidad.SetNombre(nombre);
            unidad.SetSemestre(semestre);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task ToggleUnidad(int id)
        {
            var unidad = await _unidadRepo.GetByIdAsync(id);
            if (unidad.IsDeleted)
                unidad.Restore();
            else
                unidad.SoftDelete();
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion
    }
}
