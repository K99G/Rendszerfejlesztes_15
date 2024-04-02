public class Manager
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    // Navigation property
    public ICollection<Task> Tasks { get; set; }
}