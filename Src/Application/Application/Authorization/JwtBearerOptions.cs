using Microsoft.IdentityModel.Tokens;

namespace Application.Authorization;

public class JwtBearerOptions
{
    public string SecretKey { get; set; }
    public string ProviderKey { get; set; }
    public bool ValidateIssuer { get; set; } = true;
    public string Issuer { get; set; }
    public string Subject { get; set; }
    public bool ValidateAudience { get; set; } = true;
    public string Audience { get; set; }

    public string[] RoutesToValidate { get; set; } = Array.Empty<string>();
    public string[] OriginAccess { get; set; } = Array.Empty<string>();
    public string CorsName { get; set; }

    public DateTime Expiration => IssuedAt.Add(ValidFor);
    public DateTime NotBefore => DateTime.UtcNow;
    public DateTime IssuedAt => DateTime.UtcNow;
    public TimeSpan ValidFor { get; set; } = TimeSpan.FromDays(30);
    public Func<Task<string>> JtiGenerator => () => Task.FromResult(Guid.NewGuid().ToString());
    public SigningCredentials SigningCredentials { get; set; }
}
