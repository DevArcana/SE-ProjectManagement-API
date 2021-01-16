using System;
using FluentAssertions;
using NUnit.Framework;
using ProjectManagement.API.Common.Exceptions;
using ProjectManagement.API.Domain.Projects.Entities;
using ProjectManagement.API.Domain.Users.Entities;

namespace ProjectManagement.API.Test.Domain
{
    public class ProjectShould
    {
        [Test]
        public void CanOnlyBeCreatedWithManager()
        {
            Action action = () =>
            {
                var project = new Project(null, "project");
            };

            action.Should().Throw<PropertyValidationException>().And.PropertyName.Should().Be("manager");
        }
        
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void CanOnlyBeCreatedWithValidName(string name)
        {
            Action action = () =>
            {
                var project = new Project(new ApplicationUser(), name);
            };

            action.Should().Throw<PropertyValidationException>().And.PropertyName.Should().Be("name");
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void EnsureValidNameDuringRename(string name)
        {
            Action action = () =>
            {
                var project = new Project(new ApplicationUser(), "project");
                project.Rename(name);
            };
            
            action.Should().Throw<PropertyValidationException>().And.PropertyName.Should().Be("name");
        }
        
        [Test]
        public void EnsureManagerCantBeSetToNull()
        {
            Action action = () =>
            {
                var project = new Project(new ApplicationUser(), "project");
                project.AssignManager(null);
            };

            action.Should().Throw<PropertyValidationException>().And.PropertyName.Should().Be("manager");
        }
    }
}