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
        private readonly IGenericRepo<TramiteEstado> _tramiteEstadoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TramiteService(
            IGenericRepo<Tramite> tramiteRepository,
            IGenericRepo<TramiteEstado> tramiteEstadoRepository,
            IUnitOfWork unitOfWork)
        {
            _tramiteRepository = tramiteRepository;
            _tramiteEstadoRepository = tramiteEstadoRepository;
            _unitOfWork = unitOfWork;
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


        //TramitEstado CRUD
        public async Task<IEnumerable<TramiteEstado>> ListarEstados()
        {
            return await _tramiteEstadoRepository.GetAllAsync();
        }
        public async Task CrearEstado(TramiteEstado tramiteEstado)
        {
            await _unitOfWork.GenericRepo<TramiteEstado>().AddAsync(tramiteEstado);
            await _unitOfWork.CompleteAsync();
        }

        public async Task EditarEstado(TramiteEstado tramiteEstado)
        {
            await _tramiteEstadoRepository.UpdateAsync(tramiteEstado);
        }

        public async Task EliminarEstado(int id)
        {
            var estado = await _tramiteEstadoRepository.GetByIdAsync(id);
            await _tramiteEstadoRepository.DeleteAsync(estado);
        }
    }
}