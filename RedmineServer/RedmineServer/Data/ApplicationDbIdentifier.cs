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
    public int Type_Id { get; set; } 
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
    public int Project_Id { get; set; } 
    public int User_Id { get; set; }
    public DateTime Deadline { get; set; }
}