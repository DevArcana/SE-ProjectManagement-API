using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
                // TODO: Suggest a better strategy
                return null;
            }
            
            project = new Project(manager, name);

            _context.Add(project);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("{user} created project {projectName}", project.Manager.UserName, project.Name);
            
            return _mapper.Map<ProjectDto>(project);
        }
    }
}