using NUnit.Framework;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.Tests.ViewModelTests
{
    [TestFixture]
    public class ProjectEvaluationTests
    {
        [Test]
        public void ProjectEvaluationTest()
        {
            // TODO: REFACTOR: Reduce number of calls.

            ProjectEvaluation pEval = new ProjectEvaluation();

            pEval.AddEvaluation(1d);
            Assert.Less(0d, pEval.Statistics.Average);
            Assert.LessOrEqual(0d, pEval.Statistics.StandardDeviation);

            pEval.AddEvaluation(2d);
            Assert.Less(0d, pEval.Statistics.Average);
            Assert.Greater(pEval.Statistics.StandardDeviation, 0d);

            Assert.GreaterOrEqual(pEval.Statistics.Probabilities.Count, 1);

            Issue issue1
                = Issue.ConstructIssue(1, "A", Status.Open, 1);
            Issue issue2
                = Issue.ConstructIssue(1, "A", Status.Open, 1);
            pEval.AddIssueEvaluation(issue1, 2, 0.5d, 1d);
            pEval.AddIssueEvaluation(issue1, 3, 0.5d, 2d);
            pEval.AddIssueEvaluation(issue2, 4, 0.5d, 1d);
            pEval.AddIssueEvaluation(issue2, 5, 0.5d, 3d);
            var ie = pEval.GetIssueEvaluations();
            Assert.Less(0d, ie[0].Average);
            Assert.Less(0d, ie[1].Average);
        }
    }
}