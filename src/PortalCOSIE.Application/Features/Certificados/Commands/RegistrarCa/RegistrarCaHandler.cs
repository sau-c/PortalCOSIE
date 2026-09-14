using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Application.Features.Certificados.Commands.RegistrarCa;

public class RegistrarCaHandler : IRequestHandler<RegistrarCaCommand, Result<string>>
{
    private readonly ICertificadoCaService _certificadoCa;

    public RegistrarCaHandler(ICertificadoCaService certificadoCa)
        => _certificadoCa = certificadoCa;

    public Task<Result<string>> Handle(RegistrarCaCommand command)
        => _certificadoCa.RegistrarCaAsync(command.CerStream);
}