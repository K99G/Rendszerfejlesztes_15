namespace RedmineServer.Models
{
    public class TaskDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Project { get; set; }
        public string DateTime { get; set; }
         public string Manager { get; set; }
        public string Developer { get; set; }

    }
}
