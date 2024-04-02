using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly JsonDataService<Manager> _managerService;
    private readonly IConfiguration _configuration;

    public LoginController(JsonDataService<Manager> managerService, IConfiguration configuration)
    {
        _managerService = managerService;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login(string email, string password)
    {
        var manager = _managerService.GetAll().FirstOrDefault(u => u.Email == email && u.Password == password);
        if (manager != null)
        {
            var token = GenerateJwtToken(manager);
            return Ok(new { token });
        }
        return Unauthorized(new { message = "Incorrect email or password." });
    }

    private string GenerateJwtToken(Manager manager)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JwtKey"]); // Ensure your key is stored securely
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, manager.Id.ToString()),
                new Claim(ClaimTypes.Name, manager.Name)
            }),
            Expires = DateTime.UtcNow.AddDays(7), // Token expiration
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}