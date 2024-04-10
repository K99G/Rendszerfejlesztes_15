using Microsoft.EntityFrameworkCore;
using RedmineServer.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // Database tables
    public DbSet<Manager> Managers { get; set; }
    public DbSet<Project_Developers> Project_Developers { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Developer> Developers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Define relationships and constraints

        // Each project has one project type
        modelBuilder.Entity<Project>()
            .HasOne(p => p.ProjectType)
            .WithMany()
            .HasForeignKey(p => p.Type_Id);

   
  
        // Many-to-many relationship between projects and developers
        modelBuilder.Entity<Project_Developers>()
            .HasKey(pd => new { pd.Project_Id, pd.Developer_Id });

        // Each project has many developers
        modelBuilder.Entity<Project_Developers>()
            .HasOne(pd => pd.Project)
            .WithMany(p => p.Project_Developers)
            .HasForeignKey(pd => pd.Project_Id);

        // Each developer has many projects
        modelBuilder.Entity<Project_Developers>()
            .HasOne(pd => pd.Developer)
            .WithMany(p => p.Project_Developers)
            .HasForeignKey(pd => pd.Developer_Id);
    
        // Each task belongs to one project
        modelBuilder.Entity<Task>()
            .HasOne(t => t.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.Project_Id);

        // Each task is managed by one manager
        modelBuilder.Entity<Task>()
            .HasOne(t => t.Manager)
            .WithMany(m => m.Tasks)
            .HasForeignKey(t => t.User_Id);
    }
}