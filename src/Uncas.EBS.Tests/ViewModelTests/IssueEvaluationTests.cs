using NUnit.Framework;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.ViewModel;
using System;

namespace Uncas.EBS.Tests.ViewModelTests
{
    [TestFixture]
    public class IssueEvaluationTests
    {
        private const double _standardNumberOfHoursPerDay = 7.5d;

        [Test]
        public void Constructor_NormalInput_OK()
        {
            IssueEvaluation ie
                = new IssueEvaluation
                (Issue.ConstructIssue(1, "A", Status.Open, 1)
                , 2, 3d, 4d, _standardNumberOfHoursPerDay);
            Assert.AreEqual(2, ie.NumberOfOpenTasks);
            Assert.AreEqual(3d, ie.Elapsed);
            Assert.AreEqual(4d / _standardNumberOfHoursPerDay
                , ie.Average);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ElapsedNegative_Exception()
        {
            IssueEvaluation ie
                = new IssueEvaluation
                    (Issue.ConstructIssue(1, "A", Status.Open, 1)
                    , 2, -1d, 0d
                    , _standardNumberOfHoursPerDay);
        }

        [Test]
        public void AddEvaluation_TwoItems_AverageOK()
        {
            // Adding evaluations in terms of hours:
            double evaluation1 = 1d;
            IssueEvaluation issueEvaluation
                = new IssueEvaluation
                    (
                        Issue.ConstructIssue
                            (1, "A", Status.Open, 1)
                        , 2
                        , 1d
                        , evaluation1
                        , _standardNumberOfHoursPerDay
                    );
            double evaluation2 = 6.5d;
            issueEvaluation.AddEvaluation(evaluation2);

            // Checking the resulting average in days:
            Assert.AreEqual
                (
                (evaluation1 + evaluation2)
                    / (2d * _standardNumberOfHoursPerDay)
                , issueEvaluation.Average
                );
        }
    }
}