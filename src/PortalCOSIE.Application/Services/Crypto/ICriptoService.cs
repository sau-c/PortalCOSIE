namespace PortalCOSIE.Application.Services.Crypto
{
    public interface ICriptoService
    {
        public byte[] CalcularHash(Stream archivo);
    }
}
