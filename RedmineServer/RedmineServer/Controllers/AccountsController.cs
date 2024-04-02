using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public IActionResult Login(string email, string password)
    {
        {
            // Retrieve user from database based on username
            var user = _context.Managers.FirstOrDefault(u => u.Name == email);

            if (user == null || !VerifyPassword(password, user.Password))
            {
                // Authentication failed
                return Unauthorized("Invalid username or password.");
            }

            // Authentication successful
            // You can generate a token or session ID here if needed
            return Ok();
        }
    }
    private bool VerifyPassword(string enteredPassword, string storedPassword)
    {
        // Implement your password verification logic here
        // For simplicity, you can use a simple string comparison
        return enteredPassword == storedPassword;
    }

}
