using ProjectTrackingSystem.Application.Services; // Agrega esta importación
using ProjectTrackingSystem.Infrastructure.Data; // También asegura que esta importación esté presente
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllersWithViews();

// Configuración de DbContext para MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql("Server=localhost;Database=project_tracking_system;User=root;Password=Pbv.120803;",
        new MySqlServerVersion(new Version(8, 0, 25)))); // Ajusta los detalles de la conexión según tu configuración

// Registrar ProjectService
builder.Services.AddScoped<ProjectService>(); // Esto es lo que debes agregar para registrar el servicio

builder.Services.AddScoped<UserService, UserService>();

builder.Services.AddScoped<TaskService>();


var app = builder.Build();

// Configurar el pipeline de solicitud HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
