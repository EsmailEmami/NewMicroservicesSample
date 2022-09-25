namespace Domain.Core.Entities;

public interface IEntity<TPrimaryKey>
{
    TPrimaryKey Id { get; set; }
    DateTime CreateDate { get; }
    bool Deleted { get; set; }

    bool IsTransient();
}

