public class Manager
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<Task> Tasks { get; set; }
}

public class Developer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public ICollection<ProjectDeveloper> ProjectDevelopers { get; set; }

}

public class ProjectDeveloper
{
    public int Developer_Id { get; set; }
    public Developer Developer { get; set; }
    public int Project_Id { get; set; }
    public Project Project { get; set; }
}

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Type_Id { get; set; }
    public string Description { get; set; }

    public Project_Types ProjectType { get; set; }
    public ICollection<ProjectDeveloper> ProjectDevelopers { get; set; }
    public ICollection<Task> Tasks { get; set; }
}

public class Project_Types
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public ICollection<Project> Projects { get; set; }

}

public class Task
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Project_Id { get; set; }
    public Project Project { get; set; }
    public int User_Id { get; set; }
    public Manager Manager { get; set; } 
    public DateTime Deadline { get; set; }
}