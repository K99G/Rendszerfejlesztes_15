public class Manager
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class Developer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int TypeId { get; set; } 
    public string Description { get; set; }
}

public class ProjectType
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Task
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ProjectId { get; set; } 
    public int UserId { get; set; }
    public DateTime Deadline { get; set; }
}