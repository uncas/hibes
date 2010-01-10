using System;
using NUnit.Framework;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.Tests.ViewModelTests
{
    [TestFixture]
    public class IssueEvaluationTests
    {
        private const double StandardNumberOfHoursPerDay = 7.5d;

        private IssueDetails GetIssue()
        {
            return IssueDetails.ReconstructIssueDetails
                (1
                , DateTime.Now
                , 1
                , "A"
                , Status.Open
                , "B"
                , 0
                , null
                , null);
        }

        [Test]
        public void Constructor_NormalInput_OK()
        {
            IssueEvaluation ie
                = new IssueEvaluation
                (GetIssue()
                , 2
                , 3d
                , 4d
                , StandardNumberOfHoursPerDay);
            Assert.AreEqual(2, ie.NumberOfOpenTasks);
            Assert.AreEqual(3d, ie.Elapsed);
            Assert.AreEqual
                (4d / StandardNumberOfHoursPerDay
                , ie.Average);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ElapsedNegative_Exception()
        {
            IssueEvaluation ie
                = new IssueEvaluation
                    (GetIssue()
                    , 2
                    , -1d
                    , 0d
                    , StandardNumberOfHoursPerDay);
        }

        [Test]
        public void AddEvaluation_TwoItems_AverageOK()
        {
            // Adding evaluations in terms of hours:
            double evaluation1 = 1d;
            IssueEvaluation issueEvaluation
                = new IssueEvaluation
                    (GetIssue()
                    , 2
                    , 1d
                    , evaluation1
                    , StandardNumberOfHoursPerDay);
            double evaluation2 = 6.5d;
            issueEvaluation.AddEvaluation(evaluation2);

            // Checking the resulting average in days:
            Assert.AreEqual
                ((evaluation1 + evaluation2)
                    / (2d * StandardNumberOfHoursPerDay)
                , issueEvaluation.Average);
        }
    }
}