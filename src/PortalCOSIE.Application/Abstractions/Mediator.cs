using PortalCOSIE.Application.Abstractions;

public class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var requestType = request.GetType();

        // Construimos el tipo genérico: IRequestHandler<TRequest, TResponse>
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));

        // Resolvemos la instancia desde el DI Container
        var handler = _serviceProvider.GetService(handlerType);

        if (handler == null)
        {
            throw new InvalidOperationException($"No se encontró un manejador registrado para {requestType.Name}");
        }

        // Invocamos el método 'Handle' usando Reflexión
        // Nota: MediatR original usa wrappers para optimizar esto, pero esto es funcional e idéntico en lógica.
        var method = handlerType.GetMethod("Handle");

        var task = (Task<TResponse>)method.Invoke(handler, new object[] { request });

        return await task;
    }
}