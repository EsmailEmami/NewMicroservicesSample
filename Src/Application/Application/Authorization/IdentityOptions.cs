namespace Application.Authorization;

public class IdentityOptions
{
    public string DefaultScheme { get; set; }
    public string? DefaultAuthenticateScheme { get; set; }
    public string? DefaultChallengeScheme { get; set; }
    public TokenParameters TokenParameters { get; set; } = new();
    public IEnumerable<string>? ValidEventPaths { get; set; }

    public bool RequireHttpsMetadata { get; set; } = false;
    public string? Authority { get; set; }
    public string? ClaimsIssuer { get; set; }
    public string Audience { get; set; }
    public bool SaveToken { get; set; } = true;

    public IEnumerable<IdentityPolicy> Policies { get; set; }
}

public class TokenParameters
{
    public bool ValidateIssuer { get; set; }
    public IEnumerable<string>? ValidIssuers { get; set; }
    public bool ValidateAudience { get; set; }
    public IEnumerable<string>? ValidAudiences { get; set; }
    public bool RequireExpirationTime { get; set; } = false;
    public bool ValidateLifetime { get; set; } = true;
    public TimeSpan ClockSkew { get; set; } = TimeSpan.Zero;
}

public class IdentityPolicy
{
    public string Name { get; set; }
    public string ClaimType { get; set; }
    public string[] Values { get; set; }
}