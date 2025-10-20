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
        private readonly IGenericRepo<TipoTramite> _tipoTramiteRepository;
        private readonly IGenericRepo<DocumentoEstado> _documentoEstadoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TramiteService(
            IGenericRepo<Tramite> tramiteRepository,
            IGenericRepo<TramiteEstado> tramiteEstadoRepository,
            IGenericRepo<TipoTramite> tipoTramiteRepository,
            IGenericRepo<DocumentoEstado> documentoEstadoRepository,
            IUnitOfWork unitOfWork)
        {
            _tramiteRepository = tramiteRepository;
            _tramiteEstadoRepository = tramiteEstadoRepository;
            _tipoTramiteRepository = tipoTramiteRepository;
            _documentoEstadoRepository = documentoEstadoRepository;
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


        //TramiteEstado CRUD
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
            await _unitOfWork.GenericRepo<TramiteEstado>().DeleteAsync(estado);
            await _unitOfWork.CompleteAsync();
        }

        //DocumentoEstado CRUD
        public async Task<IEnumerable<DocumentoEstado>> ListarEstadosDocumento()
        {
            return await _documentoEstadoRepository.GetAllAsync();
        }
        public async Task EliminarEstadoDocumento(int id)
        {
            var estado = await _documentoEstadoRepository.GetByIdAsync(id);
            await _unitOfWork.GenericRepo<DocumentoEstado>().DeleteAsync(estado);
            await _unitOfWork.CompleteAsync();
        }
        public async Task EditarEstadoDocumento(DocumentoEstado documentoEstado)
        {
            await _documentoEstadoRepository.UpdateAsync(documentoEstado);
        }
        public async Task CrearEstadoDocumento(DocumentoEstado documentoEstado)
        {
            await _unitOfWork.GenericRepo<DocumentoEstado>().AddAsync(documentoEstado);
            await _unitOfWork.CompleteAsync();
        }

        //TipoTramite CRUD
        public async Task<IEnumerable<TipoTramite>> ListarTipoTramite()
        {
            return await _tipoTramiteRepository.GetAllAsync();
        }
        public async Task EliminarTipoTramite(int id)
        {
            var estado = await _tipoTramiteRepository.GetByIdAsync(id);
            await _unitOfWork.GenericRepo<TipoTramite>().DeleteAsync(estado);
            await _unitOfWork.CompleteAsync();
        }
        public async Task EditarTipoTramite(TipoTramite tipoTramite)
        {
            await _tipoTramiteRepository.UpdateAsync(tipoTramite);
        }
        public async Task CrearTipoTramite(TipoTramite tipoTramite)
        {
            await _unitOfWork.GenericRepo<TipoTramite>().AddAsync(tipoTramite);
            await _unitOfWork.CompleteAsync();
        }
    }
}