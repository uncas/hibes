using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Uncas.EBS.Domain.Simulation;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.Tests.SimulationTests
{
    [TestFixture]
    public class EvaluationsTests
    {
        [Test]
        public void GetEvaluationTest_OneCase()
        {
            var historicalTasks = new List<Task>();
            historicalTasks.Add(new Task
            {
                OriginalEstimate = 1d,
                CurrentEstimate = 2d,
                Elapsed = 2d,
                Status = Status.Closed
            });

            Evaluations evals = new Evaluations(historicalTasks);

            var tasks = new List<Task>();
            tasks.Add(new Task
            {
                CurrentEstimate = 1d,
                Elapsed = 0d,
                Status = Status.Open
            });

            var issueViews = new List<IssueView>();
            issueViews.Add(new IssueView
            {
                Issue = new IssueDetails(),
                Tasks = tasks
            });

            var result = evals.GetProjectEvaluation(issueViews, 100);

            Assert.AreEqual(2d, result.Statistics.Average);
            var ie = result.GetIssueEvaluations();
            Assert.AreEqual(1, ie.Count);
            Assert.AreEqual(2d, ie[0].Average);
        }

        [Test]
        public void GetEvaluationTest_TwoCases()
        {
            var historicalTasks = new List<Task>();
            historicalTasks.Add(new Task
            {
                OriginalEstimate = 1d,
                CurrentEstimate = 2d,
                Elapsed = 2d,
                Status = Status.Closed
            });
            historicalTasks.Add(new Task
            {
                OriginalEstimate = 1d,
                CurrentEstimate = 1.5d,
                Elapsed = 1.5d,
                Status = Status.Closed
            });

            Evaluations evals = new Evaluations(historicalTasks);

            var tasks = new List<Task>();
            tasks.Add(new Task
            {
                CurrentEstimate = 1d,
                Elapsed = 0d,
                Status = Status.Open
            });

            var issueViews = new List<IssueView>();
            issueViews.Add(new IssueView
            {
                Issue = new IssueDetails(),
                Tasks = tasks
            });

            var result = evals.GetProjectEvaluation(issueViews, 1000);
            var average = result.Statistics.Average;
            Assert.Less(1.6d, average);
            Assert.Less(average, 1.9d);
            var ie = result.GetIssueEvaluations();
            Assert.AreEqual(1, ie.Count);
            Assert.Less(1.6d, ie[0].Average);
            Assert.Less(ie[0].Average, 1.9d);
        }
    }
}
