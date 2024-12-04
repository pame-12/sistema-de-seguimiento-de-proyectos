using Microsoft.EntityFrameworkCore;
using ProjectTrackingSystem.Domain.Entities;

namespace ProjectTrackingSystem.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTrackingSystem.Domain.Entities.Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public object Task { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=project_tracking_system;User=root;Password=Pbv.120803;",
                new MySqlServerVersion(new Version(8, 0, 25))); // Especifica la versión de MySQL
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de entidades
            modelBuilder.Entity<Project>().ToTable("projects")
                .HasMany(p => p.Tasks)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId); ;
            modelBuilder.Entity<ProjectTrackingSystem.Domain.Entities.Task>().ToTable("tasks");
            modelBuilder.Entity<User>().ToTable("users")
                .HasMany(u => u.Tasks)
                .WithOne(t => t.User) // Asegúrate de que `Task` tenga una propiedad `UserId` o similar
                .HasForeignKey(t => t.UserId);
        }
    }
}
