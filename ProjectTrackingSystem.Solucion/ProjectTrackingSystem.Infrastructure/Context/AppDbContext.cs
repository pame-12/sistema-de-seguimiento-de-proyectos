using Microsoft.EntityFrameworkCore;
using ProjectTrackingSystem.Domain.Entities;

namespace ProjectTrackingSystem.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTrackingSystem.Domain.Entities.Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=project_tracking_system;User=root;Password=Pbv.120803;",
                new MySqlServerVersion(new Version(8, 0, 25))); // Especifica la versión de MySQL
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de entidades
            modelBuilder.Entity<Project>().ToTable("projects");
            modelBuilder.Entity<ProjectTrackingSystem.Domain.Entities.Task>().ToTable("tasks");
            modelBuilder.Entity<User>().ToTable("users");
        }
    }
}
