using PortalCOSIE.Application.Abstractions;

public interface IMediator
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
}
