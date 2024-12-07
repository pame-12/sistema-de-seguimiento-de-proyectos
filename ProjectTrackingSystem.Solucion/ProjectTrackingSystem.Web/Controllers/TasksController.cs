using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectTrackingSystem.Application.Services;
using System.Linq;
using DomainTask = ProjectTrackingSystem.Domain.Entities.Task; // Alias para evitar conflicto con System.Threading.Tasks.Task

namespace ProjectTrackingSystem.Web.Controllers
{
    public class TasksController : Controller
    {
        private readonly TaskService _taskService;
        private readonly ProjectService _projectService;
        private readonly UserService _userService;

        public TasksController(TaskService taskService, ProjectService projectService, UserService userService)
        {
            _taskService = taskService;
            _projectService = projectService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = await _taskService.GetTasksAsync();
            return View(tasks);


        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Projects = new SelectList(await _projectService.GetProjectsAsync(), "Id", "Name");
            ViewBag.Users = new SelectList(await _userService.GetUsersAsync(), "Id", "FirstName");

            // Inicializa las propiedades requeridas
            var newTask = new DomainTask
            {
                Title = "",
                Description = "",
                Status = "",
                DueDate = DateTime.Now.AddDays(7), // Fecha predeterminada
                ProjectId = 0, // Inicializa con un valor inválido que obligue al usuario a seleccionarlo
                UserId = 0 // Igual, requiere selección explícita por parte del usuario
            };

            return View(newTask);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DomainTask task)
        {
            if (!ModelState.IsValid)
            {
                // Mostrar los errores en la consola o registrarlos
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }

                // Recargar los datos para los selectores en caso de error
                ViewBag.Projects = new SelectList(await _projectService.GetProjectsAsync(), "Id", "Name", task.ProjectId);
                ViewBag.Users = new SelectList(await _userService.GetUsersAsync(), "Id", "FirstName", task.UserId);

                return View(task); // Retornar el modelo con los errores
            }

            // Validar que ProjectId y UserId no estén vacíos o nulos
            if (task.ProjectId == 0)
            {
                ModelState.AddModelError("ProjectId", "Please select a project.");
            }

            if (task.UserId == 0)
            {
                ModelState.AddModelError("UserId", "Please select a user.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Si es válido, crear la tarea
                    await _taskService.CreateTaskAsync(task);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    Console.WriteLine($"Error: {ex.Message}");

                    // Recargar los datos para los selectores en caso de excepción
                    ViewBag.Projects = new SelectList(await _projectService.GetProjectsAsync(), "Id", "Name", task.ProjectId);
                    ViewBag.Users = new SelectList(await _userService.GetUsersAsync(), "Id", "FirstName", task.UserId);

                    return View(task); // Retornar el modelo con los datos cargados
                }
            }

            return View(task);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound();

            ViewBag.Projects = new SelectList(await _projectService.GetProjectsAsync(), "Id", "Name");
            ViewBag.Users = new SelectList(await _userService.GetUsersAsync(), "Id", "FirstName");
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DomainTask task)
        {
            if (!ModelState.IsValid)
            {
                // Recargar los datos en caso de error
                ViewBag.Projects = new SelectList(await _projectService.GetProjectsAsync(), "Id", "Name", task.ProjectId);
                ViewBag.Users = new SelectList(await _userService.GetUsersAsync(), "Id", "FirstName");
                return View(task); // Retornar el modelo con los errores
            }

            await _taskService.UpdateTaskAsync(task);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound();

            return View(task);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound();

            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _taskService.DeleteTaskAsync(id); // Solo necesitas el id para eliminar
            return RedirectToAction("Index");
        }
    }
}
