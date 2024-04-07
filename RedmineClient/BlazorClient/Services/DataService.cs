using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using BlazorClient.Models;

namespace BlazorClient.Services
{
    public class DataService
    {
        private readonly HttpClient _http;
        
        // Declare the event as nullable
        public event Action<string>? OnError;

        public DataService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ProjectDTO>> GetProjectsAsync()
        {
            try
            {
                var projects = await _http.GetFromJsonAsync<List<ProjectDTO>>("api/Projects");
                return projects ?? new List<ProjectDTO>();
            }
            catch (HttpRequestException e)
            {
                HandleError($"Network error while fetching projects: {e.Message}");
            }
            catch (NotSupportedException e)
            {
                HandleError($"The content type is not supported while fetching projects: {e.Message}");
            }
            catch (JsonException e)
            {
                HandleError($"Invalid JSON while fetching projects: {e.Message}");
            }
            catch (Exception e)
            {
                HandleError($"Unexpected error while fetching projects: {e.Message}");
            }
            return new List<ProjectDTO>();
        }

        public async Task<List<ManagerDTO>> GetManagersAsync()
        {
            try
            {
                var managers = await _http.GetFromJsonAsync<List<ManagerDTO>>("api/Managers");
                return managers ?? new List<ManagerDTO>();
            }
            catch (Exception e)
            {
                HandleError($"Error fetching managers: {e.Message}");
            }
            return new List<ManagerDTO>();
        }

        public async Task<List<TaskDTO>> GetTasksAsync()
        {
            try
            {
                var tasks = await _http.GetFromJsonAsync<List<TaskDTO>>("api/Tasks");
                return tasks ?? new List<TaskDTO>();
            }
            catch (Exception e)
            {
                HandleError($"Error fetching tasks: {e.Message}");
            }
            return new List<TaskDTO>();
        }

        private void HandleError(string message)
        {
            // For now, let's just pass the error message back up to the UI
            OnError?.Invoke(message);
        }
    }
}