namespace Domain.Identity;

public interface IUser
{
    Guid UserId { get; }
    bool IsAuthenticated();
}