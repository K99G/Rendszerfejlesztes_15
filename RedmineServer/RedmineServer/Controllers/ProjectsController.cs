using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using RedmineServer.Data;
using RedmineServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedmineServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjects()
        {
            var projects = await _context.Projects
                .Include(p => p.ProjectType) // use ProjectType
                .Select(p => new ProjectDTO 
                {
                    ID = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    TypeId = p.ProjectType.Id,
                    TypeName = p.ProjectType.Name
                })
                .ToListAsync();

            return Ok(projects);
        }
    }
}
