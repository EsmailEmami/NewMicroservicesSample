namespace Domain.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException()
    {
    }
    public EntityNotFoundException(string message) : base(message)
    {
    }
    public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
    public EntityNotFoundException(Type entityType, object id)
           : this(entityType, id, null)
    {
    }
    public EntityNotFoundException(Type entityType, object id, Exception? innerException)
        : base($"There is no such an entity. Entity type: {entityType.FullName}, id: {id}", innerException)
    {
    }
}

