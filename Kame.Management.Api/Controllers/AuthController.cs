using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using Kame.Management.Core.Entity;
using Kame.Management.Core.Services;


namespace Kame.Management.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        public AuthController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<dynamic> Post([FromBody] User user)
        {
            string password = CreateSha512Password(user.Password);
            User authUser = null;

            IKameDbContext dbContext = (IKameDbContext)HttpContext.RequestServices.GetService(typeof(IKameDbContext));
            authUser = dbContext.GetUser(user.Name, password);
            if (authUser != null)
            {
                authUser.Token = GenerateToken(authUser);
            }

            return authUser;
        }


        private string CreateSha512Password(string password)
        {
            SHA512 sha1 = SHA512.Create();
            return Convert.ToBase64String(sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(Configuration["Kame:ApiKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("Kame", "user")
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
