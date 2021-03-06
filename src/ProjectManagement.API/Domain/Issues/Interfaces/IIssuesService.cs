﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagement.API.Domain.Issues.Entities;
using ProjectManagement.API.Domain.Users.Entities;

namespace ProjectManagement.API.Domain.Issues.Interfaces
{
    public interface IIssuesService
    {
        Task<Issue> CreateIssueAsync(ApplicationUser user, long projectId, string name, string description, CancellationToken cancellationToken = default);
        Task<Issue> GetIssueByIdAsync(ApplicationUser user, long projectId, long issueId,
            CancellationToken cancellationToken = default);
        Task<IQueryable<Issue>> GetIssuesForProjectAsync(ApplicationUser user, long projectId, CancellationToken cancellationToken = default);

        Task<Issue> UpdateIssueAsync(ApplicationUser user, long projectId, long issueId, string name,
            string description, bool closed, Status status, CancellationToken cancellationToken = default);
        Task<Issue> DeleteIssueAsync(ApplicationUser user, long projectId, long issueId, CancellationToken cancellationToken = default);
        Task<IEnumerable<ApplicationUser>> GetAssignableUsers(ApplicationUser user, long projectId,
            CancellationToken cancellationToken = default);
        Task<bool> AssignUserToIssue(ApplicationUser user, long projectId, long issueId, string username,
            CancellationToken cancellationToken = default);
    }
}