namespace PortalCOSIE.Domain.Entities.EntradaBitacoras
{
    public interface IBitacoraRepository
    {
        Task<IEnumerable<EntradaBitacora>> ListarConCorreo();
    }
}
