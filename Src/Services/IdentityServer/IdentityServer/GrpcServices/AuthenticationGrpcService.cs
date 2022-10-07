using User.Grpc.Protos;

namespace IdentityServer.GrpcServices;

public class AuthenticationGrpcService
{
    private readonly AuthenticationProtoService.AuthenticationProtoServiceClient _authenticationProtoServiceClient;

    public AuthenticationGrpcService(AuthenticationProtoService.AuthenticationProtoServiceClient authenticationProtoServiceClient)
    {
        _authenticationProtoServiceClient = authenticationProtoServiceClient ?? throw new ArgumentNullException(nameof(authenticationProtoServiceClient));
    }

    public AuthenticatedUserResponse GetAuthenticatedUser(AuthenticatedUserRequest request)
    {
        return _authenticationProtoServiceClient.GetAuthenticatedUser(request);
    }

    public AuthenticatedUserResponse GetAuthenticatedUserByUserId(GetAuthenticatedUserByUserIdRequest request)
    {
        return _authenticationProtoServiceClient.GetAuthenticatedUserByUserId(request);
    }
}