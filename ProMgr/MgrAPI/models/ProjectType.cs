public class ProjectType
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation property
    public ICollection<Project> Projects { get; set; }
}
