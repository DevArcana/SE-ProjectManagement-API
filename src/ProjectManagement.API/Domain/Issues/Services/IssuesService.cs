﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
            var project = await _projectsService.GetProjectById(user, projectId, cancellationToken);

            if (project == null)
            {
                return null;
            }

            var issue = new Issue(project, name, description);
            
            _context.Issues.Add(issue);
            await _context.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation("{user} created issue {issueName} for project id {projectId} [{projectName}]",
                user.UserName, issue.Name, project.Id, project.Name);

            return issue;
        }

        public async Task<Issue> GetIssueByIdAsync(ApplicationUser user, long issueId, CancellationToken cancellationToken = default)
        {
            var issue = await _context.Issues
                .Include(x => x.Project)
                .FirstOrDefaultAsync(x => x.Id == issueId, cancellationToken);

            if (issue == null)
            {
                return null;
            }

            var project = await _projectsService.GetProjectById(user, issue.Project.Id, cancellationToken);

            // Verify the user has access to the project in the first place
            return project == null ? null : issue;
        }

        public async Task<IQueryable<Issue>> GetIssuesForProjectAsync(ApplicationUser user, long projectId, CancellationToken cancellationToken = default)
        {
            var project = await _projectsService.GetProjectById(user, projectId, cancellationToken);

            if (project == null)
            {
                return null;
            }

            return _context.Issues
                .Where(x => x.Project.Id == projectId);
        }
    }
}