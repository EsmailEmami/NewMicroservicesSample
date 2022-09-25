using Domain.Core.Entities;

namespace Application.Outbox;

public class OutboxMessage : Entity<Guid>
{
    public OutboxMessage(Guid id)
    {
        this.Id = id;
    }
    public OutboxMessage()
    {
        Id = Guid.NewGuid();
    }

    public DateTime CreatedUtc { get; } = DateTime.UtcNow;
    public string Type { get; private set; }
    public string Data { get; private set; }
    public DateTime? Processed { get; private set; }

    public void CreateMessage(string type, string data, DateTime? processed = null)
    {
        Type = type;
        Data = data;
        Processed = processed;
    }

    public void SetProcessed(DateTime processedUtc)
    {
        Processed = processedUtc;
    }
}