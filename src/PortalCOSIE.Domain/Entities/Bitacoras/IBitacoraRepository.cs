namespace PortalCOSIE.Domain.Entities.Bitacoras
{
    public interface IBitacoraRepository
    {
        Task<IEnumerable<EntradaBitacora>> ListarConCorreo();
    }
}
