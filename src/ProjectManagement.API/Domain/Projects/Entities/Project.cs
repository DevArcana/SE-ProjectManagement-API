using System;
using ProjectManagement.API.Common.Entities;
using ProjectManagement.API.Domain.Users.Entities;

namespace ProjectManagement.API.Domain.Projects.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; private set; }
        public ApplicationUser Manager { get; private set; }
        
        private Project()
        {
            // Needed by EF Core
        }

        public void AssignManager(ApplicationUser manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager), "Manager must not be null!");
            }

            Manager = manager;
        }

        public void Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Project's name must not be empty!");
            }

            Name = name;
        }

        public Project(ApplicationUser manager, string name)
        {
            AssignManager(manager);
            Rename(name);
        }
    }
}