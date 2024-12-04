using Microsoft.EntityFrameworkCore;
using ProjectTrackingSystem.Domain.Entities;
using ProjectTrackingSystem.Infrastructure.Data;
using DomainTask = ProjectTrackingSystem.Domain.Entities.Task;


namespace ProjectTrackingSystem.Application.Services
{
    public class TaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DomainTask>> GetTasksAsync()
        {
            // Usar Include para cargar las relaciones de Project y User
            var tasks = await _context.Tasks
                .Include(t => t.Project)  // Incluir la relación con el proyecto
                .Include(t => t.User)     // Incluir la relación con el usuario
                .ToListAsync();          // Ejecutar la consulta de forma asíncrona

            return tasks;
        }

        public async Task<DomainTask> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<DomainTask> CreateTaskAsync(DomainTask task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task; // Devuelve el objeto creado
        }


        public async Task<DomainTask> UpdateTaskAsync(DomainTask task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task; // Devuelve el objeto actualizado
        }


        public async Task<DomainTask> DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                throw new KeyNotFoundException($"No se encontró una tarea con el ID {id}");
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return task; // Devuelve el objeto eliminado
        }

    }
}
