using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagement.API.Application.Models;
using ProjectManagement.API.Domain.Users.Entities;

namespace ProjectManagement.API.Application.Interfaces
{
    public interface IProjectsAppService
    {
        Task<ProjectDto> CreateProjectAsync(ApplicationUser manager, string name, CancellationToken cancellationToken = default);
        Task<ProjectDto> GetProjectByIdAsync(ApplicationUser user, long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProjectDto>> GetProjectsAsync(ApplicationUser user, CancellationToken cancellationToken = default);
        Task<ProjectDto> DeleteProjectAsync(ApplicationUser user, long projectId,
            CancellationToken cancellationToken = default);
        Task<ProjectDto> UpdateProjectAsync(ApplicationUser user, long projectId, string name, CancellationToken cancellationToken = default);

    }
}