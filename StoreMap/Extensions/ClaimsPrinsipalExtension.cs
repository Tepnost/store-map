using System.Linq;
using System.Security.Claims;
using StoreMap.Data.Enums;
using StoreMap.Data.Extensions;

namespace StoreMap.Extensions
{
    public static class ClaimsPrinsipalExtension
    {
        public static bool IsInRole(this ClaimsPrincipal user, UserRole role)
        {
            return role.GetRefCodesList().Where(user.IsInRole).Any();
        }
    }
}