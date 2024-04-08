using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedmineServer.Models
{
    public class ProjectDeveloperDTO
    {
        public int projectID { get; set; }
        public string projectName { get; set; }
        public int developerID { get; set; }
        public string developerName { get; set; }
    }
}