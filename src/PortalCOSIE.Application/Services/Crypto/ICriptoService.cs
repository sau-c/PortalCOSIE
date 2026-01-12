namespace PortalCOSIE.Application.Services.Crypto
{
    public interface ICriptoService
    {
        public byte[] FirmarDocumento(Stream contenido, Stream certPath, Stream keyPath, string password);
        public bool VerificarFirma(Stream contenido, Stream firma, Stream certificadoStream);
    }
}
