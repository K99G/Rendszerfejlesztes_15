using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Manager> Managers { get; set; }
    public DbSet<Developer> Developers { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectType> ProjectTypes { get; set; }
    public DbSet<Task> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectDeveloper>()
            .HasKey(pd => new { pd.DeveloperId, pd.ProjectId });

        modelBuilder.Entity<ProjectDeveloper>()
            .HasOne(pd => pd.Project)
            .WithMany(p => p.ProjectDevelopers)
            .HasForeignKey(pd => pd.ProjectId);

        modelBuilder.Entity<ProjectDeveloper>()
            .HasOne(pd => pd.Developer)
            .WithMany(d => d.ProjectDevelopers)
            .HasForeignKey(pd => pd.DeveloperId);
        

    }
}