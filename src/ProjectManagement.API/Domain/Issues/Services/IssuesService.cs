﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectManagement.API.Domain.Issues.Entities;
using ProjectManagement.API.Domain.Issues.Interfaces;
using ProjectManagement.API.Domain.Projects.Interfaces;
using ProjectManagement.API.Domain.Users.Entities;
using ProjectManagement.API.Infrastructure.Persistence;

namespace ProjectManagement.API.Domain.Issues.Services
{
    public class IssuesService : IIssuesService
    {
        private readonly ILogger<IssuesService> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IProjectsService _projectsService;

        public IssuesService(ILogger<IssuesService> logger, ApplicationDbContext context, IProjectsService projectsService)
        {
            _logger = logger;
            _context = context;
            _projectsService = projectsService;
        }

        public async Task<Issue> CreateIssueAsync(ApplicationUser user, long projectId, string name, string description, CancellationToken cancellationToken = default)
        {
            var project = await _projectsService.GetProjectByIdAsync(user, projectId, cancellationToken);

            if (project == null)
            {
                return null;
            }

            project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == project.Id, cancellationToken);

            var issue = new Issue(project, name, description);
            
            _context.Issues.Add(issue);
            await _context.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation("{user} created issue {issueName} for project id {projectId} [{projectName}]",
                user.UserName, issue.Name, project.Id, project.Name);

            return issue;
        }

        public async Task<Issue> GetIssueByIdAsync(ApplicationUser user, long projectId, long issueId, CancellationToken cancellationToken = default)
        {
            var issue = await _context.Issues
                .Include(x => x.Project)
                .ThenInclude(x => x.Manager)
                .Include(x => x.Assignee)
                .FirstOrDefaultAsync(x => x.Id == issueId, cancellationToken);

            if (issue == null)
            {
                return null;
            }

            if (projectId != issue.Project.Id)
            {
                // No bueno if user wants to bypass the access check
                return null;
            }

            var project = await _projectsService.GetProjectByIdAsync(user, projectId, cancellationToken);

            // Verify the user has access to the project in the first place
            return project == null ? null : issue;
        }

        public async Task<IQueryable<Issue>> GetIssuesForProjectAsync(ApplicationUser user, long projectId, CancellationToken cancellationToken = default)
        {
            var project = await _projectsService.GetProjectByIdAsync(user, projectId, cancellationToken);

            if (project == null)
            {
                return null;
            }

            return _context.Issues
                .AsNoTracking()
                .Include(x => x.Project)
                .ThenInclude(x => x.Manager)
                .Include(x => x.Assignee)
                .Where(x => x.Project.Id == projectId)
                .OrderBy(x => x.Id);
        }

        public async Task<Issue> UpdateIssueAsync(ApplicationUser user, long projectId, long issueId, string name,
            string description, bool closed, Status status, CancellationToken cancellationToken = default)
        {
            var project = await _projectsService.GetProjectByIdAsync(user, projectId, cancellationToken);

            if (project == null)
            {
                return null;
            }

            _context.Attach(project);

            var issue = await _context.Issues
                .Include(x => x.Project)
                .FirstOrDefaultAsync(x => x.Id == issueId, cancellationToken);

            if (issue == null)
            {
                return null;
            }

            _context.Issues.Attach(issue);

            if (projectId != issue.Project.Id)
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                issue.Rename(name);
            }

            issue.ChangeDescription(description);
            if (closed)
            {
                issue.Close();
            }

            issue.SetStatus(status);
          
            await _context.SaveChangesAsync(cancellationToken);
            
            return issue;
        }

        public async Task<Issue> DeleteIssueAsync(ApplicationUser user, long projectId, long issueId,
            CancellationToken cancellationToken = default)
        {
            var project = await _projectsService.GetProjectByIdAsync(user, projectId, cancellationToken);

            if (project == null)
            {
                return null;
            }

            _context.Attach(project);
            var issue = await _context.Issues
                .Include(x => x.Project)
                .FirstOrDefaultAsync(x => x.Id == issueId, cancellationToken);
           
            if (issue == null)
            {
                return null;
            }
            
            _context.Issues.Attach(issue);
            
            if (projectId != issue.Project.Id)
            {
                return null;
            }

            _context.Entry(issue).State = EntityState.Deleted;
            await _context.SaveChangesAsync(cancellationToken);

            return issue;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAssignableUsers(ApplicationUser user, long projectId, CancellationToken cancellationToken = default)
        {
            var project = await _projectsService.GetProjectByIdAsync(user, projectId, cancellationToken);

            if (project == null)
            {
                return null;
            }
            
            // TODO: Respect the access table
            var users = await _context.UserProjectAccess
                .Where(x => x.ProjectId == projectId)
                .Include(x => x.User)
                .Select(x => x.User)
                .ToListAsync(cancellationToken);

            users = users
                .Append(project.Manager)
                .ToList();

            return users.AsQueryable();
        }

        public async Task<bool> AssignUserToIssue(ApplicationUser user, long projectId, long issueId, string username,
            CancellationToken cancellationToken = default)
        {
            var issue = await GetIssueByIdAsync(user, projectId, issueId, cancellationToken);

            if (issue == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                issue.AssignUser(null);
            }
            else
            {
                var users = await _context.UserProjectAccess
                    .Where(x => x.ProjectId == projectId)
                    .Include(x => x.User)
                    .Select(x => x.User)
                    .ToListAsync(cancellationToken);

                users = users
                    .Append(user)
                    .Where(x => x.Id != issue.Assignee?.Id).ToList();
            
                // TODO: Respect the access table
                var assignee = users.FirstOrDefault(x => x.UserName == username);

                if (assignee == null)
                {
                    return false;
                }

                issue.AssignUser(assignee);
            }
            
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
