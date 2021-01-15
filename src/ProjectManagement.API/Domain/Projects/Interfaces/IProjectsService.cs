using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagement.API.Domain.Projects.Entities;
using ProjectManagement.API.Domain.Users.Entities;

namespace ProjectManagement.API.Domain.Projects.Interfaces
{
    public interface IProjectsService
    {
        Task<Project> CreateProjectAsync(ApplicationUser manager, string name, CancellationToken cancellationToken = default);
        Task<Project> GetProjectByIdAsync(ApplicationUser user, long id, CancellationToken cancellationToken = default);
        IQueryable<Project> GetProjects(ApplicationUser user);
        Task<Project> DeleteProjectAsync(ApplicationUser user, long projectId, CancellationToken cancellationToken = default);
        Task<Project> UpdateProjectAsync(ApplicationUser user, long projectId, string name, CancellationToken cancellationToken = default);

        Task<UserProjectAccess> AddCollaboratorAsync(ApplicationUser user, long projectId, string name,
            CancellationToken cancellationToken = default);
        Task<UserProjectAccess> GetCollaboratorByNameAsync(ApplicationUser user, long projectId, string name,
            CancellationToken cancellationToken = default);
        IQueryable<UserProjectAccess> GetCollaborators(ApplicationUser user, long projectId);
        Task<UserProjectAccess> DeleteCollaboratorAsync(ApplicationUser user, long projectId, string name);
    }
}