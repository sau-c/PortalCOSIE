using PortalCOSIE.Domain;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException ex)
        {
            _logger.LogWarning(ex, "Domain exception occurred");
            await HandleDomainExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleDomainExceptionAsync(HttpContext context, DomainException ex)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;

        if (IsAjaxRequest(context.Request))
        {
            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
        }
        else
        {
            context.Items["AlertType"] = "error";
            context.Items["AlertMessage"] = ex.Message;
            context.Response.Redirect(context.Request.Path);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        if (IsAjaxRequest(context.Request))
        {
            await context.Response.WriteAsJsonAsync(new { error = "Ocurrió un error interno" });
        }
        else
        {
            context.Items["AlertType"] = "error";
            context.Items["AlertMessage"] = "Ocurrió un error interno";
            context.Response.Redirect(context.Request.Path);
        }
    }

    private static bool IsAjaxRequest(HttpRequest request)
    {
        return request.Headers["X-Requested-With"] == "XMLHttpRequest";
    }
}

// Extensión para registrar el middleware
public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}