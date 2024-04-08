using Microsoft.EntityFrameworkCore;
using RedmineServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RedmineServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ManagersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Retrieves all managers as ManagerDTO objects.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ManagerDTO>>> GetManagers()
        {
            try
            {
                var managers = await _context.Managers
                    .Select(m => new ManagerDTO { Email = m.Email, Name = m.Name, Password = m.Password })
                    .ToListAsync();

                return Ok(managers);
            }
            catch (Exception)
            {
                // Return an internal server error if database operation fails.
                return StatusCode(500, "Error accessing database");
            }
        }
    
    [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ManagerDTO>>> GetManagersId(int id)
        {
            try
            {
                    var managers = await _context.Managers.Where(x=>x.Id==id)
                        .Select(m => new ManagerDTO { Email = m.Email, Name = m.Name, Password = m.Password })
                        .ToListAsync();

                    return Ok(managers);
            }
            catch (Exception)
            {
                    // Return an internal server error if database operation fails.
                    return StatusCode(500, "Error accessing database");
            }
        }
    }
}

