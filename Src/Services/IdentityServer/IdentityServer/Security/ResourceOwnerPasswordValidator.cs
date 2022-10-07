using System.Security.Claims;
using IdentityServer.GrpcServices;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using User.Grpc.Protos;

namespace IdentityServer.Security;

public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly AuthenticationGrpcService _authenticationGrpcService;

    public ResourceOwnerPasswordValidator(AuthenticationGrpcService authenticationGrpcService)
    {
        _authenticationGrpcService = authenticationGrpcService;
    }

    public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        try
        {
            //get your user model from db (by username - in my case its email)
            var user = _authenticationGrpcService.GetAuthenticatedUser(new AuthenticatedUserRequest()
            {
                UserName = context.UserName,
                Password = context.Password,
            });

            if (user == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
                return Task.CompletedTask;
            }

            //set the result
            context.Result = new GrantValidationResult(
                subject: user.UserId.ToString(),
                authenticationMethod: "custom",
                claims: GetUserClaims(user));

            return Task.CompletedTask;

        }
        catch
        {
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
            return Task.CompletedTask;
        }
    }

    public static IEnumerable<Claim> GetUserClaims(AuthenticatedUserResponse user)
    {
        return new Claim[]
        {
            new (ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new (ClaimTypes.Name, user.UserName),
            new (ClaimTypes.GivenName,user.FirstName),
            new (ClaimTypes.Surname, user.LastName),
        };
    }
}