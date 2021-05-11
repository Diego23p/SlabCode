using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SlabCode.API.Controller
{
    public class TokenController : ControllerBase
    {
        [HttpPost("/token")]
        public IActionResult Get(string username, string password)
        {
            if (username == password)
                return Ok(GenerateToken(username));
            else
                return Unauthorized();
        }

        private string GenerateToken(string username)
        {
            var sk = "grvb$#&5rtdgDFHgjhhghfg";
            var key = Encoding.ASCII.GetBytes(sk);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, username));

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
