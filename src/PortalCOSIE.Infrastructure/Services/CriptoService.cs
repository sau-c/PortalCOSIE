using PortalCOSIE.Application.Services.Crypto;
using System.Security.Cryptography;

namespace PortalCOSIE.Infrastructure.Services
{
    public class CriptoService : ICriptoService
    {
        public byte[] CalcularHash(Stream archivo)
        {
            if (archivo == null) return null;

            using (var sha512 = SHA512.Create())
            {
                // Asegúrate de posicionar el stream al inicio
                if (archivo.CanSeek)
                    archivo.Position = 0;

                return sha512.ComputeHash(archivo);;
            }
        }
    }
}
