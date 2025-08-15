using PortalCOSIE.Application.DTO.Catalogo;

namespace PortalCOSIE.Application.Interfaces
{
    public interface ICatalogoService
    {
        Task<IEnumerable<CarreraDTO>> ListarCarrerasAsync();
        Task<IEnumerable<PlanEstudioDTO>> ListarPlanesEstudioAsync();
    }
}
