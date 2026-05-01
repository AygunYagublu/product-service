using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;

    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginRequest request)
    {
        /* sadə istifadəçi yoxlaması */
        if (request.Username == "admin" && request.Password == "password123")
        {
            var token = GenerateToken(request.Username);
            return Ok(new { token });
        }
        return Unauthorized(new { message = "Sehv istifadeci adi ve ya parol" });
    }

    private string GenerateToken(string username)
    {
        var key     = new SymmetricSecurityKey(
                          Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds   = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var token = new JwtSecurityToken(
            issuer:   _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims:   claims,
            expires:  DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public record LoginRequest(string Username, string Password);