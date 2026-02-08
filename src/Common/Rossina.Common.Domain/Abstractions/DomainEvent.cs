
namespace Rossina.Common.Domain.Abstractions;

public abstract class DomainEvent : IDomainEvent
{
    public Guid Id { get; init; } 
    public DateTime OccurredAt { get; init; }

    protected DomainEvent()
    {
        Id = Guid.NewGuid();
        OccurredAt = DateTime.UtcNow;
    }

    protected DomainEvent(Guid id, DateTime occurredAt)
    {
        Id = id;
        OccurredAt = occurredAt;
    }
}