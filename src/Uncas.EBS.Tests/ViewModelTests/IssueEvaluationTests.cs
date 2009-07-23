using NUnit.Framework;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.ViewModel;
using System;

namespace Uncas.EBS.Tests.ViewModelTests
{
    [TestFixture]
    public class IssueEvaluationTests
    {
        [Test]
        public void Constructor_NormalInput_OK()
        {
            IssueEvaluation ie
                = new IssueEvaluation
                (Issue.ConstructIssue(1, "A", Status.Open, 1)
                , 2, 3d, 4d);
            Assert.AreEqual(2, ie.NumberOfOpenTasks);
            Assert.AreEqual(3d, ie.Elapsed);
            // HACK: Here a hard-coded value of 7.5 is used!
            Assert.AreEqual(4d / 7.5d, ie.Average);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ElapsedNegative_Exception()
        {
            IssueEvaluation ie
                = new IssueEvaluation
                    (Issue.ConstructIssue(1, "A", Status.Open, 1)
                    , 2, -1d, 0d);
        }

        [Test]
        public void AddEvaluation_TwoItems_AverageOK()
        {
            // Adding evaluations in terms of hours:
            IssueEvaluation issueEvaluation = new IssueEvaluation
                (Issue.ConstructIssue(1, "A", Status.Open, 1), 2, 1d, 1d);
            issueEvaluation.AddEvaluation(6.5d);

            // Checking the resulting average in days:
            // HACK: Assumes a value of 7.5 hours per day:
            Assert.AreEqual(0.5d, issueEvaluation.Average);
        }
    }
}