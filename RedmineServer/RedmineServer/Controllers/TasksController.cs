using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedmineServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedmineServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // HTTP GET method to retrieve all tasks.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetTasks()
        {
            try
            {
                // Retrieve all tasks including their associated project, project type, and manager.
                var tasks = await _context.Tasks
                    .Include(t => t.Project)
                    .Include(t => t.Manager)
                    .Select(t => new TaskDTO
                    {
                        ID = t.Id,
                        Name = t.Name,
                        Description = t.Description,
                        ProjectId = t.Project_Id,
                        ProjectName = t.Project.Name,
                        UserId = t.Manager.Id,
                        ManagerName = t.Manager.Name, 
                        DateTime = t.Deadline
                    })
                    .ToListAsync();

                return Ok(tasks);
            }
            catch (Exception)
            {
                // Return 500 Internal Server Error if database operation fails.
                return StatusCode(500, "Error accessing database");
            }
        }
    }
}