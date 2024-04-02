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
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetTasks()
        {
            var tasks = await _context.Tasks
                .Select(t => new TaskDTO { Name = t.Name, Description = t.Description, Project = t.ProjectId.ToString(), DateTime = t.Deadline.ToString() })
                .ToListAsync();

            return Ok(tasks);
        }
    }
}