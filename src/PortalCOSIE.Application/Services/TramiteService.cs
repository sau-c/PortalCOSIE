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

        public IEnumerable<Tramite> ListarTodos()
        {
            return _tramiteRepository.GetAll();
        }
        public Tramite? BuscarPorId(int id)
        {
            return _tramiteRepository.GetById(id);
        }

        public void Crear(Tramite tramite)
        {
            _tramiteRepository.Add(tramite);
            _tramiteRepository.Save();
        }

        public void Actualizar(Tramite tramite)
        {
            _tramiteRepository.Update(tramite);
            _tramiteRepository.Save();
        }

        public void Eliminar(int id)
        {
            var tramite = _tramiteRepository.GetById(id);
            _tramiteRepository.Delete(tramite);
            _tramiteRepository.Save();
        }
    }
}