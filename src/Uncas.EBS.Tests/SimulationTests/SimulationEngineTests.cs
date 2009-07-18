using System.Collections.Generic;
using NUnit.Framework;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Simulation;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.Tests.SimulationTests
{
    [TestFixture]
    public class SimulationEngineTests
    {
        [Test]
        public void GetProjectEvaluation_OneCase_OK()
        {
            // Setting up:
            var closedTasks = new List<Task>();
            closedTasks.Add(new Task
            {
                OriginalEstimate = 1d,
                CurrentEstimate = 2d,
                Elapsed = 2d,
                Status = Status.Closed
            });

            var openTasks = new List<TaskDetails>();
            openTasks.Add(new TaskDetails
            {
                CurrentEstimate = 1d,
                Elapsed = 0d,
                Status = Status.Open
            });

            var issueViews = new List<IssueView>();
            issueViews.Add(new IssueView
            {
                Issue = new IssueDetails(),
                Tasks = openTasks
            });


            // Testing:
            SimulationEngine simulationEngine
                = new SimulationEngine(closedTasks);
            var result = simulationEngine.GetProjectEvaluation
                (null, issueViews, 100);


            // Checking results:
            Assert.Less(0d, result.Statistics.Average);
            var ie = result.GetIssueEvaluations();
            Assert.AreEqual(1, ie.Count);
            Assert.Less(0d, ie[0].Average);
        }

        [Test]
        public void GetProjectEvaluation_TwoCases_OK()
        {
            List<Task> closedTasks;
            List<IssueView> issueViews;
            PrepareIssuesAndTasks(out closedTasks, out issueViews);

            // Testing:
            SimulationEngine evals = new SimulationEngine(closedTasks);
            var result = evals.GetProjectEvaluation
                (null, issueViews, 1000);

            // Checking (a lot of things):
            var average = result.Statistics.Average;
            Assert.Greater(average, 0d);
            Assert.Less(average, 1.9d);
            var ie = result.GetIssueEvaluations();
            Assert.AreEqual(1, ie.Count);
            Assert.Greater(ie[0].Average, 0d);
            Assert.Less(ie[0].Average, 1.9d);
        }

        private static void PrepareIssuesAndTasks(out List<Task> closedTasks, out List<IssueView> issueViews)
        {
            // Setting up:
            closedTasks = new List<Task>();
            closedTasks.Add(new Task
            {
                OriginalEstimate = 1d,
                CurrentEstimate = 2d,
                Elapsed = 2d,
                Status = Status.Closed
            });
            closedTasks.Add(new Task
            {
                OriginalEstimate = 1d,
                CurrentEstimate = 1.5d,
                Elapsed = 1.5d,
                Status = Status.Closed
            });

            var openTasks = new List<TaskDetails>();
            openTasks.Add(new TaskDetails
            {
                CurrentEstimate = 1d,
                Elapsed = 0d,
                Status = Status.Open
            });

            issueViews = new List<IssueView>();
            issueViews.Add(new IssueView
            {
                Issue = new IssueDetails(),
                Tasks = openTasks
            });
        }

        [Test]
        public void GetProjectEvalution_SpeedTest()
        {
            List<Task> closedTasks;
            List<IssueView> issueViews;
            PrepareIssuesAndTasks(out closedTasks, out issueViews);

            // Testing:
            SimulationEngine evals = new SimulationEngine(closedTasks);
            FuncToSpeedTest func =
                () =>
                {
                    evals.GetProjectEvaluation
                        (null, issueViews, 100);
                };

            SpeedTester.RunSpeedTest
                ("GetProjectEvaluation_SpeedTest"
                , func
                , 100);
        }
    }
}