using System;
using System.Diagnostics;
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
            int projectId = _projRepo
                .GetProjects().FirstOrDefault()
                .ProjectId;

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
                .Any(p => p.ProjectName.Equals(issue.ProjectName)));
        }

        [Test]
        public void GetProjectEstimate_NonExistingProject()
        {
            var projectEstimate = _projRepo.GetProjectEvaluation
                (0, 0, 100, 100);
            Trace.WriteLine(projectEstimate);
            foreach (var ie in projectEstimate.GetIssueEvaluations())
            {
                Trace.WriteLine(ie.ToString());
                break;
            }
        }

        [Test]
        public void GetProjectEstimate_FirstProject()
        {
            var projId = _projRepo.GetProjects()
                .Min(p => p.ProjectId);
            var projectEstimate = _projRepo.GetProjectEvaluation
                (projId, null, 100, 100);
            Trace.WriteLine(projectEstimate);
            foreach (var ie in projectEstimate.GetIssueEvaluations())
            {
                Trace.WriteLine(ie.ToString());
                break;
            }
        }

        [Test]
        public void GetProjectEstimate_OneProjectAtATime()
        {
            int maxNumberOfHistoricalData = 50;
            RunSimulation(10, maxNumberOfHistoricalData);
            for (int numberOfSimulations = 100
                ; numberOfSimulations <= 110 * 1000
                ; numberOfSimulations *= 2)
            {
                long startTicks = DateTime.Now.Ticks;
                RunSimulation(numberOfSimulations
                    , maxNumberOfHistoricalData);
                long endTicks = DateTime.Now.Ticks;
                Trace.WriteLine(string.Format("{0}: {1:N0}"
                    , numberOfSimulations
                    , TimeSpan.FromTicks(endTicks - startTicks)
                    .TotalMilliseconds));
            }
        }

        private void RunSimulation(int numberOfSimulations, int maxNumberOfHistoricalData)
        {
            foreach (var project in _projRepo.GetProjects())
            {
                var projId = project.ProjectId;
                var projectEstimate = _projRepo.GetProjectEvaluation
                    (projId
                    , null
                    , numberOfSimulations
                    , maxNumberOfHistoricalData);
                //Trace.WriteLine(projectEstimate);
                foreach (var ie in projectEstimate.GetIssueEvaluations())
                {
                    //Trace.WriteLine(ie.ToString());
                    break;
                }
            }
        }
    }
}