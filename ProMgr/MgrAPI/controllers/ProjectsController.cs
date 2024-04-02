using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly JsonDataService<Project> _projectService;
        private readonly JsonDataService<Task> _taskService; 

        public ProjectsController(JsonDataService<Project> projectService, JsonDataService<Task> taskService)
        {
            _projectService = projectService;
            _taskService = taskService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Project>> GetProjects([FromQuery] int? typeId)
        {
            try
            {
                var projects = _projectService.GetAll();
                if (typeId.HasValue)
                {
                    projects = projects.Where(p => p.TypeId == typeId.Value).ToList();
                }
                return Ok(projects);
            }
            catch (Exception ex)
            {
        
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id}/Tasks")]
        public ActionResult<IEnumerable<Task>> GetTasks(int id)
        {
            try
            {
                var tasks = _taskService.GetAll().Where(t => t.ProjectId == id);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }

