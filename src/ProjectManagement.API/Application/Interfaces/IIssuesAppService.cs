﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagement.API.Domain.Issues.Model;
using ProjectManagement.API.Domain.Users.Entities;

namespace ProjectManagement.API.Application.Interfaces
{
    public interface IIssuesAppService
    {
        Task<IssueDto> CreateIssueAsync(ApplicationUser user, long projectId, string name, string description, CancellationToken cancellationToken = default);
        Task<IssueDto> GetIssueByIdAsync(ApplicationUser user, long issueId, CancellationToken cancellationToken = default);
        Task<IEnumerable<IssueDto>> GetIssuesForProjectAsync(ApplicationUser user, long projectId, CancellationToken cancellationToken = default);
    }
}