﻿using System;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using Uncas.EBS.ApplicationServices;
using Uncas.EBS.Domain.Repository;

namespace Uncas.EBS.IntegrationTests.ApplicationServicesTests
{
    [TestFixture]
    public class ProjectServiceTests
    {
        private ProjectService _projectService
             = new ProjectService(TestApp.Repositories);

        private IProjectRepository _projectRepository
            = TestApp.Repositories.ProjectRepository;

        [Test]
        public void GetProjectEvaluation_All_PositiveAverage()
        {
            var projectEvaluation
                = _projectService.GetProjectEvaluation
                    (null
                    , null
                    , 100
                    , 10);

            Assert.IsNotNull(projectEvaluation.Average);
            Assert.Less(0d, projectEvaluation.Average);
        }

        [Test]
        public void GetProjectEstimate_NonExistingProject()
        {
            var projectEstimate = _projectService.GetProjectEvaluation
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
            var projId = _projectRepository.GetProjects()
                .Min(p => p.ProjectId);
            var projectEstimate = _projectService.GetProjectEvaluation
                (projId, null, 100, 20);
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
            for (int numberOfSimulations = 100
                ; numberOfSimulations <= 16 * 100
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
            foreach (var project in _projectRepository.GetProjects())
            {
                var projId = project.ProjectId;
                var projectEstimate = _projectService.GetProjectEvaluation
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