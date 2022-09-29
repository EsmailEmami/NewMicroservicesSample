using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authorization;

public class JwtFactory : IJwtFactory
{
    private readonly JwtBearerOptions _jwtOptions;

    public JwtFactory(IOptions<JwtBearerOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
        ThrowIfInvalidOptions(_jwtOptions);
    }

    public async Task<string> GenerateJwtToken(ClaimsIdentity claimsIdentity)
    {
        claimsIdentity.AddClaims(new Claim[]
        {
            new(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
            new(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
        });

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            Subject = claimsIdentity,
            NotBefore = _jwtOptions.NotBefore,
            Expires = _jwtOptions.Expiration,
            SigningCredentials = _jwtOptions.SigningCredentials,
        });

        return tokenHandler.WriteToken(token);
    }

    private static void ThrowIfInvalidOptions(JwtBearerOptions options)
    {
        if (options == null) throw new ArgumentNullException(nameof(options));

        if (options.ValidFor <= TimeSpan.Zero)
        {
            throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtBearerOptions.ValidFor));
        }

        if (options.SigningCredentials == null)
        {
            throw new ArgumentNullException(nameof(JwtBearerOptions.SigningCredentials));
        }

        if (options.JtiGenerator == null)
        {
            throw new ArgumentNullException(nameof(JwtBearerOptions.JtiGenerator));
        }
    }

    /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() -
                             new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
}