using Microsoft.AspNetCore.Mvc;
using ProjectTrackingSystem.Application.Services;
using ProjectTrackingSystem.Domain.Entities;

namespace ProjectTrackingSystem.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ProjectService _projectService;

        public ProjectsController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetProjectsAsync();
            return View(projects);
        }

        public IActionResult Create(ICollection<Domain.Entities.Task> tasks)
        {
            // Inicializa las propiedades requeridas
            var project = new Project
            {
                Name = string.Empty,
                Description = string.Empty,
                Tasks = tasks
            };
            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Project project)
        {
            if (ModelState.IsValid)
            {
                await _projectService.CreateProjectAsync(project);
                return RedirectToAction("Index");
            }
            return View(project);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
                return NotFound();

            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                await _projectService.CreateProjectAsync(project);
                return RedirectToAction("Index");
            }
            return View(project);
        }

        public async Task<IActionResult> Details(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
                return NotFound();

            return View(project);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
                return NotFound();

            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Project project)
        {
            await _projectService.DeleteProjectAsync(project.Id);
            return RedirectToAction("Index");
        }
    }
}
