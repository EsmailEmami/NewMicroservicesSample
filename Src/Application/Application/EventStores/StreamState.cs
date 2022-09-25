using Domain.Core.Entities;

namespace Application.EventStores;

public class StreamState : Entity<Guid>
{
    public StreamState()
    {
        Id = Guid.NewGuid();
    }

    public Guid AggregateId { get; set; }
    public DateTime CreatedUtc { get; } = DateTime.UtcNow;
    public string Type { get; set; }
    public string Data { get; set; }
    public int Version { get; set; } = 0;
}