using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.API.Domain.Projects.Interfaces;
using ProjectManagement.API.Domain.Projects.Models;
using ProjectManagement.API.Domain.Users.Entities;

namespace ProjectManagement.API.Domain.Projects.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService _projectsService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectsController(IProjectsService projectsService, UserManager<ApplicationUser> userManager)
        {
            _projectsService = projectsService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto dto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var project = await _projectsService.CreateProjectAsync(user, dto.Name);
            return CreatedAtAction(nameof(GetProjectById), new {Id = project.Id}, project);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(long id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var project = await _projectsService.GetProjectById(user, id);

            if (project == null)
            {
                var problem = new ProblemDetails
                {
                    Instance = HttpContext.Request.Path,
                    Status = StatusCodes.Status404NotFound,
                    Type = $"https://httpstatuses.com/404",
                    Title = "Not found",
                    Detail = $"Project {id} does not exist or you do not have access to it."
                };

                return NotFound(problem);
            }
            return Ok(project);
        }
    }
}