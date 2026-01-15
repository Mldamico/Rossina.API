namespace Rossina.Modules.Catalog.Domain.Abstractions;

public interface IDomainEvent
{
    Guid Id { get; }
    DateTime OccurredAt { get; }
}