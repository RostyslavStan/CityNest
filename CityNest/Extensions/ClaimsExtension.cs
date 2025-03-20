using System;
using System.Linq;
using System.Security.Claims;

namespace CityNest.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            var userIdString = principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdString != null && Guid.TryParse(userIdString, out Guid userId))
            {
                return userId;
            }
            else
            {
                throw new Exception("User ID not found or is invalid in the token");
            }
        }
    }
}
