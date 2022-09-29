using System.Security.Claims;

namespace Application.Authorization;

public interface IJwtFactory
{
    Task<string> GenerateJwtToken(ClaimsIdentity claimsIdentity);
}