using ProjectManagement.API.Domain.Users.Entities;

namespace ProjectManagement.API.Domain.Projects.Entities
{
    public class UserProjectAccess
    {
        public ApplicationUser User { get; private set; }
        public Project Project { get; private set; }

        private UserProjectAccess()
        {
            
        }

        private UserProjectAccess(ApplicationUser u, Project p)
        {
            User = u;
            Project = p;
        }
        
    }
}