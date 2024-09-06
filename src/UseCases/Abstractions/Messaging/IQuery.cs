using MediatR;
namespace UseCases.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
