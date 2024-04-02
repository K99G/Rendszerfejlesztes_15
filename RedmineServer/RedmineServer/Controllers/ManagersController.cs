using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ManagerDTO>>> GetManagers()
        {
            var managers = await _context.Managers
                .Select(m => new ManagerDTO { Email = m.Email, Name = m.Name })
                .ToListAsync();

            return Ok(managers);
        }
    }
}