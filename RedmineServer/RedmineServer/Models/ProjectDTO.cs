namespace RedmineServer.Models
{
    public class ProjectDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; } 
    }
}
