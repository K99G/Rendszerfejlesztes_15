using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedmineClient.DTOs
{
    internal class TaskDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ID { get; set; }
        public string Project {  get; set; }
        public string Manager { get; set; }
        public string DateTime { get; set; } 
        public string Developer { get; set; }
    }
}
