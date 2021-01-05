using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagement.API.Domain.Projects.Entities;
using ProjectManagement.API.Domain.Projects.Models;
using ProjectManagement.API.Domain.Users.Entities;

namespace ProjectManagement.API.Domain.Projects.Interfaces
{
    public interface IProjectsService
    {
        Task<ProjectDto> CreateProjectAsync(ApplicationUser manager, string name, CancellationToken cancellationToken = default);
        Task<ProjectDto> GetProjectById(ApplicationUser user, long id, CancellationToken cancellationToken = default);
        Task<List<ProjectDto>> GetProjects(ApplicationUser user, CancellationToken cancellationToken = default);
    }
}