using Microsoft.Data.SqlClient;
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
        catch (DomainException ex)
        {
            _logger.LogWarning(ex, "Excepción de dominio: {Message}", ex.Message);
            await HandleExceptionAsync(httpContext, ex, StatusCodes.Status400BadRequest);
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, "Excepción de aplicación: {Message}", ex.Message);
            await HandleExceptionAsync(httpContext, ex, StatusCodes.Status400BadRequest);
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "Error de SQL: {Message}", ex.Message);
            var userMessage = ex.Number switch
            {
                2627 or 2601 => "El registro ya existe en el sistema.",
                547 => "No se puede eliminar el registro porque tiene datos relacionados.",
                _ => "Error en la base de datos. Por favor, intente nuevamente."
            };
            await HandleExceptionAsync(httpContext, new ApplicationException(userMessage), StatusCodes.Status400BadRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Excepción no controlada: {Message}", ex.Message);
            await HandleExceptionAsync(httpContext, ex, StatusCodes.Status500InternalServerError);
        }
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

//public class GlobalExceptionHandlingMiddleware
//{
//    private readonly RequestDelegate _next;
//    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

//    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
//    {
//        _next = next;
//        _logger = logger;
//    }

//    public async Task InvokeAsync(HttpContext httpContext)
//    {
//        try
//        {
//            await _next(httpContext);
//        }
//        catch (DomainException ex)
//        {
//            _logger.LogError(ex, "Se ha producido una excepción específica de dominio.");
//            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest; // Adjusted status code for domain-specific errors
//            httpContext.Response.ContentType = "application/json";
//            await httpContext.Response.WriteAsJsonAsync(new { message = ex.Message });
//        }
//        catch (ApplicationException ex)
//        {
//            _logger.LogError(ex, "Se ha producido una excepción específica de aplicación.");
//            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError; // Adjusted status code for domain-specific errors
//            httpContext.Response.ContentType = "application/json";
//            await httpContext.Response.WriteAsJsonAsync(new { message = ex.Message });
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "Se ha producido una excepción no controlada.");
//            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
//            httpContext.Response.ContentType = "application/json";
//            await httpContext.Response.WriteAsJsonAsync(new { message = "Ocurrió un error inesperado sin manejar." });
//        }
//    }
//}
