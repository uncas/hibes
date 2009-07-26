using System.Collections.Generic;
using NUnit.Framework;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.ViewModel;
using Uncas.EBS.Utility.Simulation;

namespace Uncas.EBS.Tests.SimulationTests
{
    [TestFixture]
    public class SimulationEngineTests
    {
        private const double DefaultStandardNumberOfHoursPerDay
            = 7.5d;

        private Task GetTask
            (double original
            , double current
            , double elapsed
            , Status status)
        {
            Task task = Task.ConstructTask
                (1
                , "test"
                , status
                , 1
                , original
                , elapsed
                , null
                , null
                , 1);
            task.CurrentEstimate = current;
            return task;
        }

        [Test]
        public void GetProjectEvaluation_OneCase_OK()
        {
            // Setting up:
            var closedTasks = new List<Task>();
            closedTasks.Add(GetTask
                (1d, 2d, 2d, Status.Closed));

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
                (new PersonView(1, null, null), issueViews, 100
                , DefaultStandardNumberOfHoursPerDay);


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
                (new PersonView(1, null, null), issueViews, 1000
                , DefaultStandardNumberOfHoursPerDay);

            // Checking (a lot of things):
            var average = result.Statistics.Average;
            Assert.Greater(average, 0d);
            Assert.Less(average, 1.9d);
            var ie = result.GetIssueEvaluations();
            Assert.AreEqual(1, ie.Count);
            Assert.Greater(ie[0].Average, 0d);
            Assert.Less(ie[0].Average, 1.9d);
        }

        private void PrepareIssuesAndTasks(out List<Task> closedTasks, out List<IssueView> issueViews)
        {
            // Setting up:
            closedTasks = new List<Task>();
            closedTasks.Add(GetTask
                (1d, 2d, 2d, Status.Closed));
            closedTasks.Add(GetTask
                (1d, 1.5d, 1.5d, Status.Closed));

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
                        (new PersonView(1, null, null)
                        , issueViews
                        , 100
                        , DefaultStandardNumberOfHoursPerDay);
                };

            SpeedTester.RunSpeedTest
                ("GetProjectEvaluation_SpeedTest"
                , func
                , 100);
        }
    }
}