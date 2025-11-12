using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Domain;

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
                _ => (StatusCodes.Status500InternalServerError, "Error interno del servidor", true) // Esta maneja las NO controladas
            };

            if (logAsError)
            {
                _logger.LogError(ex, "Excepción manejada: {Message}", ex.Message);
            }
            else
            {
                _logger.LogWarning(ex, "Excepción de dominio/aplicación: {Message}", ex.Message);
            }

            await HandleExceptionAsync(httpContext, ex, statusCode);
        }
    }

    private static (int statusCode, string message, bool logAsError) MapSqlException(SqlException ex)
    {
        return ex.Number switch
        {
            2627 or 2601 => (StatusCodes.Status409Conflict, "El registro ya existe en el sistema, revisas los indices unicos.", true),
            547 => (StatusCodes.Status400BadRequest, "No se puede completar la operación porque tiene datos relacionados.", true),
            _ => (StatusCodes.Status500InternalServerError, "Error en la base de datos. Por favor, intente nuevamente.", true)
        };
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var response = new
        {
            success = false,
            message = exception.Message,
            type = exception.GetType().Name
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}
