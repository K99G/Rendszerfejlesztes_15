public class Developer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    // Navigation property
    public ICollection<ProjectDeveloper> ProjectDevelopers { get; set; }
}
