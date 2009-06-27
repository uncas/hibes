using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;
using System;

namespace Uncas.EBS.Tests.RepositoryTests
{
    [TestFixture]
    public class IssueRepositoryTests
    {
        private IIssueRepository _issueRepo
            = TestApp.Repositories.IssueRepository;

        private ITaskRepository _taskRepo
            = TestApp.Repositories.TaskRepository;

        private Random _rnd = new Random();

        #region GetIssues

        [Test]
        public void GetIssues_All()
        {
            // Setting up:
            Issue issueClosed = new Issue
            {
                ProjectName = "TestProject2",
                Priority = _rnd.Next(100),
                Status = Status.Closed,
                Title = "TestIssue",
            };
            _issueRepo.InsertIssue(issueClosed);
            Issue issueOpen = new Issue
            {
                ProjectName = "TestProject2",
                Priority = _rnd.Next(100),
                Status = Status.Open,
                Title = "TestIssue",
            };
            _issueRepo.InsertIssue(issueOpen);

            // Testing:
            var issues = _issueRepo.GetIssues(null, Status.Any);

            // Checking:
            Assert.IsNotNull(issues);
            Assert.IsTrue(issues
                .Any(i => i.IssueId == issueClosed.IssueId.Value));
            Assert.IsTrue(issues
                .Any(i => i.IssueId == issueOpen.IssueId.Value));

            // Testing:
            issues = _issueRepo.GetIssues(null, Status.Closed);

            // Checking:
            Assert.IsNotNull(issues);
            Assert.IsTrue(issues
                .Any(i => i.IssueId == issueClosed.IssueId.Value));
            Assert.IsFalse(issues
                .Any(i => i.IssueId == issueOpen.IssueId.Value));

            // Testing:
            issues = _issueRepo.GetIssues(null, Status.Open);

            // Checking:
            Assert.IsNotNull(issues);
            Assert.IsFalse(issues
                .Any(i => i.IssueId == issueClosed.IssueId.Value));
            Assert.IsTrue(issues
                .Any(i => i.IssueId == issueOpen.IssueId.Value));
        }

        #endregion

        #region GetIssueView

        [Test]
        public void GetIssueView()
        {
            // Setting up:
            Issue issue = new Issue
            {
                ProjectName = "IssueRepositoryTests",
                Title = "GetIssueView"
            };
            _issueRepo.InsertIssue(issue);
            Task task = new Task
            {
                CurrentEstimate = 1d,
                OriginalEstimate = 1d,
                Description = "GetIssueView1",
                RefIssueId = issue.IssueId.Value,
            };
            _taskRepo.InsertTask(task);
            Task taskClosed = new Task
            {
                CurrentEstimate = 1d,
                OriginalEstimate = 1d,
                Description = "GetIssueView1",
                RefIssueId = issue.IssueId.Value,
                Status = Status.Closed
            };
            _taskRepo.InsertTask(taskClosed);

            // Testing:
            var issueView = _issueRepo.GetIssueView
                (issue.IssueId.Value, Status.Any);

            // Checking:
            Assert.IsNotNull(issueView);
            Assert.AreEqual(issue.IssueId.Value
                , issueView.Issue.IssueId.Value);
            Assert.AreEqual(2, issueView.Tasks.Count());
            Assert.AreEqual(task.TaskId.Value
                , issueView.Tasks[0].TaskId.Value);
            Assert.AreEqual(taskClosed.TaskId.Value
                , issueView.Tasks[1].TaskId.Value);

            // Testing:
            issueView = _issueRepo.GetIssueView
                (issue.IssueId.Value, Status.Closed);

            // Checking:
            Assert.IsNotNull(issueView);
            Assert.AreEqual(issue.IssueId.Value
                , issueView.Issue.IssueId.Value);
            Assert.AreEqual(1, issueView.Tasks.Count());
            Assert.AreEqual(taskClosed.TaskId.Value
                , issueView.Tasks[0].TaskId.Value);
        }

        #endregion

        #region Insert

        [Test]
        public void InsertIssueTest()
        {
            // Setting up:
            Issue issue = new Issue
            {
                ProjectName = "TestProject2",
                Priority = _rnd.Next(100),
                Status = Status.Closed,
                Title = "TestIssue",
            };

            // Testing:
            _issueRepo.InsertIssue(issue);

            // Checking:
            Assert.IsNotNull(issue.IssueId);
            var issueView = _issueRepo.GetIssueView
                (issue.IssueId.Value, Status.Open);
            Assert.AreEqual(issue.IssueId, issueView.Issue.IssueId);
            Assert.AreEqual(issue.Status, issueView.Issue.Status);
        }

        [Test]
        [ExpectedException(typeof(RepositoryException))]
        public void InsertIssueTest_MissingProjectName()
        {
            // Setting up:
            Issue issue = new Issue
            {
                Priority = _rnd.Next(100),
                Status = Status.Closed,
                Title = "TestIssue",
            };

            // Testing:
            _issueRepo.InsertIssue(issue);
        }

        [Test]
        [ExpectedException(typeof(RepositoryException))]
        public void InsertIssueTest_MissingTitle()
        {
            // Setting up:
            Issue issue = new Issue
            {
                Priority = _rnd.Next(100),
                Status = Status.Closed,
                ProjectName = "TestProject3",
            };

            // Testing:
            _issueRepo.InsertIssue(issue);
        }

        [Test]
        //[ExpectedException(typeof(RepositoryException))]
        public void InsertIssueTest_MissingTitle_Retry()
        {
            // Setting up:
            Issue issue = new Issue
            {
                Priority = _rnd.Next(100),
                Status = Status.Closed,
                ProjectName = "TestProject3",
            };

            // Testing:
            try
            {
                _issueRepo.InsertIssue(issue);
            }
            catch
            {
                issue.Title = "Remmeber title now";
                _issueRepo.InsertIssue(issue);
            }
        }

        #endregion

        #region Update, Delete

        [Test]
        public void UpdateIssue()
        {
            // Setting up:
            int oldPriority = _rnd.Next(100);
            string oldTitle = "OriginalTitle";
            Status oldStatus = Status.Open;
            Issue issue = new Issue
            {
                Priority = oldPriority,
                ProjectName = "TestProject3",
                Status = oldStatus,
                Title = oldTitle
            };
            Trace.WriteLine(issue);
            _issueRepo.InsertIssue(issue);
            int newPriority = oldPriority + 2;
            string newTitle = "New title";
            Status newStatus = Status.Closed;
            issue.Title = newTitle;
            issue.Status = newStatus;
            issue.Priority = newPriority;

            // Testing:
            _issueRepo.UpdateIssue(issue);

            // Checking:
            Assert.IsNotNull(issue.IssueId);
            var retrievedIssue = _issueRepo.GetIssueView
                (issue.IssueId.Value, Status.Closed)
                .Issue;
            Assert.AreEqual(newPriority, retrievedIssue.Priority);
            Assert.AreEqual(newTitle, retrievedIssue.Title);
            Assert.AreEqual(newStatus, retrievedIssue.Status);
        }

        [Test]
        public void DeleteIssue()
        {
            // Setting up:
            Issue issue = new Issue
            {
                Priority = _rnd.Next(100),
                ProjectName = "TestProject3",
                Status = Status.Closed,
                Title = "asd"
            };
            _issueRepo.InsertIssue(issue);
            Assert.IsNotNull(issue.IssueId);

            // Testing:
            _issueRepo.DeleteIssue(issue.IssueId.Value);

            // Checking:
            var retrievedIssue = _issueRepo.GetIssueView
                (issue.IssueId.Value, Status.Closed)
                .Issue;
            Assert.IsNull(retrievedIssue);
        }

        [Test]
        [ExpectedException(typeof(RepositoryException))]
        public void DeleteIssue_NonExisting()
        {
            // Testing:
            _issueRepo.DeleteIssue(-1);
        }

        [Test]
        [ExpectedException(typeof(RepositoryException))]
        public void DeleteIssue_WithTasks()
        {
            // Setting up:
            Issue issue = new Issue
            {
                Priority = _rnd.Next(100),
                ProjectName = "TestProject3",
                Status = Status.Closed,
                Title = "asd"
            };
            _issueRepo.InsertIssue(issue);
            Assert.IsNotNull(issue.IssueId);
            Task task = new Task
            {
                CurrentEstimate = 1d,
                OriginalEstimate = 1d,
                Description = "asd22",
                RefIssueId = issue.IssueId.Value,
            };
            _taskRepo.InsertTask(task);
            Assert.IsNotNull(task.TaskId);

            // Testing:
            _issueRepo.DeleteIssue(issue.IssueId.Value);

            // Checking:
            var retrievedIssue = _issueRepo.GetIssueView
                (issue.IssueId.Value, Status.Closed)
                .Issue;
            Assert.IsNull(retrievedIssue);
        }
        #endregion
    }
}