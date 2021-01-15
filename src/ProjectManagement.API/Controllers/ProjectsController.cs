﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.API.Application.Interfaces;
using ProjectManagement.API.Application.Models;
using ProjectManagement.API.Domain.Projects.Interfaces;
using ProjectManagement.API.Domain.Users.Entities;

namespace ProjectManagement.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsAppService _projectsAppService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectsController(IProjectsAppService projectsService, UserManager<ApplicationUser> userManager)
        {
            _projectsAppService = projectsService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto dto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var project = await _projectsAppService.CreateProjectAsync(user, dto.Name);
            return CreatedAtAction(nameof(GetProjectById), new {Id = project.Id}, project);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(long id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var project = await _projectsAppService.GetProjectByIdAsync(user, id);

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
        
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var projects = await _projectsAppService.GetProjectsAsync(user);
            
            return Ok(projects);
        }

        [HttpDelete("{projectId}")]
        public async Task<IActionResult> DeleteProject(long projectId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var project = await _projectsAppService.DeleteProjectAsync(user, projectId);

            if (project == null)
            {
                var problem = new ProblemDetails
                {
                    Instance = HttpContext.Request.Path,
                    Status = StatusCodes.Status404NotFound,
                    Type = $"https://httpstatuses.com/404",
                    Title = "Not found",
                    Detail = $"Project {projectId} does not exist or you do not have access to it."
                };

                return NotFound(problem);
            }
            return Ok(project);
        }
        
        [HttpPut]
        public async Task<IActionResult> EditProject([FromBody] ProjectDto dto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var project = await _projectsAppService.UpdateProjectAsync(user, dto.Id, dto.Name);
            if (project == null)
            {
                var problem = new ProblemDetails
                {
                    Instance = HttpContext.Request.Path,
                    Status = StatusCodes.Status404NotFound,
                    Type = $"https://httpstatuses.com/404",
                    Title = "Not found",
                    Detail = $"Project {dto.Id} does not exist or you do not have access to it."
                };
                return NotFound(problem);
            }
            return Ok(project);
        }

        [HttpGet("{projectId}/collaborators")]
        public async Task<IActionResult> GetCollaborators(long projectId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var projects = await _projectsAppService.GetCollaboratorsAsync(user, projectId);
            return Ok(projects);
        }

        [HttpPost("{projectId}/collaborators")]
        public async Task<IActionResult> AddCollaborator(long projectId, [FromBody] CollaboratorDto dto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var collaborator = await _projectsAppService.AddCollaboratorAsync(user, projectId, dto.UserId);
            return CreatedAtAction(nameof(GetCollaboratorByName), new {Id = collaborator.UserId}, collaborator);
        }

        [HttpGet("{projectId}/collaborators/{userName}")]
        public async Task<IActionResult> GetCollaboratorByName(long projectId, string userName)
        {
            return Ok();
        }
        
        [HttpDelete("{id}/collaborators")]
        public async Task<IActionResult> DeleteCollaborator(string name)
        {
            return Ok();
        }
    }
}