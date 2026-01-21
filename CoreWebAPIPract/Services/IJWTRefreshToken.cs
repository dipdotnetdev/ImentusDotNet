using CoreWebAPIPract.Models;
using Microsoft.AspNetCore.Identity;

namespace CoreWebAPIPract.Services
{
    public interface IJWTRefreshToken
    {
        public string GenerateAccessToken(IdentityUser user, IList<string> roles);
        public RefreshToken GenerateRefreshToken(string userId);
    }
}
