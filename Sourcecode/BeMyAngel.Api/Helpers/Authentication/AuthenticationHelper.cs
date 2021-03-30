using IdentityModel;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace BeMyAngel.Api.Helpers.Authentication
{
    public class AuthenticationHelper
    {
        public static int? GetCurrentUserId(HttpContext httpContext)
        {
            var user = httpContext.User;
            if (user != null)
            {
                var userClaims = user.Claims.Where(x => x.Type == JwtClaimTypes.Id).FirstOrDefault();
                if(userClaims != null)
                    return int.Parse(userClaims.Value);
            }
            return null;
        }
    }
}
