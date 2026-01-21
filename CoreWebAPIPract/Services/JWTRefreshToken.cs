using CoreWebAPIPract.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CoreWebAPIPract.Services
{
    public class JWTRefreshToken : IJWTRefreshToken
    {
        private readonly IConfiguration _configuration;

        public JWTRefreshToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateAccessToken(IdentityUser user, IList<string> roles)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName)
        };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    int.Parse(_configuration["Jwt:AccessTokenExpiryMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);


        }
        public RefreshToken GenerateRefreshToken(string userId)
        {
            return new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddDays(int.Parse(_configuration["jwt:RefreshTokenExpiryDays"])),
                UserId = userId,
                IsRevoked = false
            };
        }
    }
}
