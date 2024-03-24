namespace TaskManagement.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; private init; }
}