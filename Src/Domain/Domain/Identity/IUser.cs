namespace Domain.Identity;

public interface IUser
{
    long UserId { get; }
    bool IsAuthenticated();
}