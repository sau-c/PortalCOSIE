using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Domain;
using System.Net;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            var (statusCode, userMessage, logAsError) = ex switch
            {
                DomainException => (StatusCodes.Status400BadRequest, ex.Message, false),
                ApplicationException => (StatusCodes.Status400BadRequest, ex.Message, true),
                DbUpdateException dbEx when dbEx.InnerException is SqlException sqlEx => MapSqlException(sqlEx),
                SqlException sqlEx => MapSqlException(sqlEx),
                _ => (StatusCodes.Status500InternalServerError, $"Error del servidor: {ex.Message}", true)
            };

            if (logAsError)
                _logger.LogError(ex, "Excepción manejada: {Message}", ex.Message);
            else
                _logger.LogWarning(ex, "Excepción de dominio/aplicación: {Message}", ex.Message);

            // DECISIÓN: ¿Es una API/AJAX o es una vista normal?
            if (IsApiRequest(httpContext.Request))
            {
                // Devolver JSON
                await HandleJsonExceptionAsync(httpContext, userMessage, statusCode, ex.GetType().Name);
            }
            else
            {
                // Redirigir a la Vista de Error
                HandleViewException(httpContext, userMessage, statusCode);
            }
        }
    }

    // Método para detectar si la petición espera JSON
    private bool IsApiRequest(HttpRequest request)
    {
        // Verifica si es una llamada AJAX (X-Requested-With) o si el cliente pide explícitamente JSON (Accept)
        // También puede verificar si la ruta empieza con /api/ si usas esa convención
        return request.Headers["X-Requested-With"] == "XMLHttpRequest" ||
               request.Headers["Accept"].ToString().Contains("application/json") ||
               request.Path.StartsWithSegments("/api");
    }

    private static (int statusCode, string message, bool logAsError) MapSqlException(SqlException ex)
    {
        return ex.Number switch
        {
            2627 or 2601 => (StatusCodes.Status409Conflict, "El registro ya existe, revisa los campos unicos.", true),
            547 => (StatusCodes.Status400BadRequest, "No se puede completar la operación porque tiene datos relacionados.", true),
            _ => (StatusCodes.Status500InternalServerError, "Error en la base de datos. Por favor, intente nuevamente.", true)
        };
    }

    private static async Task HandleJsonExceptionAsync(HttpContext context, string userMessage, int statusCode, string exceptionType)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var response = new
        {
            success = false,
            message = userMessage,
            type = exceptionType
        };

        await context.Response.WriteAsJsonAsync(response);
    }

    // Método para manejar redirección a Vistas
    private void HandleViewException(HttpContext context, string message, int statusCode)
    {
        // Usamos WebUtility.UrlEncode para asegurar que el mensaje viaje bien en la URL
        var url = $"/Calendario/Error?message={WebUtility.UrlEncode(message)}&code={statusCode}";

        context.Response.Redirect(url);
    }
}