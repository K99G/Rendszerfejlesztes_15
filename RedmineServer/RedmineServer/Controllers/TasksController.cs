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
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetTask(int id)
        {
            try
            {
                // Retrieve all tasks including their associated project, project type, and manager.
                var tasks = await _context.Tasks
                    .Include(t => t.Project)
                    .Include(t => t.Manager)
                    .Where(t=>t.Id==id)
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

        // project alapján taskokat keres
        [HttpGet("project/{id}")]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetTaskByProject(int id)
        {
            var tasks = await _context.Tasks
                .Include(t=>t.Project)
                .Include(t=>t.Manager)
                .Where(t => t.Project_Id == id)
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

        // manager alapján taskokat keres
        [HttpGet("manager/{id}")]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetTaskByManager(int id)
        {
            var tasks = await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.Manager)
                .Where(t => t.User_Id == id)
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
        // taskot hozzáadni
        [HttpPost]
        public async Task<IActionResult> PostTodoItem(TaskDTO taskDTO)
        {
            var developerID = _context.Developers.Where(p => p.Name == taskDTO.Developer).FirstOrDefault().Id;
            //var userID = _context.Managers.Where(m => m.Name == taskDTO.Name).FirstOrDefault().Id;
            //var projectID = _context.Projects.Where(p => p.Name == taskDTO.ProjectName).FirstOrDefault().Id;

            var task = new Task
            {
                Name = taskDTO.Name,
                Description = taskDTO.Description,
                Deadline = DateTime.Now.AddDays(30),
                Project_Id = taskDTO.ProjectId,
                User_Id = taskDTO.UserId
            };

            _context.Tasks.Add(task);
            if (ProjectDevExists(taskDTO.ProjectId, developerID)==false)
            {

                _context.Project_Developers.Add(new Project_Developers
                {
                    Developer_Id = developerID,
                    Project_Id = taskDTO.ProjectId,
                });
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTask),
                new { id = task.Id },
                new TaskDTO { Name = task.Name, Description = task.Description });
        }
        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
        private bool ProjectDevExists(int projectID, int developerID)
        {
            return _context.Project_Developers.Any(e => ((e.Project_Id == projectID) && (e.Developer_Id == developerID)));
        }
    }
}