namespace PortalCOSIE.Application.Services
{
    public interface ICriptoService
    {
        public byte[] CalcularHash(Stream archivo);
    }
}
