namespace PortalCOSIE.Application.Services
{
    public interface ICertificadoAlumnoService
    {
        Task<Result<string>> AsignarCertificadoAsync(string identityUserId, Stream certificadoCer);
        Task<Result<string>> RemoverCertificadoAsync(string identityUserId);
    }
}