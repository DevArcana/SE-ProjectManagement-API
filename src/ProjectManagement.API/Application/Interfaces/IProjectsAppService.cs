using System.Threading;
using System.Threading.Tasks;
using ProjectManagement.API.Application.Models;
using ProjectManagement.API.Domain.Users.Entities;

namespace ProjectManagement.API.Application.Interfaces
{
    public interface IProjectsAppService
    {
        Task<ProjectDto> CreateProjectAsync(ApplicationUser manager, string name, CancellationToken cancellationToken = default);
        Task<ProjectDto> GetProjectById(ApplicationUser user, long id, CancellationToken cancellationToken = default);
    }
}