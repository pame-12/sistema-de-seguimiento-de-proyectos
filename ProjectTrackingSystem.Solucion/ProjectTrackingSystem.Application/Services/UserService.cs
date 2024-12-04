using Microsoft.EntityFrameworkCore;
using ProjectTrackingSystem.Domain.Entities;
using ProjectTrackingSystem.Infrastructure.Data;

namespace ProjectTrackingSystem.Application.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user); // Agregar el usuario
            Console.WriteLine($"Guardando usuario: {user.FirstName} {user.LastName}");

            await _context.SaveChangesAsync(); // Guardar los cambios
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id); // Buscar el usuario por ID
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync(); // Obtener todos los usuarios
        }

        public async System.Threading.Tasks.Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user); // Actualizar el usuario
            await _context.SaveChangesAsync(); // Guardar los cambios
        }

        public async System.Threading.Tasks.Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id); // Buscar el usuario
            if (user != null)
            {
                _context.Users.Remove(user); // Eliminar el usuario
                await _context.SaveChangesAsync(); // Guardar los cambios
            }
        }
    }
}
