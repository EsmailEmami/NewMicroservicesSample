using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer;

public static class Config
{
    // ------------------------ IDENTITY RESOURCES --------------------------
    public static IEnumerable<IdentityResource> IdentityResources => new[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Address(),
            new IdentityResources.Email(),
            new IdentityResource("Role", "My Role", new List<string>() { "Role" })
        };

    // ------------------------ API RESOURCES --------------------------
    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ("user_api_resource","user api resource")
        };

    // ------------------------ CLIENTS --------------------------
    public static IEnumerable<Client> Clients => new Client[]
           {
               new()
               {
                   ClientId = "user_api_client",
                   ClientName = "user api client",
                   AllowedGrantTypes = GrantTypes.ClientCredentials,
                   RefreshTokenExpiration = TokenExpiration.Absolute,
                   AccessTokenLifetime = 86400,
                   AbsoluteRefreshTokenLifetime = 86400,
                   ClientSecrets = new Secret []
                   {
                       new("secret".Sha256())
                   },
                   AllowedScopes = {"user_api_scope"}
               }
           };

    // ------------------------ SCOPES --------------------------
    public static IEnumerable<ApiScope> ApiScopes =>
       new ApiScope[]
       {
           new ("user_api_scope","user api scope")
       };



    public static List<TestUser> TestUsers =>
        new()
        {
                new TestUser
                {
                    SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                    Username = "Esmail",
                    Password = "123456",
                    Claims = new List<Claim>
                    {
                        new(JwtClaimTypes.GivenName, "Esmail"),
                        new(JwtClaimTypes.FamilyName, "Emami"),
                        new(JwtClaimTypes.PreferredUserName, "EsmailEmami")
                    }
                }
        };
}
