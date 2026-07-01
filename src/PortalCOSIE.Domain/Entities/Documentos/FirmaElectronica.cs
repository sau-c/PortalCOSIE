using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.SharedKernel;

namespace PortalCOSIE.Domain.Entities.Documentos
{
    /// <summary>
    /// Firma digital CMS asociada a un documento del trámite.
    /// Referencia el certificado del usuario que firmó.
    /// </summary>
    public class FirmaElectronica
    {
        public int Id { get; private set; }
        public string CertificadoId { get; private set; }
        public byte[] FirmaCms { get; private set; }
        public string Algoritmo { get; private set; }
        public DateTime FechaFirmaUtc { get; private set; }

        public Documento? Documento { get; private set; }
        public Certificado Certificado { get; private set; }

        private FirmaElectronica() { }

        public FirmaElectronica(
            byte[] firmaCms,
            string algoritmo,
            Certificado certificado)
        {
            EstablecerFirmaCms(firmaCms);
            EstablecerAlgoritmo(algoritmo);
            FechaFirmaUtc = DateTime.UtcNow;
            Certificado = certificado
                ?? throw new DomainException("El certificado del firmante es obligatorio.");
            CertificadoId = certificado.Id;
        }

        private void EstablecerFirmaCms(byte[] firmaCms)
        {
            if (firmaCms == null || firmaCms.Length == 0)
                throw new DomainException("La firma CMS no puede estar vacía.");
            FirmaCms = firmaCms;
        }

        private void EstablecerAlgoritmo(string algoritmo)
        {
            if (string.IsNullOrWhiteSpace(algoritmo))
                throw new DomainException("El algoritmo de firma no puede estar vacío.");
            if (algoritmo.Length > 100)
                throw new DomainException("El algoritmo de firma no puede exceder 100 caracteres.");
            Algoritmo = algoritmo.Trim();
        }
    }
}