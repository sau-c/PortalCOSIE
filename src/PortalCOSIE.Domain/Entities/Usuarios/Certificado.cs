using PortalCOSIE.Domain.SharedKernel;

namespace PortalCOSIE.Domain.Entities.Usuarios
{
    /// <summary>
    /// Certificado público (.cer) asociado a un usuario.
    /// Su identificador es el thumbprint SHA-1 del certificado.
    /// </summary>
    public class Certificado : BaseEntity<string>
    {
        public string Sujeto { get; private set; }
        public string NumeroSerie { get; private set; }
        public DateTime VigenteDesde { get; private set; }
        public DateTime VigenteHasta { get; private set; }
        public byte[] CertificadoDer { get; private set; }

        private Certificado() { }

        public Certificado(
            string thumbprint,
            string sujeto,
            string numeroSerie,
            DateTime vigenteDesde,
            DateTime vigenteHasta,
            byte[] certificadoDer)
        {
            EstablecerThumbprint(thumbprint);
            EstablecerSujeto(sujeto);
            EstablecerNumeroSerie(numeroSerie);
            VigenteDesde = vigenteDesde;
            VigenteHasta = vigenteHasta;
            EstablecerCertificadoDer(certificadoDer);
        }

        public bool EstaVigenteEn(DateTime instanteUtc)
            => instanteUtc >= VigenteDesde && instanteUtc <= VigenteHasta;

        private void EstablecerThumbprint(string thumbprint)
        {
            if (string.IsNullOrWhiteSpace(thumbprint))
                throw new DomainException("El thumbprint del certificado no puede estar vacío.");
            if (thumbprint.Length > 64)
                throw new DomainException("El thumbprint no puede exceder 64 caracteres.");
            Id = thumbprint.Trim();
        }

        private void EstablecerSujeto(string sujeto)
        {
            if (string.IsNullOrWhiteSpace(sujeto))
                throw new DomainException("El sujeto del certificado no puede estar vacío.");
            if (sujeto.Length > 500)
                throw new DomainException("El sujeto del certificado no puede exceder 500 caracteres.");
            Sujeto = sujeto.Trim();
        }

        private void EstablecerNumeroSerie(string numeroSerie)
        {
            if (string.IsNullOrWhiteSpace(numeroSerie))
                throw new DomainException("El número de serie del certificado no puede estar vacío.");
            if (numeroSerie.Length > 100)
                throw new DomainException("El número de serie no puede exceder 100 caracteres.");
            NumeroSerie = numeroSerie.Trim();
        }

        private void EstablecerCertificadoDer(byte[] certificadoDer)
        {
            if (certificadoDer == null || certificadoDer.Length == 0)
                throw new DomainException("El certificado público no puede estar vacío.");
            CertificadoDer = certificadoDer.ToArray();
        }
    }
}