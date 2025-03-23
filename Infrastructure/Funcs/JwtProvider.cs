using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TourProject.Infrastructure.Config;
using TourProject.Models;

namespace TourProject.Infrastructure.Funcs
{
    public class JwtProvider
    {
        private readonly JwtOptions _jwtOptions;
        public JwtProvider(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        public string CreateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new(ClaimTypes.Name ,user.Id.ToString())
            };
            foreach(var role in user.UserRoles.Select(ur=> ur.Role.Name))
            {
                claims.Add(new(ClaimTypes.Role, role));
            }
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_jwtOptions.ExpiresIn)
                );
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }
    }
}
