using CoreWebAPIPract.DTO_s;
using CoreWebAPIPract.IdentityBasedAuth;
using CoreWebAPIPract.Models;
using CoreWebAPIPract.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoreWebAPIPract.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IJWTRefreshToken _jWT;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        public AccountController(UserManager<IdentityUser> userManager, IJWTRefreshToken jWT, ApplicationDbContext context)
        {
            _userManager = userManager;
            _jWT = jWT;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string email, string password)
        {
            var user = new IdentityUser
            {
                UserName = email,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return BadRequest   (result.Errors);

            await _userManager.AddToRoleAsync(user, "User");

            return Ok("User registered successfull");
        }

        [HttpPost("assign-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRole(string email, string role)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return BadRequest("User not found");

            await _userManager.AddToRoleAsync(user, role);

            return Ok($"Role {role} assigned to {email}");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email,
            string password,
            [FromServices]IConfiguration configuration,
            [FromServices]SignInManager<IdentityUser> signInManager)
        {
            var user = await _userManager.FindByNameAsync(email);

            if (user == null)
                return Unauthorized();

            var result = await signInManager.CheckPasswordSignInAsync(user, password, false);

            if (!result.Succeeded)
                return Unauthorized();

            var roles = await _userManager.GetRolesAsync(user);

            var accessToken = _jWT.GenerateAccessToken(user, roles);
            var refreshToken = _jWT.GenerateRefreshToken(user.Id);

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                accessToken,
                refreshToken = refreshToken.Token,
            });

            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, user.UserName),
            //    new Claim(ClaimTypes.NameIdentifier, user.Id),
            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            //};

            //foreach(var role in roles)
            //    claims.Add(new Claim(ClaimTypes.Role, role.ToString()));

            //var key = new SymmetricSecurityKey(
            //    Encoding.UTF8.GetBytes(configuration["jwt:key"]));

            //var token = new JwtSecurityToken(
            //    issuer: configuration["jwt:Issuer"],
            //    audience: configuration["jwt:Audience"],
            //    claims: claims,
            //    expires: DateTime.UtcNow.AddMinutes(
            //        int.Parse(configuration["jwt:DurationInMinutes"])),
            //    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            //    );

            //return Ok(new
            //{
            //    token = new JwtSecurityTokenHandler().WriteToken(token)
            //});
        }

        //[HttpPost("Logout")]
        //public async Task<ActionResult> Logout()
        //{

        //}

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenDto token)
        {
            var storedToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token.RefreshToken);

            if (storedToken == null || storedToken.IsRevoked || storedToken.Expires < DateTime.UtcNow)
                return Unauthorized();

            storedToken.IsRevoked = true;

            var user = await _userManager.FindByIdAsync(storedToken.UserId);
            var roles = await _userManager.GetRolesAsync(user);

            var newAccessToken = _jWT.GenerateAccessToken(user, roles);
            var newRefreshToken = _jWT.GenerateRefreshToken(user.Id);

            _context.RefreshTokens.Add(newRefreshToken);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                newAccessToken,
                newRefreshToken = newRefreshToken.Token,
            });
        }
    }
}
