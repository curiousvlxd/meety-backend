using MediatR;
namespace UseCases.Abstractions;

public class Handlers
{
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>;

    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
        where TCommand : ICommand;
}
