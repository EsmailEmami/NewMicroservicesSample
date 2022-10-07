using Application.Common;
using Grpc.Core;
using User.Application.Core.User;
using User.Application.Core.User.Dtos;
using User.Grpc.Protos;

namespace User.Grpc.Services;

public class AuthenticationService : AuthenticationProtoService.AuthenticationProtoServiceBase
{
    private readonly IUserService _userService;

    public AuthenticationService(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task<AuthenticatedUserResponse> GetAuthenticatedUser(AuthenticatedUserRequest request, ServerCallContext context)
    {
        var user = await _userService.Login(Mapping.Map<AuthenticatedUserRequest, LoginUserDto>(request));

        return new AuthenticatedUserResponse()
        {
            UserId = user.Id,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
        };
    }

    public override async Task<AuthenticatedUserResponse> GetAuthenticatedUserByUserId(GetAuthenticatedUserByUserIdRequest request, ServerCallContext context)
    {
        var user = await _userService.GetById(request.UserId);

        return new AuthenticatedUserResponse()
        {
            UserId = user.Id,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
        };
    }
}