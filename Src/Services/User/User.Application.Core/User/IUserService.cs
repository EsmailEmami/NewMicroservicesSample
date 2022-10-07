using User.Application.Core.User.Dtos;

namespace User.Application.Core.User;

public interface IUserService
{
    Task<long> AddUser(CreateUserDto userDto);
     Task<Domain.Entities.User> Login(LoginUserDto loginDto);
     Task<Domain.Entities.User> GetById(long userId);
}