using System.Security.Claims;
using Application.Extensions;
using Domain.Identity;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Authorization;

public class AspNetUser : IUser
{
    private readonly IHttpContextAccessor _accessor;
    public AspNetUser(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public long UserId => _accessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value.Parse<long>() ?? 0;

    public bool IsAuthenticated() => _accessor.HttpContext!.User.Identity!.IsAuthenticated;
}