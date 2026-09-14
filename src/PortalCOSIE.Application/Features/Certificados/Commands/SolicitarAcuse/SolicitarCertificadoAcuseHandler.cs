using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Application.Features.Certificados.Commands.SolicitarAcuse;

public class SolicitarCertificadoAcuseHandler : IRequestHandler<SolicitarCertificadoAcuseCommand, Result<string>>
{
    private readonly ICertificadoAdminService _certificados;

    public SolicitarCertificadoAcuseHandler(ICertificadoAdminService certificados)
        => _certificados = certificados;

    public async Task<Result<string>> Handle(SolicitarCertificadoAcuseCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.CsrBase64))
            return Result<string>.Failure("La solicitud CSR es obligatoria.");

        byte[] csrDer;
        try
        {
            csrDer = Convert.FromBase64String(command.CsrBase64);
        }
        catch
        {
            return Result<string>.Failure("El formato de la CSR no es válido.");
        }

        return await _certificados.EmitirAcuseDesdeCsrAsync(command.IdentityUserId, csrDer);
    }
}