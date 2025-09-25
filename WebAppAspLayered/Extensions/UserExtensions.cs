﻿using System.Security.Claims;
using System.Security.Principal;
using WebAppAspLayered.DL.Enums;

namespace WebAppAspLayered.Extensions;

public static class UserExtensions
{
    public static int GetId(this ClaimsPrincipal principal)
    {
        return int.Parse(principal.FindFirst(ClaimTypes.Sid)!.Value);
    }

    public static UserRole GetRole(this ClaimsPrincipal principal)
    {
        return Enum.Parse<UserRole>(principal.FindFirst(ClaimTypes.Role)!.Value);
    }

    public static bool IsConnected(this IPrincipal principal)
    {
        return principal.Identity!.IsAuthenticated;
    }
}
