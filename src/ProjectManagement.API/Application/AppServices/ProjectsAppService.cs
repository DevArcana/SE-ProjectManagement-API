using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjectManagement.API.Application.Interfaces;
using ProjectManagement.API.Domain.Projects.Interfaces;
using ProjectManagement.API.Domain.Projects.Models;
using ProjectManagement.API.Domain.Users.Entities;

namespace ProjectManagement.API.Application.AppServices
{
    public class ProjectsAppService : IProjectsAppService
    {
        private readonly ILogger<ProjectsAppService> _logger;
        private readonly IMapper _mapper;
        private readonly IProjectsService _projectsService;

        public ProjectsAppService(ILogger<ProjectsAppService> logger, IMapper mapper, IProjectsService projectsService)
        {
            _logger = logger;
            _mapper = mapper;
            _projectsService = projectsService;
        }

        public async Task<ProjectDto> CreateProjectAsync(ApplicationUser manager, string name, CancellationToken cancellationToken = default)
        {
            var project = await _projectsService.CreateProjectAsync(manager, name, cancellationToken);
            return project == null ? null : _mapper.Map<ProjectDto>(project);
        }

        public async Task<ProjectDto> GetProjectById(ApplicationUser user, long id, CancellationToken cancellationToken = default)
        {
            var project = await _projectsService.GetProjectById(user, id, cancellationToken);
            return project == null ? null : _mapper.Map<ProjectDto>(project);
        }
    }
}