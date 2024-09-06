using MediatR;
namespace Domain.Primitives;

public sealed record DomainEvent(Guid Id) : INotification;
