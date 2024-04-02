using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MgrAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly JsonDataService<Task> _taskService;

        public TasksController(JsonDataService<Task> taskService)
        {
            _taskService = taskService;
        }
        
        }
}