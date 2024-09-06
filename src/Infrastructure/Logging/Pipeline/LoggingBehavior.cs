using MediatR;
namespace Infrastructure.Logging.Pipeline;

public class LoggingBehavior<TRequest, TResponse>(ILogger logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.Information($"Handling {typeof(TRequest).Name}");
        var response = await next();
        logger.Information($"Handled {typeof(TRequest).Name}");
        return response;
    }
}
