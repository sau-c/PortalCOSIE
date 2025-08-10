using PortalCOSIE.Application.DTO.Cuenta;

namespace PortalCOSIE.Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result<string>> IngresarUsuarioAsync(IngresarDTO dto);
        Task CerrarSesionAsync();
    }
}
