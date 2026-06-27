using PortalCOSIE.Application.Services.Notificacion;
using PortalCOSIE.Domain.Entities.Tramites;

namespace PortalCOSIE.Application.Notifications
{
    public readonly struct TramiteEstadoSnapshot
    {
        private readonly int _estadoAnteriorId;

        private TramiteEstadoSnapshot(int estadoAnteriorId) => _estadoAnteriorId = estadoAnteriorId;

        public static TramiteEstadoSnapshot Desde(Tramite tramite) => new(tramite.EstadoTramiteId);

        public static TramiteEstadoSnapshot Inicial() => new(0);

        public Task NotificarSiCambioAsync(
            ITramiteNotificationService notificaciones,
            Tramite tramite,
            string? observaciones = null)
            => notificaciones.NotificarSiCambioAsync(tramite, _estadoAnteriorId, observaciones);
    }
}