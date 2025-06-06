using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechInvent.DM.Models;

namespace WebMVC.Services
{
    public class JWTokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public JWTokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWTKey") ?? _config["JWT:Key"]));
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("id", user.IdUser.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)

            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDesciptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(30), // _config["JWT:TTL"]
                SigningCredentials = creds,
                Issuer = Environment.GetEnvironmentVariable("JWTIssuer") ?? _config["JWT:Issuer"],
                Audience = Environment.GetEnvironmentVariable("JWTAudience") ?? _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDesciptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
