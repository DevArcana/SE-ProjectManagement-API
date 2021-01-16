using System;
using FluentAssertions;
using NUnit.Framework;
using ProjectManagement.API.Common.Exceptions;
using ProjectManagement.API.Domain.Issues.Entities;
using ProjectManagement.API.Domain.Projects.Entities;
using ProjectManagement.API.Domain.Users.Entities;

namespace ProjectManagement.API.Test.Domain
{
    public class IssueShould
    {
        [Test]
        public void CanOnlyBeCreatedWithProject()
        {
            Action action = () =>
            {
                var issue = new Issue(null, "project", "description");
            };

            action.Should().Throw<PropertyValidationException>().And.PropertyName.Should().Be("project");
        }
        
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void CanOnlyBeCreatedWithValidName(string name)
        {
            Action action = () =>
            {
                var issue = new Issue(new Project(new ApplicationUser(), "project"), name, "description");
            };

            action.Should().Throw<PropertyValidationException>().And.PropertyName.Should().Be("name");
        }
        
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void EnsuresValidName(string name)
        {
            Action action = () =>
            {
                var issue = new Issue(new Project(new ApplicationUser(), "project"), "name", "description");
                issue.Rename(name);
            };

            action.Should().Throw<PropertyValidationException>().And.PropertyName.Should().Be("name");
        }
        
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void CanBeCreatedWithNoDescription(string description)
        {
            var issue = new Issue(new Project(new ApplicationUser(), "project"), "name", description);
        }

        [Test]
        public void SetDescriptionToNullWhenEmpty()
        {
            var issue = new Issue(new Project(new ApplicationUser(), "project"), "name", "description");
            issue.Description.Should().NotBe(null);

            issue.ChangeDescription(null);
            issue.Description.Should().Be(null);
            
            issue.ChangeDescription("");
            issue.Description.Should().Be(null);
            
            issue.ChangeDescription(" ");
            issue.Description.Should().Be(null);
        }

        [Test]
        public void HaveDefaultStatusAsDoing()
        {
            var issue = new Issue(new Project(new ApplicationUser(), "project"), "name", "description");
            issue.Status.Should().Be(Status.ToDo);
        }

        [Test]
        [TestCase(Status.Done)]
        [TestCase(Status.Doing)]
        [TestCase(Status.ToDo)]
        public void SetStatusCorrectly(Status status)
        {
            var issue = new Issue(new Project(new ApplicationUser(), "project"), "name", "description");
            
            issue.SetStatus(status);
            issue.Status.Should().Be(status);
        }

        [Test]
        public void CloseCorrectly()
        {
            var issue = new Issue(new Project(new ApplicationUser(), "project"), "name", "description");

            issue.Closed.Should().Be(false);
            issue.Close();
            issue.Closed.Should().Be(true);
        }

        [Test]
        public void AllowSettingAssigneeToNull()
        {
            var issue = new Issue(new Project(new ApplicationUser(), "project"), "name", "description");

            issue.Assignee.Should().Be(null);
            issue.AssignUser(new ApplicationUser());
            issue.Assignee.Should().NotBe(null);
            issue.AssignUser(null);
            issue.Assignee.Should().Be(null);
        }
    }
}