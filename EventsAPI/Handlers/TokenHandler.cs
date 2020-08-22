using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventsAPI.Handlers
{
    public static class TokenHandler
    {
        public static bool IsUserAllowed(int UserId, string Token)
        {
            try
            {                
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(Token);
                var tokenS = handler.ReadToken(Token) as JwtSecurityToken;
                string Id = tokenS.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
                string Role = tokenS.Claims.First(claim => claim.Type == ClaimTypes.Role).Value;
                if (UserId == Convert.ToInt32(Id) || Role == "Admin") { return true; } else { return false; }
            }
            catch (Exception ex) { return false; }
        }
    }
}
