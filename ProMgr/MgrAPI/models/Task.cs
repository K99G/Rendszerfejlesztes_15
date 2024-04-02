public class Task
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ProjectId { get; set; }
    public int UserId { get; set; }
    public DateTime Deadline { get; set; }

    // Navigation properties
    public Project Project { get; set; }
    public Manager User { get; set; }
}