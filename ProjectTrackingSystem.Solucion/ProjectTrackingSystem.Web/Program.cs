using ProjectTrackingSystem.Infrastructure.Data; // Aseg�rate de tener la referencia correcta
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuraci�n del DbContext para MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql("Server=localhost;Database=project_tracking_system;User=root;Password=Pbv.120803;",
        new MySqlServerVersion(new Version(8, 0, 25)))); // Ajusta los detalles de la conexi�n seg�n tu configuraci�n

var app = builder.Build();

// Configure the HTTP request pipeline.
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

