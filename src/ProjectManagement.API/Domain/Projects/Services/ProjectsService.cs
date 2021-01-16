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
            var projects = GetProjects(user);
            var project = await projects
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return project;
        }
        public IQueryable<Project> GetProjects(ApplicationUser user)
        {
            var managed = _context.Projects
                .AsNoTracking()
                .Where(x => x.Manager.Id == user.Id);
            var collaborated = _context.UserProjectAccess
                .AsNoTracking()
                .Where(x => x.UserId == user.Id).Select(x=>x.Project);
            return managed.Concat(collaborated);
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
        public async Task<UserProjectAccess> AddCollaboratorAsync(ApplicationUser user, long projectId, string name, CancellationToken cancellationToken = default)
        {
            var collaborator = await  _context.UserProjectAccess.AsNoTracking()
                    .FirstOrDefaultAsync(x =>  x.User.UserName == name && x.ProjectId == projectId, cancellationToken);
            if (collaborator != null)
            {
                throw new EntityAlreadyExistsException("User is already collaborator", $"User {user.UserName} is already collaborator of project {projectId}");
            }
            
            var project = await _context.Projects
                .Include(x => x.Manager)
                .FirstOrDefaultAsync(x => x.Id == projectId && x.Manager.Id == user.Id, cancellationToken);
            if (project == null)
            {
                return null;
            }
            
            var collabUser = await _context.Users
                .FirstOrDefaultAsync(x => x.UserName == name && x.Id!=project.Manager.Id, cancellationToken);
            
            if (collabUser == null)
            {
                return null;
            }
            
            collaborator = new UserProjectAccess(collabUser, project);
            _context.UserProjectAccess.Add(collaborator);
       
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"{name} added as collaborator to  project {project.Name}", project.Manager.UserName, project.Name);
            
            return collaborator;
        }

        public async Task<UserProjectAccess> GetCollaboratorByNameAsync(ApplicationUser user, long projectId,
            string name, CancellationToken cancellationToken = default)
        {
            var collaborator = await _context.UserProjectAccess
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.User.UserName == name && x.ProjectId == projectId, cancellationToken);

            return collaborator;
        }

        public IQueryable<UserProjectAccess> GetCollaborators(ApplicationUser user, long projectId)
        {
            return _context.UserProjectAccess
                .AsNoTracking()
                .Where(x => x.User.UserName != user.UserName && x.ProjectId == projectId);
        }

        public async Task<UserProjectAccess> DeleteCollaboratorAsync(ApplicationUser user, long projectId, string name, CancellationToken cancellationToken)
        {
            var collaborator = await _context.UserProjectAccess
                .FirstOrDefaultAsync(x => x.User.UserName == name && x.ProjectId == projectId && x.Project.Manager.Id == user.Id, cancellationToken);

            if (collaborator == null)
            {
                return null;
            }

            _context.Entry(collaborator).State = EntityState.Deleted;
            await _context.SaveChangesAsync(cancellationToken);
            
            return collaborator;
        }
    }
}