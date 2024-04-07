using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using BlazorClient.Models;

namespace BlazorClient.Services
{
    public class DataService
    {
        private readonly HttpClient _http;

        public DataService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ProjectDTO>> GetProjectsAsync()
        {
           var projects = await _http.GetFromJsonAsync<List<ProjectDTO>>("api/Projects");
           return projects ?? new List<ProjectDTO>(); 
        }

        public async Task<List<ManagerDTO>> GetManagersAsync()
        {
            var managers = await _http.GetFromJsonAsync<List<ManagerDTO>>("api/Managers");
            return managers ?? new List<ManagerDTO>(); // Provide an empty list as fallback
        }

        public async Task<List<TaskDTO>> GetTasksAsync()
        {
            var tasks = await _http.GetFromJsonAsync<List<TaskDTO>>("api/Tasks");
            return tasks ?? new List<TaskDTO>(); // Provide an empty list as fallback
        }
    }
}