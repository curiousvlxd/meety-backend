using System.Runtime.Serialization;
using System.Text.Json.Serialization;
namespace Domain.Primitives;

public abstract class Entity
{
    private readonly IList<DomainEvent> _domainEvents = [];
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    [IgnoreDataMember] [JsonIgnore] public IEnumerable<DomainEvent> DomainEvents => _domainEvents;

    protected void Raise(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
