using ProjectManagement.API.Common.Entities;
using ProjectManagement.API.Common.Exceptions;
using ProjectManagement.API.Domain.Projects.Entities;
using ProjectManagement.API.Domain.Users.Entities;

namespace ProjectManagement.API.Domain.Issues.Entities
{
    public class Issue : BaseEntity
    {
        public Project Project { get; }
        
        public string Name { get; private set; }
        public string Description { get; private set; }

        private Issue()
        {
            // Needed by EF Core
        }

        public Issue(Project project, string name, string description)
        {
            Project = project;
            Rename(name);
            ChangeDescription(description);
        }

        public void Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new PropertyValidationException(nameof(name), "Issue's name must not be empty!");
            }
            
            Name = name;
        }

        public void ChangeDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new PropertyValidationException(nameof(description), "Issue's description must not be empty!");
            }
            
            Description = description;
        }
    }
}