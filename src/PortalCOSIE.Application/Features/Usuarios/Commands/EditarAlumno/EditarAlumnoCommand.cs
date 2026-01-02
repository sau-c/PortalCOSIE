using PortalCOSIE.Application.Abstractions;

namespace PortalCOSIE.Application.Features.Usuarios.Commands.EditarAlumno
{
    public sealed record EditarAlumnoCommand(
        string IdentityUserId,
        string Nombre,
        string ApellidoPaterno,
        string ApellidoMaterno,
        string PeriodoIngreso,
        string NumeroBoleta,
        int CarreraId
        ) : IRequest<Result<string>>;
}
