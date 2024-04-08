using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // HTTP GET method to retrieve all projects.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjects()
        {
            try
            {
                // Retrieve all projects including their associated project types.
                var projects = await _context.Projects
                    .Include(p => p.ProjectType)
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
            catch (Exception)
            {
                // Return 500 Internal Server Error if database operation fails.
                return StatusCode(500, "Error accessing database");
            }
        }
         [HttpGet("{id}")]
         public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjectsId(int id)
        {
            try
            {
                // Retrieve all projects including their associated project types.
                var projects = await _context.Projects.Where(x=>x.Type_Id==id)
                    .Include(p => p.ProjectType)
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
            catch (Exception)
            {
                // Return 500 Internal Server Error if database operation fails.
                return StatusCode(500, "Error accessing database");
            }
        }
    }
}
