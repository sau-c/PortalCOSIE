using PortalCOSIE.Application.Abstractions;

namespace PortalCOSIE.Application.Features.Usuarios.Commands.RegistrarAlumno
{
    public sealed record RegistrarAlumnoCommand(
        string IdentityUserId,
        string Nombre,
        string ApellidoPaterno,
        string ApellidoMaterno,
        string NumeroBoleta,
        string PeriodoIngreso,
        int CarreraId
        //int PlanEstudioId
        ) : IRequest<Result<string>>;
}
