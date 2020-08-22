using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EventsAPI.Models;
using EventsAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EventsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _config;
        private IUser UserRepository;
        
        public LoginController(IConfiguration config)
        {
            _config = config;
            this.UserRepository = new UserRepository(new EventsDBContext());
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] Login Login)
        {
            IActionResult Response = Unauthorized();
            User User = UserRepository.Authorize(Login);

            if (User != null)
            {
                var TokenString = GenerateJSONWebToken(User);
                Response = Ok(new { token = TokenString });
            }

            return Response;
        }

        private string GenerateJSONWebToken(User User)
        {
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var Claims = new[] {
                new Claim(ClaimTypes.Email, User.Email),
                new Claim(ClaimTypes.Role, User.Role),
                new Claim(ClaimTypes.NameIdentifier, User.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                Claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: Credentials);            

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
                
    }
}
