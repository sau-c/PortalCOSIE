
namespace PortalCOSIE.Application.Interfaces
{
    public interface ICuentaCorreoService
    {
        Task<Result<string>> ConfirmarCorreoAsync(string correo, string token);
        Task<Result<string>> VerificarCorreoAsync(string userId, string correo);
        Task<Result<string>> ActualizarCorreoAsync(string id, string correo, string token);
    }
}
