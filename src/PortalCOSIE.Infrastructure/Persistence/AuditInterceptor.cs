using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using PortalCOSIE.Domain.Entities.EntradaBitacoras;
using System.Security.Claims;
using System.Text.Json;

namespace PortalCOSIE.Infrastructure.Persistence;

public class AuditInterceptor(IHttpContextAccessor httpContextAccessor) : SaveChangesInterceptor
{
    private readonly List<Pendiente> _pendientes = [];
    private bool _guardandoBitacora;

    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        WriteIndented = false,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null && !_guardandoBitacora)
            Capturar(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null && !_guardandoBitacora)
            await GuardarPendientesAsync(eventData.Context, cancellationToken);

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private void Capturar(DbContext context)
    {
        if (httpContextAccessor.HttpContext is null)
            return;

        foreach (var entrada in context.ChangeTracker.Entries()
            .Where(e => e.Entity is not EntradaBitacora
                     && e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
            .ToList())
        {
            var cambios = entrada.State switch
            {
                EntityState.Added => Valores(entrada.CurrentValues),
                EntityState.Modified => Cambios(entrada),
                EntityState.Deleted => Valores(entrada.OriginalValues),
                _ => []
            };

            if (cambios.Count == 0)
                continue;

            var accion = entrada.State switch
            {
                EntityState.Added => "Crear",
                EntityState.Modified => "Editar",
                _ => "Eliminar"
            };

            _pendientes.Add(new Pendiente(
                accion,
                entrada.Entity.GetType().Name,
                entrada.State == EntityState.Added ? entrada.Entity : null,
                entrada.State == EntityState.Added ? null : ObtenerId(entrada),
                cambios));
        }
    }

    private async Task GuardarPendientesAsync(DbContext context, CancellationToken ct)
    {
        if (_pendientes.Count == 0)
            return;

        var http = httpContextAccessor.HttpContext;
        var pendientes = _pendientes.ToList();
        _pendientes.Clear();

        if (http is null)
            return;

        var userId = http.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
            userId = null;
        var ip = http.Connection.RemoteIpAddress?.ToString() ?? "N/A";
        var ua = http.Request.Headers["User-Agent"].ToString();

        var registros = new List<EntradaBitacora>();

        foreach (var p in pendientes)
        {
            var id = p.Id ?? ObtenerId(context.Entry(p.Entity!));
            if (!IdValido(id))
                continue;

            registros.Add(new EntradaBitacora(
                p.Accion, p.Entidad, id!, JsonSerializer.Serialize(p.Cambios, JsonOpts),
                userId, ip, ua));
        }

        if (registros.Count == 0)
            return;

        context.Set<EntradaBitacora>().AddRange(registros);

        _guardandoBitacora = true;
        try
        {
            await context.SaveChangesAsync(ct);
        }
        finally
        {
            _guardandoBitacora = false;
        }
    }

    private static string? ObtenerId(EntityEntry entrada)
    {
        var clave = entrada.Metadata.FindPrimaryKey()?.Properties.FirstOrDefault();
        if (clave is null)
            return null;

        var valor = entrada.State == EntityState.Deleted
            ? entrada.OriginalValues[clave]
            : entrada.CurrentValues[clave];

        return valor?.ToString();
    }

    private static bool IdValido(string? id)
        => !string.IsNullOrWhiteSpace(id)
        && (!int.TryParse(id, out var n) || n > 0);

    private static Dictionary<string, object?> Valores(PropertyValues valores)
        => valores.Properties
            .Where(DebeAuditar)
            .ToDictionary(p => p.Name, p => Formatear(valores[p]));

    private static Dictionary<string, object?> Cambios(EntityEntry entrada)
    {
        var cambios = new Dictionary<string, object?>();

        foreach (var prop in entrada.OriginalValues.Properties.Where(DebeAuditar))
        {
            var anterior = entrada.OriginalValues[prop];
            var actual = entrada.CurrentValues[prop];
            if (!Equals(anterior, actual))
                cambios[prop.Name] = Formatear(actual);
        }

        return cambios;
    }

    private static bool DebeAuditar(IProperty prop)
        => !prop.IsShadowProperty()
        && !prop.IsPrimaryKey()
        && !EsSensible(prop.Name);

    private static bool EsSensible(string nombre)
        => nombre.Contains("Password", StringComparison.OrdinalIgnoreCase)
        || nombre.Contains("Contrasena", StringComparison.OrdinalIgnoreCase)
        || nombre.Contains("Token", StringComparison.OrdinalIgnoreCase)
        || nombre.Contains("Secret", StringComparison.OrdinalIgnoreCase)
        || nombre.Contains("Firma", StringComparison.OrdinalIgnoreCase)
        || nombre.Contains("Certificado", StringComparison.OrdinalIgnoreCase)
        || nombre.Contains("Hash", StringComparison.OrdinalIgnoreCase);

    private static object? Formatear(object? valor) => valor switch
    {
        null => null,
        byte[] => "[oculto]",
        DateTime d => d.ToString("yyyy-MM-dd HH:mm:ss"),
        DateTimeOffset d => d.ToString("yyyy-MM-dd HH:mm:ss"),
        string s when s.Length > 500 => s[..500] + "…",
        _ => valor
    };

    private sealed record Pendiente(
        string Accion,
        string Entidad,
        object? Entity,
        string? Id,
        Dictionary<string, object?> Cambios);
}