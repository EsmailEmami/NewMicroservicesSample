using User.Application.Core.User.Dtos;

namespace User.Application.Core.User;

public interface IUserService
{
    Task<long> AddUser(CreateUserDto userDto);
}