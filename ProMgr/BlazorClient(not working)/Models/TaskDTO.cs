using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorClient.Models
{
    public class TaskDTO
    {
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public int UserId { get; set; }
    public string ManagerName { get; set; }
    public DateTime DateTime { get; set; }
    }
}