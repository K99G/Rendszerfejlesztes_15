using Microsoft.EntityFrameworkCore;
using RedmineServer.Models;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Manager> Managers { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Developer> Developers { get; set; }
    public DbSet<Task> Tasks { get; set; }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);


    modelBuilder.Entity<Project>()
        .HasOne(p => p.ProjectType)
        .WithMany() 
        .HasForeignKey(p => p.Type_Id);

    modelBuilder.Entity<ProjectDeveloper>()
        .HasKey(pd => new { pd.Project_Id, pd.Developer_Id });

    modelBuilder.Entity<ProjectDeveloper>()
        .HasOne(pd => pd.Project)
        .WithMany(p => p.ProjectDevelopers)
        .HasForeignKey(pd => pd.Project_Id);

    modelBuilder.Entity<ProjectDeveloper>()
        .HasOne(pd => pd.Developer)
        .WithMany() 
        .HasForeignKey(pd => pd.Developer_Id);

    modelBuilder.Entity<Task>()
        .HasOne(t => t.Project)
        .WithMany(p => p.Tasks)
        .HasForeignKey(t => t.Project_Id);

    modelBuilder.Entity<Task>()
        .HasOne(t => t.Manager)
        .WithMany(m => m.Tasks)
        .HasForeignKey(t => t.User_Id);
}
      
}