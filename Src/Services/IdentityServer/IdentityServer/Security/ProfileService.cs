using System.Security.Claims;
using IdentityServer.GrpcServices;
using IdentityServer4.Models;
using IdentityServer4.Services;
using User.Grpc.Protos;

namespace IdentityServer.Security;

public class ProfileService : IProfileService
{
    private readonly AuthenticationGrpcService _authenticationGrpcService;
    private readonly ILogger<ProfileService> _logger;

    public ProfileService(AuthenticationGrpcService authenticationGrpcService, ILogger<ProfileService> logger)
    {
        _authenticationGrpcService = authenticationGrpcService;
        _logger = logger;
    }

    public Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        try
        {
            long.TryParse(context.Subject.FindFirstValue(ClaimTypes.NameIdentifier), out long userId);
            if (userId == 0) return Task.CompletedTask;

            var user = _authenticationGrpcService.GetAuthenticatedUserByUserId(
                new GetAuthenticatedUserByUserIdRequest()
                {
                    UserId = userId
                });

            if (user == null) return Task.CompletedTask;

            var claims = ResourceOwnerPasswordValidator.GetUserClaims(user);

            //set issued claims to return
            context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();

            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message} {ex.InnerException?.Message}");
            return Task.CompletedTask;
        }
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        try
        {
            long.TryParse(context.Subject.FindFirstValue(ClaimTypes.NameIdentifier), out long userId);
            if (userId == 0)
            {
                context.IsActive = false;
                return Task.CompletedTask;
            }

            var user = _authenticationGrpcService.GetAuthenticatedUserByUserId(
                new GetAuthenticatedUserByUserIdRequest()
                {
                    UserId = userId
                });

            if (user == null)
            {
                context.IsActive = false;
                return Task.CompletedTask;
            }

            context.IsActive = true;
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message} {ex.InnerException?.Message}");
            context.IsActive = false;
            return Task.CompletedTask;
        }
    }
}