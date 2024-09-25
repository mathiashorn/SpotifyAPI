using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SpotifyAPI.Models;
using SpotifyAPI.Models.Auth;
using SpotifyAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpotifyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        // login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            // Check user credentials (in a real application, you'd authenticate against a database)
            if (model is { Username: "demo", Password: "password" })
            {
                // generate token for user
                var token = GenerateAccessToken(model.Username);
                // return access token for user's use
                return Ok(new { AccessToken = new JwtSecurityTokenHandler().WriteToken(token) });

            }
            // unauthorized user
            return Unauthorized("Invalid credentials");
        }

        // Generating token based on user information
        private JwtSecurityToken GenerateAccessToken(string userName)
        {
            // Create user claims
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userName),
            // Add additional claims as needed (e.g., roles, etc.)
        };

            // Create a JWT
            var token = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(1), // Token expiration time
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("averylongsecretkeythatisrequiredtobeused")),
                    SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}
