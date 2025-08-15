using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Application.Interfaces.Common;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Interfaces;
using System.Linq.Expressions;

namespace PortalCOSIE.Application
{
    public class TramiteService : ITramiteService
    {
        private readonly IGenericRepo<Tramite> _tramiteRepository;

        public TramiteService(IGenericRepo<Tramite> tramiteRepository)
        {
            _tramiteRepository = tramiteRepository;
        }

        public async Task<IEnumerable<Tramite>> ListarTodos()
        {
            return await _tramiteRepository.GetAllAsync();
        }
        public async Task<Tramite?> BuscarPorId(int id)
        {
            return await _tramiteRepository.GetByIdAsync(id);
        }

        public async Task<Tramite> Crear(Tramite tramite)
        {
            return await _tramiteRepository.AddAsync(tramite);
        }

        public async Task Actualizar(Tramite tramite)
        {
            await _tramiteRepository.UpdateAsync(tramite);
        }

        public async Task Eliminar(int id)
        {
            var tramite = await _tramiteRepository.GetByIdAsync(id);
            await _tramiteRepository.DeleteAsync(tramite);
        }
    }
}