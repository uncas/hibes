using System.Linq;
using NUnit.Framework;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;

namespace Uncas.EBS.IntegrationTests.RepositoryTests
{
    [TestFixture]
    public class ProjectRepositoryTests
    {
        private IProjectRepository _projRepo
            = TestApp.Repositories.ProjectRepository;

        private IIssueRepository _issueRepo
            = TestApp.Repositories.IssueRepository;

        [Test]
        public void GetProjects()
        {
            var project = _projRepo
                .GetProjects().FirstOrDefault();
            int projectId = project.ProjectId;

            // Setting up:
            Issue issue = new Issue
            {
                RefProjectId = projectId,
                Title = "GetProjects"
            };
            _issueRepo.InsertIssue(issue);

            // Testing:
            var projects = _projRepo.GetProjects();

            // Checking:
            Assert.IsNotNull(projects);
            Assert.Greater(projects.Count, 0);
            Assert.IsTrue(projects
                .Any(p => p.ProjectName.Equals(project.ProjectName)));
        }
    }
}