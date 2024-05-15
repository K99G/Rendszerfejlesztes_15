
using Microsoft.AspNetCore.Components.Authorization;

public class Manager
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public ICollection<Task> Tasks { get; set; } // Collection of tasks assigned to the manager
}

public class Developer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public ICollection<Project_Developers> Project_Developers { get; set; } // Collection of projects the developer is associated with

}

public class Project_Developers
{
    public int Developer_Id { get; set; }  // Foreign key for Developer
    public Developer Developer { get; set; }  // Navigation property to Developer
    public int Project_Id { get; set; }  // Foreign key for Project
    public Project Project { get; set; }  // Navigation property to Project

}

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Type_Id { get; set; } // Foreign key for Project_Types
    public string Description { get; set; }

    public Project_Types ProjectType { get; set; }  // Navigation property to Project_Types
    public ICollection<Project_Developers> Project_Developers { get; set; }  // Collection of developers associated with the project
    public ICollection<Task> Tasks { get; set; }  // Collection of tasks associated with the project
}

public class Project_Types
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public ICollection<Project> Projects { get; set; } // Collection of projects with this project type

}

public class Task
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Project_Id { get; set; } // Foreign key for Project
     public Project Project { get; set; } // Navigation property to Project
    public int User_Id { get; set; } // Foreign key for Manager
    public Manager Manager { get; set; } // Navigation property to Manager
    public DateTime Deadline { get; set; }
    internal static async Task<AuthenticationState> FromResult(AuthenticationState authenticationState)
    {
        throw new NotImplementedException();
    }
} 