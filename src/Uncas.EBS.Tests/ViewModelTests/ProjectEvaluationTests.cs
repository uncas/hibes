using System;
using NUnit.Framework;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.Tests.ViewModelTests
{
    [TestFixture]
    public class ProjectEvaluationTests
    {
        private const double
            StandardNumberOfHoursPerDay = 7.5d;

        private IssueDetails GetIssue()
        {
            return IssueDetails.ReconstructIssueDetails
                (1, DateTime.Now, 1, "A", Status.Open, "B"
                , 0, null, null);
        }

        [Test]
        public void ProjectEvaluationTest()
        {
            // TODO: REFACTOR: Reduce number of asserts.

            ProjectEvaluation projEval = new ProjectEvaluation
                (new PersonView(1, "A", null)
                , StandardNumberOfHoursPerDay);

            projEval.AddEvaluation(1d);
            Assert.Less(0d, projEval.Statistics.Average);
            Assert.LessOrEqual(0d, projEval.Statistics.StandardDeviation);

            projEval.AddEvaluation(2d);
            Assert.Less(0d, projEval.Statistics.Average);
            Assert.Greater(projEval.Statistics.StandardDeviation, 0d);

            Assert.GreaterOrEqual(projEval.Statistics.Probabilities.Count, 1);

            IssueDetails issue1 = GetIssue();
            IssueDetails issue2 = GetIssue();
            projEval.AddIssueEvaluation(issue1, 2, 0.5d, 1d);
            projEval.AddIssueEvaluation(issue1, 3, 0.5d, 2d);
            projEval.AddIssueEvaluation(issue2, 4, 0.5d, 1d);
            projEval.AddIssueEvaluation(issue2, 5, 0.5d, 3d);
            var ie = projEval.GetIssueEvaluations();
            Assert.Less(0d, ie[0].Average);
            Assert.Less(0d, ie[1].Average);
        }
    }
}