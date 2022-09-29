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

    public Guid UserId => _accessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value.ToGuid();

    public bool IsAuthenticated() => _accessor.HttpContext!.User.Identity!.IsAuthenticated;
}