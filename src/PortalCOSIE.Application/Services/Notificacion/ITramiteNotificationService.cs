using PortalCOSIE.Domain.Entities.Tramites;

namespace PortalCOSIE.Application.Services.Notificacion
{
    public interface ITramiteNotificationService
    {
        Task NotificarSiCambioAsync(Tramite tramite, int estadoAnteriorId, string? observaciones = null);
    }
}