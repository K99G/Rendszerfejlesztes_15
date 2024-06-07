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
            var token = GenerateJwtToken(user.Email, user.Role);

            // Return token in response
            return Ok(new { token });
        }
    }
    /*[HttpPost("validate")]
    public IActionResult ValidateToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                // Extract a specific claim, e.g., user's email or a user identifier
                var userClaim = principal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
                if (userClaim == null)
                {
                    Console.WriteLine("Claims present in the token:");
                    foreach (var claim in principal.Claims)
                    {
                        Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
                    }
                    return Unauthorized("Token is valid but does not contain the expected claim.");
                }
                // Construct a success message
                var successMessage = $"Token is valid for user: {userClaim}";
                return Ok(new { Message = successMessage });

            }
            // authorization exceptions
            catch (SecurityTokenExpiredException)
            {
                return Unauthorized("Token has expired.");
            }
            catch (SecurityTokenValidationException stvex)
            {
                return Unauthorized($"Token validation failed: {stvex.Message}");
            }
            catch (Exception ex)
            {
                return Unauthorized($"Invalid token: {ex.Message}");
            }
        }catch(Exception ex)
        { Console.WriteLine(ex.Message.ToString());
            Console.WriteLine(ex.InnerException.ToString());
            return Unauthorized($"Invalid token: {ex.Message}");
        }
    }*/

    private bool VerifyPassword(string enteredPassword, string storedPassword)
    {
        // Implement your password verification logic here
        // For simplicity, you can use a simple string comparison
        return enteredPassword == storedPassword;
    }

    private string GenerateJwtToken(string email, string role)
    {
        // Create security key
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        // Create signing credentials
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Create claims
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, role)
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

    
    /* auto user validation
    [HttpGet("secure-data")]
    public IActionResult GetSecureData()
    {
        if (User.Identity.IsAuthenticated)
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            return Ok($"This is secure data available to {userEmail}");
        }
        return Unauthorized("You need to be logged in.");
    } */

    //manual validation
     

}
