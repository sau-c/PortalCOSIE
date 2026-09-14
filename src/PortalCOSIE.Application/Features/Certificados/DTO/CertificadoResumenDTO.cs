namespace PortalCOSIE.Application.Features.Certificados.DTO;

public sealed record CertificadoResumenDTO(
    string Thumbprint,
    string Sujeto,
    string NumeroSerie,
    DateTime VigenteDesde,
    DateTime VigenteHasta,
    bool Vigente,
    bool PuedeEmitir);