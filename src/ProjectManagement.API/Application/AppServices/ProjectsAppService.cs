﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectManagement.API.Application.Interfaces;
using ProjectManagement.API.Application.Models;
using ProjectManagement.API.Domain.Projects.Interfaces;
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

        public async Task<ProjectDto> GetProjectByIdAsync(ApplicationUser user, long id, CancellationToken cancellationToken = default)
        {
            var project = await _projectsService.GetProjectByIdAsync(user, id, cancellationToken);
            return project == null ? null : _mapper.Map<ProjectDto>(project);
        }

        public async Task<IEnumerable<ProjectDto>> GetProjectsAsync(ApplicationUser user, CancellationToken cancellationToken = default)
        {
            return await _projectsService
                .GetProjects(user)
                .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }


        public async Task<ProjectDto> DeleteProjectAsync(ApplicationUser user, long projectId, CancellationToken cancellationToken = default)
        {
            var project = await _projectsService.DeleteProjectAsync(user, projectId, cancellationToken);
            return project == null ? null : _mapper.Map<ProjectDto>(project);
        }
        
        public async Task<ProjectDto> UpdateProjectAsync(ApplicationUser user, long projectId, string name,
            CancellationToken cancellationToken = default)
        {
            var project = await _projectsService.UpdateProjectAsync(user, projectId, name, cancellationToken);
            return project == null ? null : _mapper.Map<ProjectDto>(project);
        }

        public Task<IEnumerable<CollaboratorDto>> GetCollaborators(ApplicationUser user, long projectId, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<CollaboratorDto> AddCollaborator(ApplicationUser user, long projectId, string userName,
            CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<CollaboratorDto> DeleteCollaborator(ApplicationUser user, long projectId, string userName,
            CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}