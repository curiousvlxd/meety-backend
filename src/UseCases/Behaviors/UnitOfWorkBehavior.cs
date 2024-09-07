using Infrastructure.Database.Abstractions;
using MediatR;
namespace UseCases.Behaviors;

public sealed class UnitOfWorkBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (IsNotCommand()) return await next();
      

        var response = await next();
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return response;
    }

    private static bool IsNotCommand() => !typeof(TRequest).Name.EndsWith("Command");
}
