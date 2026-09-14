using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Application.Features.Usuarios.Commands.ToggleAlumnoRol;

public class ToggleAlumnoRolHandler : IRequestHandler<ToggleAlumnoRolCommand, Result<string>>
{
    private readonly ISecurityService _securityService;
    private readonly ICertificadoAlumnoService _certificadoAlumnoService;

    public ToggleAlumnoRolHandler(
        ISecurityService securityService,
        ICertificadoAlumnoService certificadoAlumnoService)
    {
        _securityService = securityService;
        _certificadoAlumnoService = certificadoAlumnoService;
    }

    public async Task<Result<string>> Handle(ToggleAlumnoRolCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.UserId))
            return Result<string>.Failure("ID de usuario no válido.");
        if (command.Rol != "Alumno")
            return Result<string>.Failure("Esta operación solo aplica al rol Alumno.");

        var tieneRol = await _securityService.TieneRolAsync(command.UserId, command.Rol);

        if (tieneRol)
        {
            var remover = await _certificadoAlumnoService.RemoverCertificadoAsync(command.UserId);
            if (!remover.Succeeded)
                return remover;

            return await _securityService.ToggleRol(command.UserId, command.Rol);
        }

        return await _securityService.ToggleRol(command.UserId, command.Rol);
    }
}