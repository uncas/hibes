using System;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using Uncas.EBS.ApplicationServices;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Tests;

namespace Uncas.EBS.IntegrationTests.RepositoryTests
{
    [TestFixture]
    public class IssueRepositoryTests
    {
        private IIssueRepository _issueRepo
            = TestApp.Repositories.IssueRepository;

        private ITaskRepository _taskRepo
            = TestApp.Repositories.TaskRepository;

        private IProjectRepository _projectRepo
            = TestApp.Repositories.ProjectRepository;

        private ProjectService _projectService
            = new ProjectService(TestApp.Repositories);

        private Random _rnd = new Random();

        #region GetIssues

        [Test]
        public void GetIssues_All()
        {
            int projectId = _projectRepo
                .GetProjects().FirstOrDefault()
                .ProjectId;

            // Setting up:
            Issue issueClosed = new Issue
            {
                RefProjectId = projectId,
                Priority = _rnd.Next(100),
                Status = Status.Closed,
                Title = "TestIssue",
            };
            _issueRepo.InsertIssue(issueClosed);
            Issue issueOpen = new Issue
            {
                RefProjectId = projectId,
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
            int projectId = _projectRepo
                .GetProjects().FirstOrDefault()
                .ProjectId;

            // Setting up:
            Issue issue = new Issue
            {
                RefProjectId = projectId,
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
            int projectId = _projectRepo
                .GetProjects().FirstOrDefault()
                .ProjectId;

            // Setting up:
            Issue issue = new Issue
            {
                RefProjectId = projectId,
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
        public void InsertIssueTest_MissingProjectId()
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
            int projectId = _projectRepo
                .GetProjects().FirstOrDefault()
                .ProjectId;

            // Setting up:
            Issue issue = new Issue
            {
                Priority = _rnd.Next(100),
                Status = Status.Closed,
                RefProjectId = projectId
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
            int projectId = _projectRepo
                .GetProjects().FirstOrDefault()
                .ProjectId;

            // Setting up:
            int oldPriority = _rnd.Next(100);
            string oldTitle = "OriginalTitle";
            Status oldStatus = Status.Open;
            Issue issue = new Issue
            {
                Priority = oldPriority,
                RefProjectId = projectId,
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
            int projectId = _projectRepo
                .GetProjects().FirstOrDefault()
                .ProjectId;
            // Setting up:
            Issue issue = new Issue
            {
                Priority = _rnd.Next(100),
                RefProjectId = projectId,
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

        #region Speed tests

        /*
No indexes:
    GetIssues: 0,62
    GetIssueView-Any: 3,22
    GetIssueView-Open: 3,57
    GetIssueView-Closed: 3,54
    GetTasks: 0,58
    GetProjects: 0,23
    GetProjectEvaluation-10-10: 8,81
Indexes on all foreign keys:
    GetIssues: 0,67
    GetIssueView-Any: 3,26
    GetIssueView-Open: 3,48
    GetIssueView-Closed: 3,47
    GetTasks: 0,56
    GetProjects: 0,20
    GetProjectEvaluation-10-10: 8,69
         */

        [Test]
        public void RunAllSpeedTests()
        {
            GetIssues_Speed();
            GetIssueView_Speed(Status.Any);
            GetIssueView_Speed(Status.Open);
            GetIssueView_Speed(Status.Closed);
            GetTasks_Speed();
            GetProjects_Speed();
            GetProjectEvaluation_Speed(5, 5);
        }

        public void GetIssueView_Speed(Status status)
        {
            var issue = _issueRepo.GetIssues
                (null, status).FirstOrDefault();
            if (issue == null)
            {
                return;
            }
            int issueId = issue.IssueId.Value;
            FuncToSpeedTest tf = () =>
            {
                _issueRepo.GetIssueView(issueId, status);
                _issueRepo.GetIssueView(issueId, status);
                _issueRepo.GetIssueView(issueId, status);
            };
            string testTitle = string.Format("GetIssueView-{0}"
                , status);
            TestSpeed(testTitle, tf);
        }

        public void GetProjectEvaluation_Speed
            (int numberOfSimulations
            , int maxNumberOfHistoricalData)
        {
            FuncToSpeedTest tf = () =>
            {
                _projectService.GetTeamEvaluation
                    (null, null, numberOfSimulations
                    , maxNumberOfHistoricalData);
                _projectService.GetTeamEvaluation
                    (null, null, numberOfSimulations
                    , maxNumberOfHistoricalData);
                _projectService.GetTeamEvaluation
                    (null, null, numberOfSimulations
                    , maxNumberOfHistoricalData);
            };
            string testTitle = string.Format("GetProjectEvaluation-{0}-{1}"
                , numberOfSimulations
                , maxNumberOfHistoricalData);
            TestSpeed(testTitle, tf);
        }

        public void GetIssues_Speed()
        {
            int projectId = _projectRepo.GetProjects().First().ProjectId;
            FuncToSpeedTest tf = () =>
            {
                _issueRepo.GetIssues(null, Status.Any);
                _issueRepo.GetIssues(null, Status.Closed);
                _issueRepo.GetIssues(projectId, Status.Any);
            };
            TestSpeed("GetIssues", tf);
        }

        public void GetTasks_Speed()
        {
            FuncToSpeedTest tf = () =>
                {
                    _taskRepo.GetTasksByStatus(Status.Any);
                    _taskRepo.GetTasksByStatus(Status.Closed);
                    _taskRepo.GetTasksByStatus(Status.Open);
                };
            TestSpeed("GetTasks", tf);
        }

        public void GetProjects_Speed()
        {
            FuncToSpeedTest tf = () =>
            {
                _projectRepo.GetProjects();
                _projectRepo.GetProjects();
                _projectRepo.GetProjects();
            };
            TestSpeed("GetProjects", tf);
        }

        private static void TestSpeed(string testTitle, FuncToSpeedTest tf)
        {
            SpeedTester.RunSpeedTest(testTitle, tf, 5);
        }

        #endregion
    }
}