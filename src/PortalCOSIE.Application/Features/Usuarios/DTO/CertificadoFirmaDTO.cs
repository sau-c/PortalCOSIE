namespace PortalCOSIE.Application.Features.Usuarios.DTO
{
    public sealed record CertificadoFirmaDTO(
        string Thumbprint,
        string Sujeto,
        DateTime VigenteDesde,
        DateTime VigenteHasta,
        string CertificadoDerBase64,
        bool EsValidoParaFirma);
}