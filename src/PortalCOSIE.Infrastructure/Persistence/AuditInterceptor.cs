using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PortalCOSIE.Domain.Entities.EntradaBitacoras;
using System.Security.Claims;
using System.Text.Json;

namespace PortalCOSIE.Infrastructure.Persistence
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context != null)
            {
                AuditarCambios(eventData.Context);
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void AuditarCambios(DbContext context)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return;
            var userId = httpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var ipAddress = httpContext?.Connection?.RemoteIpAddress?.ToString() ?? "N/A";
            var userAgent = httpContext?.Request?.Headers["User-Agent"].ToString() ?? "N/A";

            var entradas = context.ChangeTracker.Entries()
                    .Where(e => e.Entity is not EntradaBitacora && // No auditar la bitácora misma
                               e.State != EntityState.Unchanged &&
                               e.State != EntityState.Detached)
                    .ToList();

            foreach (var entrada in entradas)
            {
                var bitacora = CrearRegistroBitacora(entrada, userId, ipAddress, userAgent);
                if (bitacora != null)
                {
                    context.Set<EntradaBitacora>().Add(bitacora);
                }
            }
        }

        private EntradaBitacora? CrearRegistroBitacora(
            EntityEntry entrada,
            string userId,
            string ipAddress,
            string userAgent)
        {
            var entidadNombre = entrada.Entity.GetType().Name;
            var entidadId = ObtenerEntidadId(entrada);

            if (string.IsNullOrEmpty(entidadId))
                return null;

            string accion = entrada.State switch
            {
                EntityState.Added => "Crear",
                EntityState.Modified => "Editar",
                EntityState.Deleted => "Eliminar",
                _ => "Desconocido"
            };

            //string? valorAnterior = null;
            string? valorNuevo = null;

            if (entrada.State == EntityState.Modified)
            {
                var cambios = ObtenerCambios(entrada);
                if (cambios.Any())
                {
                    //valorAnterior = JsonSerializer.Serialize(
                    //    cambios.ToDictionary(c => c.Propiedad, c => c.ValorAnterior),
                    //    new JsonSerializerOptions { WriteIndented = false }
                    //);
                    valorNuevo = JsonSerializer.Serialize(
                        cambios.ToDictionary(c => c.Propiedad, c => c.ValorNuevo),
                        new JsonSerializerOptions { WriteIndented = false }
                    );
                }
            }
            else if (entrada.State == EntityState.Added)
            {
                var propiedades = entrada.CurrentValues.Properties
                    .Where(p => !p.IsShadowProperty()) // Excluir propiedades shadow
                    .ToDictionary(p => p.Name, p => entrada.CurrentValues[p]);

                valorNuevo = JsonSerializer.Serialize(propiedades,
                    new JsonSerializerOptions { WriteIndented = false });
            }
            else if (entrada.State == EntityState.Deleted)
            {
                var propiedades = entrada.OriginalValues.Properties
                    .Where(p => !p.IsShadowProperty())
                    .ToDictionary(p => p.Name, p => entrada.OriginalValues[p]);

                valorNuevo = JsonSerializer.Serialize(propiedades,
                    new JsonSerializerOptions { WriteIndented = false });
            }

            return new EntradaBitacora(
                accion,
                entidadNombre,
                entidadId,
                valorNuevo,
                userId,
                ipAddress,
                userAgent
            );
        }

        private string? ObtenerEntidadId(EntityEntry entrada)
        {
            var keyProperty = entrada.Metadata.FindPrimaryKey()?.Properties.FirstOrDefault();
            if (keyProperty == null)
                return string.Empty;

            var keyValue = entrada.Property(keyProperty.Name).CurrentValue;
            return keyValue?.ToString();
        }

        private List<(string Propiedad, /*object? ValorAnterior,*/ object? ValorNuevo)> ObtenerCambios(EntityEntry entrada)
        {
            var cambios = new List<(string, /*object?,*/ object?)>();

            foreach (var propiedad in entrada.OriginalValues.Properties)
            {
                // Ignorar propiedades de navegación y shadow properties
                //if (propiedad.IsShadowProperty() || propiedad.IsForeignKey())
                //    continue;

                var valorOriginal = entrada.OriginalValues[propiedad];
                var valorActual = entrada.CurrentValues[propiedad];

                if (!Equals(valorOriginal, valorActual))
                {
                    cambios.Add((propiedad.Name,/* valorOriginal,*/ valorActual));
                }
            }

            return cambios;
        }
    }
}