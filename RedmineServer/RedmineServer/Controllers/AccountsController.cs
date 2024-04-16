using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public AccountController(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login(string email, string password)
    {
        {
            // Retrieve user from database based on username
            var user = _context.Managers.FirstOrDefault(u => u.Email == email);

            if (user == null || !VerifyPassword(password, user.Password))
            {
                // Authentication failed
                return Unauthorized("Invalid email ("+email+") or password ("+password+").");
            }

            // Authentication successful
            // You can generate a token or session ID here if needed
            var token = GenerateJwtToken(user.Email);

            // Return token in response
            return Ok(new { token });
        }
    }
    private bool VerifyPassword(string enteredPassword, string storedPassword)
    {
        // Implement your password verification logic here
        // For simplicity, you can use a simple string comparison
        return enteredPassword == storedPassword;
    }

    private string GenerateJwtToken(string email)
    {
        // Create security key
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        // Create signing credentials
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Create claims
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Create token
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),
            signingCredentials: creds);

        // Serialize token to string
        return new JwtSecurityTokenHandler().WriteToken(token);
    }


}
