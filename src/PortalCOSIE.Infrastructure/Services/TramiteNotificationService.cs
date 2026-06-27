using PortalCOSIE.Application.Services.Notificacion;
using PortalCOSIE.Application.Services.Query;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.SharedKernel;

namespace PortalCOSIE.Infrastructure.Services
{
    public class TramiteNotificationService : ITramiteNotificationService
    {
        private readonly IUsuarioQueryService _usuarioQuery;
        private readonly INotificationService _email;

        public TramiteNotificationService(IUsuarioQueryService usuarioQuery, INotificationService email)
        {
            _usuarioQuery = usuarioQuery;
            _email = email;
        }

        public async Task NotificarSiCambioAsync(Tramite tramite, int estadoAnteriorId, string? observaciones = null)
        {
            if (tramite.EstadoTramiteId == estadoAnteriorId)
                return;

            var contacto = await _usuarioQuery.ObtenerContactoAlumnoPorId(tramite.AlumnoId);
            if (contacto is null)
                return;

            var estado = Enumeration.FromValue<EstadoTramite>(tramite.EstadoTramiteId);

            await _email.EnviarAsync(
                contacto.Email,
                $"Actualización de trámite #{tramite.Id} - {estado.Nombre}",
                plantillas => plantillas.EstadoTramiteCambiado(
                    tramite.Id,
                    estado.Nombre,
                    contacto.NombreCompleto,
                    observaciones));
        }
    }
}