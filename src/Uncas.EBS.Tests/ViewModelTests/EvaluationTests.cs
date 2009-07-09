using NUnit.Framework;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.Tests.ViewModelTests
{
    [TestFixture]
    public class EvaluationTests
    {
        [Test]
        public void IssueEvaluationTest()
        {
            IssueEvaluation iEval = new IssueEvaluation
                (new Issue(), 2, 1d, 1d);

            iEval.AddEvaluation(2d);
            Assert.Less(0d, iEval.Average);

            iEval.AddEvaluation(3d);
            Assert.Less(0d, iEval.Average);
        }

        [Test]
        public void ProjectEvaluationTest()
        {
            ProjectEvaluation pEval = new ProjectEvaluation();

            pEval.AddEvaluation(1d);
            Assert.Less(0d, pEval.Statistics.Average);
            Assert.LessOrEqual(0d, pEval.Statistics.StandardDeviation);

            pEval.AddEvaluation(2d);
            Assert.Less(0d, pEval.Statistics.Average);
            Assert.Greater(pEval.Statistics.StandardDeviation, 0d);

            Assert.GreaterOrEqual(pEval.Statistics.Probabilities.Count, 1);

            Issue issue1 = new Issue();
            Issue issue2 = new Issue();
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