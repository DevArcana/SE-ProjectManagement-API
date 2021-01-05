using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectManagement.API.Common.Exceptions;
using ProjectManagement.API.Domain.Projects.Entities;
using ProjectManagement.API.Domain.Projects.Interfaces;
using ProjectManagement.API.Domain.Projects.Models;
using ProjectManagement.API.Domain.Users.Entities;
using ProjectManagement.API.Infrastructure.Persistence;

namespace ProjectManagement.API.Domain.Projects.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly ILogger<ProjectsService> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProjectsService(ILogger<ProjectsService> logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProjectDto> CreateProjectAsync(ApplicationUser manager, string name, CancellationToken cancellationToken = default)
        {
            var project = await _context.Projects.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name, cancellationToken: cancellationToken);

            if (project != null)
            {
                throw new EntityAlreadyExistsException("Project already exists", $"Project {name} already exists");
            }
            
            project = new Project(manager, name);

            _context.Add(project);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("{user} created project {projectName}", project.Manager.UserName, project.Name);
            
            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<ProjectDto> GetProjectById(ApplicationUser user, long id, CancellationToken cancellationToken = default)
        {
            // TODO: Add access lists instead of relying on being a manager
            var project = await _context.Projects
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && x.Manager.Id == user.Id, cancellationToken);

            return project == null ? null : _mapper.Map<ProjectDto>(project);
        }
        public async Task<List<ProjectDto>> GetAllProjects(ApplicationUser user, CancellationToken cancellationToken = default)
        {
            var projects = await _context.Projects
                .AsNoTracking()
                .Where(x => x.Manager.Id == user.Id)
                .ToListAsync(cancellationToken);

            return  _mapper.Map<List<ProjectDto>>(projects);
        }
    }
}