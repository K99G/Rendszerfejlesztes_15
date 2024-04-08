using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedmineServer.Models;

namespace RedmineServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectDevelopersController : ControllerBase
    {
 
        private readonly ApplicationDbContext _context;

        public ProjectDevelopersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // HTTP GET method to retrieve all developers.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDeveloperDTO>>> GetProjectDevelopers()
        {
            try
            {
                // Retrieve all Developers including their associated project.
                var ProjectDevelopers = await _context.ProjectDevelopers
                    .Include(pd => pd.Project)
                    .Include(pd => pd.Developer)
                    .Select(pd => new ProjectDeveloperDTO 
                    {
                        projectID = pd.Project_Id,
                        developerID = pd.Developer_Id,

                    })
                    .ToListAsync();

                return Ok(ProjectDevelopers);
            }
            catch (Exception)
            {
                // Return 500 Internal Server Error if database operation fails.
                return StatusCode(500, "Error accessing database");
            }
        }
    }
}
    
