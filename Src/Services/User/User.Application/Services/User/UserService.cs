using Application.Common;
using Domain.Commands;
using User.Application.Core.User;
using User.Application.Core.User.Commands;
using User.Application.Core.User.Dtos;

namespace User.Application.Services.User;

public class UserService : IUserService
{
    private readonly ICommandBus _commandBus;

    public UserService(ICommandBus commandBus)
    {
        _commandBus = commandBus;
    }

    public Task<long> AddUser(CreateUserDto userDto)
    {
        var createUserCommand = Mapping.Map<CreateUserDto, CreateUserCommand>(userDto);
        return _commandBus.Send(createUserCommand);
    }
}