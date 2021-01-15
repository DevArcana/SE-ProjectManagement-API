using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectManagement.API.Common.Exceptions;
using ProjectManagement.API.Domain.Projects.Entities;
using ProjectManagement.API.Domain.Projects.Interfaces;
using ProjectManagement.API.Domain.Users.Entities;
using ProjectManagement.API.Infrastructure.Persistence;

namespace ProjectManagement.API.Domain.Projects.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly ILogger<ProjectsService> _logger;
        private readonly ApplicationDbContext _context;

        public ProjectsService(ILogger<ProjectsService> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Project> CreateProjectAsync(ApplicationUser manager, string name, CancellationToken cancellationToken = default)
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
            
            return project;
        }

        public async Task<Project> GetProjectByIdAsync(ApplicationUser user, long id, CancellationToken cancellationToken = default)
        {
            // TODO: Add access lists instead of relying on being a manager
            var project = await _context.Projects
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && x.Manager.Id == user.Id, cancellationToken);

            return project;
        }
        public IQueryable<Project> GetProjects(ApplicationUser user)
        {
            return _context.Projects
                .AsNoTracking()
                .Where(x => x.Manager.Id == user.Id);
        }

        public async Task<Project> UpdateProjectAsync(ApplicationUser user, long projectId, string name,
            CancellationToken cancellationToken = default)
        {
            var project = await _context.Projects
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == projectId && x.Manager.Id == user.Id, cancellationToken);

            if (project == null)
            {
                return null;
            }

            _context.Attach(project);   
            if (!string.IsNullOrWhiteSpace(name))
            {
                project.Rename(name);
            }
            await _context.SaveChangesAsync(cancellationToken);
            
            return project;
        }
        public async Task<Project> DeleteProjectAsync(ApplicationUser user, long projectId, CancellationToken cancellationToken = default)
        {
            var project = await _context.Projects
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == projectId && x.Manager.Id == user.Id, cancellationToken);

            if (project == null)
            {
                return null;
            }

            _context.Attach(project);
            _context.Entry(project).State = EntityState.Deleted;
            await _context.SaveChangesAsync(cancellationToken);
            
            return project;
        }
        public Task<UserProjectAccess> AddCollaboratorAsync(ApplicationUser user, long projectId, string name, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<UserProjectAccess> GetCollaborators(ApplicationUser user, long projectId)
        {
            return _context.UserProjectAccesses
                .AsNoTracking()
                .Where(x => x.UserId == user.UserName && x.ProjectId == projectId);
        }

        public Task<UserProjectAccess> DeleteCollaboratorAsync(ApplicationUser user, long projectId, string name)
        {
            throw new System.NotImplementedException();
        }
    }
}