using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Application.Features.Certificados.Commands.SolicitarAlumno;

public class SolicitarCertificadoAlumnoHandler : IRequestHandler<SolicitarCertificadoAlumnoCommand, Result<string>>
{
    private readonly ICertificadoAlumnoService _certificados;

    public SolicitarCertificadoAlumnoHandler(ICertificadoAlumnoService certificados)
        => _certificados = certificados;

    public async Task<Result<string>> Handle(SolicitarCertificadoAlumnoCommand command)
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

        return await _certificados.EmitirDesdeCsrAsync(command.IdentityUserId, csrDer);
    }
}