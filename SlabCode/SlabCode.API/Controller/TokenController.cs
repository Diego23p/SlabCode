using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SlabCode.Core.ProjectServices.Implementation;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SlabCode.API.Controller
{
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserServices UserServices;

        public TokenController(IConfiguration configuration, UserServices userServices)
        {
            _configuration = configuration;
            UserServices = userServices;
        }

        [HttpPost("/token")]
        public IActionResult Get(string username, string password)
        {
            Tuple<bool, string> response = UserServices.login(username, password);
            if (response.Item1)
                return Ok(GenerateToken(username, response.Item2));
            else
                return Unauthorized();
        }

        private string GenerateToken(string username, string role)
        {
            var sk = _configuration.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(sk);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, username));
            claims.AddClaim(new Claim(ClaimTypes.Role, role));

            var tokenD = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials( new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var th = new JwtSecurityTokenHandler();
            var CreateToken = th.CreateToken(tokenD);

            string bearer_token = th.WriteToken(CreateToken);
            return bearer_token;
        }
    }
}
