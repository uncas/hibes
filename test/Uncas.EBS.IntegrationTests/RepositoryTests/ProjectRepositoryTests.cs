using System.Linq;
using NUnit.Framework;
using Uncas.EBS.Domain;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;

namespace Uncas.EBS.IntegrationTests.RepositoryTests
{
    [TestFixture]
    public class ProjectRepositoryTests
    {
        private IProjectRepository projRepo
            = TestApp.Repositories.ProjectRepository;

        private IIssueRepository issueRepo
            = TestApp.Repositories.IssueRepository;

        [Test]
        public void GetProjects()
        {
            var project = projRepo
                .GetFirstProject();
            int projectId = project.ProjectId;

            // Setting up:
            Issue issue = Issue.ConstructIssue
                (projectId
                , "GetProjects"
                , Status.Open
                , 1);
            issueRepo.InsertIssue(issue);

            // Testing:
            var projects = projRepo.GetProjects(new Paging());

            // Checking:
            Assert.IsNotNull(projects);
            Assert.Greater(projects.Count, 0);
            Assert.IsTrue(projects
                .Any(p => p.ProjectName.Equals(project.ProjectName)));
        }
    }
}