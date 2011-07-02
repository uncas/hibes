using System;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using Uncas.EBS.ApplicationServices;
using Uncas.EBS.Domain;
using Uncas.EBS.Domain.Repository;

namespace Uncas.EBS.IntegrationTests.ApplicationServicesTests
{
    [TestFixture]
    public class ProjectServiceTests
    {
        private const double StandardNumberOfHoursPerDay = 7.5d;

        private ProjectService projectService
             = new ProjectService(TestApp.Repositories);

        private IProjectRepository projectRepository
            = TestApp.Repositories.ProjectRepository;

        [Test]
        public void GetProjectEvaluation_All_PositiveAverage()
        {
            var projectEvaluation
                = projectService.GetTeamEvaluation
                    (null
                    , null
                    , 100
                    , 10
                    , StandardNumberOfHoursPerDay)
                    .TotalEvaluation;

            Assert.IsNotNull(projectEvaluation.Average);
            Assert.Less(0d, projectEvaluation.Average);
        }

        [Test]
        public void GetProjectEstimate_NonExistingProject()
        {
            var projectEstimate
                = projectService.GetTeamEvaluation
                (0
                , 0
                , 100
                , 100
                , StandardNumberOfHoursPerDay)
                .TotalEvaluation;
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
            var projId = projectRepository.GetProjects(new Paging())
                .Min(p => p.ProjectId);
            var projectEstimate = projectService.GetTeamEvaluation
                (projId
                , null
                , 100
                , 20
                , StandardNumberOfHoursPerDay)
                .TotalEvaluation;
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
            int maxNumberOfHistoricalData = 20;
            RunSimulation(10, maxNumberOfHistoricalData);
            for (int numberOfSimulations = 100;
                numberOfSimulations <= 16 * 100;
                numberOfSimulations *= 2)
            {
                long startTicks = DateTime.Now.Ticks;
                RunSimulation(numberOfSimulations
                    , maxNumberOfHistoricalData);
                long endTicks = DateTime.Now.Ticks;
                string message
                    = string.Format
                    ("{0}: {1:N0}"
                    , numberOfSimulations
                    , TimeSpan.FromTicks(endTicks - startTicks).TotalMilliseconds);
                Trace.WriteLine(message);
            }
        }

        private void RunSimulation(int numberOfSimulations, int maxNumberOfHistoricalData)
        {
            foreach (var project in projectRepository.GetProjects(new Paging(1, 20)))
            {
                var projId = project.ProjectId;
                var projectEstimate = projectService.GetTeamEvaluation
                    (projId
                    , null
                    , numberOfSimulations
                    , maxNumberOfHistoricalData
                    , StandardNumberOfHoursPerDay)
                    .TotalEvaluation;
                Trace.WriteLine(projectEstimate);
                foreach (var ie in projectEstimate.GetIssueEvaluations())
                {
                    Trace.WriteLine(ie.ToString());
                    break;
                }
            }
        }
    }
}