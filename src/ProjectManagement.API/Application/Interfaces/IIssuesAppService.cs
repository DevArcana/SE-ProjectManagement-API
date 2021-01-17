using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagement.API.Application.Models;
using ProjectManagement.API.Domain.Issues.Entities;
using ProjectManagement.API.Domain.Users.Entities;

namespace ProjectManagement.API.Application.Interfaces
{
    public interface IIssuesAppService
    {
        Task<IssueDto> CreateIssueAsync(ApplicationUser user, long projectId, string name, string description, CancellationToken cancellationToken = default);
        Task<IssueDto> GetIssueByIdAsync(ApplicationUser user, long projectId, long issueId, CancellationToken cancellationToken = default);
        Task<IEnumerable<IssueDto>> GetIssuesForProjectAsync(ApplicationUser user, long projectId, CancellationToken cancellationToken = default);
        Task<IssueDto> UpdateIssueAsync(ApplicationUser user, long projectId, long issueId, string name, 
            string description, bool closed, Status status, CancellationToken cancellationToken = default);
        Task<IssueDto> DeleteIssueAsync(ApplicationUser user, long projectId, long issueId,
            CancellationToken cancellationToken = default);
        Task<IEnumerable<AssignableUserDto>> GetAssignableUsers(ApplicationUser user, long projectId,
            CancellationToken cancellationToken = default);
        Task<bool> AssignUserToIssue(ApplicationUser user, long projectId, long issueId, string username,
            CancellationToken cancellationToken = default);

    }
}