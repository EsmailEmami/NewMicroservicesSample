using Application.Common;
using Application.Security;
using Domain.Commands;
using Domain.Queries;
using User.Application.Core.Identity.Query;
using User.Application.Core.User;
using User.Application.Core.User.Commands;
using User.Application.Core.User.Dtos;
using User.Application.Core.User.Queries;

namespace User.Application.Services.User;

public class UserService : IUserService
{
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(ICommandBus commandBus, IQueryBus queryBus, IPasswordHasher passwordHasher)
    {
        _commandBus = commandBus;
        _queryBus = queryBus;
        _passwordHasher = passwordHasher;
    }

    public Task<long> AddUser(CreateUserDto userDto)
    {
        userDto.Password = _passwordHasher.HashPassword(userDto.Password);
        var createUserCommand = Mapping.Map<CreateUserDto, CreateUserCommand>(userDto);
        return _commandBus.Send(createUserCommand);
    }

    public async Task<Domain.Entities.User> Login(LoginUserDto loginDto)
    {
        var query = Mapping.Map<LoginUserDto, GetUserByUserNameQuery>(loginDto);
        var user = await _queryBus.Send(query);

        if (user.Identiy == null)
            user.SetIdentity(await _queryBus.Send(new GetIdentityQuery(user.IdentityId)));

        var passwordCheck = _passwordHasher.VerifyHashedPassword(user.Identiy.Password, loginDto.Password);

        if (!passwordCheck)
            throw new ApplicationException("کاربر یافت نشد.");

        return user;
    }

    public async Task<Domain.Entities.User> GetById(long userId)
    {
        var query = new GetUserByUserIdQuery(userId);
        var user = await _queryBus.Send(query);
        return user;
    }
}