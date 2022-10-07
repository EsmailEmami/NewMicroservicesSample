using System.Security.Claims;
using Application.Authorization;
using Application.Common;
using Application.Security;
using AutoMapper;
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
    private readonly IMapper _mapper;
    private readonly IJwtFactory _jwtFactory;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(ICommandBus commandBus, IMapper mapper, IPasswordHasher passwordHasher, IQueryBus queryBus, IJwtFactory jwtFactory)
    {
        _commandBus = commandBus;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _queryBus = queryBus;
        _jwtFactory = jwtFactory;
    }

    public Task<long> AddUser(CreateUserDto userDto)
    {
        userDto.Password = _passwordHasher.HashPassword(userDto.Password);
        var createUserCommand = Mapping.Map<CreateUserDto, CreateUserCommand>(userDto);
        return _commandBus.Send(createUserCommand);
    }

    public async Task<string> Login(LoginUserDto loginDto)
    {
        var query = Mapping.Map<LoginUserDto, GetUserByUserNameQuery>(loginDto);
        var user = await _queryBus.Send(query);

        if (user.Identiy == null)
            user.SetIdentity(await _queryBus.Send(new GetIdentityQuery(user.IdentityId)));

        var passwordCheck = _passwordHasher.VerifyHashedPassword(user.Identiy.Password, loginDto.Password);

        if (!passwordCheck)
            throw new ApplicationException("کاربر یافت نشد.");

        return await GenerateToken(user);
    }

    private async Task<string> GenerateToken(Domain.Entities.User user)
    {
        // Init ClaimsIdentity
        var claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

        // Generate access token
        var jwtToken = await _jwtFactory.GenerateJwtToken(claimsIdentity);

        return jwtToken;
    }
}