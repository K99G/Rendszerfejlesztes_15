public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int TypeId { get; set; }
    public string Description { get; set; }

    // Navigation properties
    public ProjectType Type { get; set; }
    public ICollection<Task> Tasks { get; set; }
    public ICollection<ProjectDeveloper> ProjectDevelopers { get; set; }
}
