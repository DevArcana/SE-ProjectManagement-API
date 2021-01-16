using ProjectManagement.API.Common.Exceptions;
using ProjectManagement.API.Domain.Users.Entities;

namespace ProjectManagement.API.Domain.Projects.Entities
{
    public class UserProjectAccess
    {
        public ApplicationUser User { get; }
        public string UserId { get; }
        public Project Project { get; }
        public long ProjectId { get; }

        private UserProjectAccess()
        {
            
        }

        public UserProjectAccess(ApplicationUser user, Project project)
        {
            if (user.Id == project.Manager.Id)
            {
                throw new PropertyValidationException(nameof(user),
                    "This user is the manager of this project, can't grant access.");
            }

            User = user;
            UserId = user.Id;

            Project = project;
            ProjectId = project.Id;
        }
        
    }
}