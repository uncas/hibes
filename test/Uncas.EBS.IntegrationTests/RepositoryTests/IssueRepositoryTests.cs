using System;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using Uncas.EBS.ApplicationServices;
using Uncas.EBS.Domain;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.ViewModel;
using Uncas.EBS.Tests;

namespace Uncas.EBS.IntegrationTests.RepositoryTests
{
    [TestFixture]
    public class IssueRepositoryTests
    {
        private const double StandardNumberOfHoursPerDay = 7.5d;

        private IIssueRepository issueRepo
            = TestApp.Repositories.IssueRepository;

        private ITaskRepository taskRepo
            = TestApp.Repositories.TaskRepository;

        private IProjectRepository projectRepo
            = TestApp.Repositories.ProjectRepository;

        private ProjectService projectService
            = new ProjectService(TestApp.Repositories);

        private Random random = new Random();

        #region GetIssues

        [Test]
        public void GetIssues_All()
        {
            int projectId = projectRepo
                .GetFirstProject()
                .ProjectId;

            // Setting up:
            Issue issueClosed = Issue.ConstructIssue
                (projectId
                , "GetIssues_All-Closed"
                , Status.Closed
                , random.Next(100));
            this.issueRepo.InsertIssue(issueClosed);
            Issue issueOpen = Issue.ConstructIssue
                (projectId
                , "GetIssues_All-Open"
                , Status.Open
                , random.Next(100));
            this.issueRepo.InsertIssue(issueOpen);

            // Testing:
            var issues = this.issueRepo.GetIssues(null, Status.Any);

            // Checking:
            Assert.IsNotNull(issues);
            Assert.IsTrue(issues
                .Any(i => i.IssueId == issueClosed.IssueId.Value));
            Assert.IsTrue(issues
                .Any(i => i.IssueId == issueOpen.IssueId.Value));

            // Testing:
            issues = this.issueRepo.GetIssues(null, Status.Closed);

            // Checking:
            Assert.IsNotNull(issues);
            Assert.IsTrue(issues
                .Any(i => i.IssueId == issueClosed.IssueId.Value));
            Assert.IsFalse(issues
                .Any(i => i.IssueId == issueOpen.IssueId.Value));

            // Testing:
            issues = this.issueRepo.GetIssues(null, Status.Open);

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
            int projectId = projectRepo
                .GetFirstProject()
                .ProjectId;

            // Setting up:
            Issue issue = Issue.ConstructIssue
                (projectId
                , "GetIssueView"
                , Status.Open
                , 15);
            this.issueRepo.InsertIssue(issue);
            Task task = GetTask(issue, Status.Open);
            this.taskRepo.InsertTask(task);
            Task taskClosed = GetTask(issue, Status.Closed);
            this.taskRepo.InsertTask(taskClosed);

            // Testing:
            var issueView = this.issueRepo.GetIssueView
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
            issueView = this.issueRepo.GetIssueView
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
        public void InsertIssue_Default_OK()
        {
            int projectId = projectRepo
                .GetFirstProject()
                .ProjectId;

            // Setting up:
            Issue issue = Issue.ConstructIssue
                (projectId
                , "InsertIssue_Default_OK"
                , Status.Closed
                , random.Next(100));

            // Testing:
            this.issueRepo.InsertIssue(issue);

            // Checking:
            Assert.IsNotNull(issue.IssueId);
            var issueView = this.issueRepo.GetIssueView
                (issue.IssueId.Value, Status.Open);
            Assert.AreEqual(issue.IssueId, issueView.Issue.IssueId);
            Assert.AreEqual(issue.Status, issueView.Issue.Status);
        }

        [Test]
        [ExpectedException(typeof(RepositoryException))]
        public void InsertIssue_NonExistingProject_Exception()
        {
            // Setting up:
            Issue issue = Issue.ConstructIssue
                (int.MaxValue
                , "InsertIssue"
                , Status.Closed
                , 20);

            // Testing:
            this.issueRepo.InsertIssue(issue);
        }

        [Test]
        [ExpectedException(typeof(RepositoryException))]
        public void InsertIssue_MissingTitle_Exception()
        {
            // Setting up:
            Issue issue = Issue.ConstructIssue
                (1
                , null
                , Status.Closed
                , random.Next(100));

            // Testing:
            this.issueRepo.InsertIssue(issue);
        }

        [Test]
        public void InsertIssueTest_MissingTitle_Retry()
        {
            int projectId = projectRepo
                .GetFirstProject()
                .ProjectId;

            // Setting up:
            Issue issue = Issue.ConstructIssue
                (projectId
                , null
                , Status.Closed
                , random.Next(100));

            // Testing:
            try
            {
                this.issueRepo.InsertIssue(issue);
            }
            catch
            {
                issue.Title = "Remmeber title now";
                this.issueRepo.InsertIssue(issue);
            }
        }

        #endregion

        #region Update, Delete

        [Test]
        public void UpdateIssue()
        {
            int projectId = projectRepo
                .GetFirstProject()
                .ProjectId;

            // Setting up:
            int oldPriority = random.Next(100);
            string oldTitle = "OriginalTitle";
            Status oldStatus = Status.Open;
            Issue issue = Issue.ConstructIssue
                (projectId
                , oldTitle
                , oldStatus
                , oldPriority);
            Trace.WriteLine(issue);
            this.issueRepo.InsertIssue(issue);
            int newPriority = oldPriority + 2;
            string newTitle = "New title";
            Status newStatus = Status.Closed;
            issue.Title = newTitle;
            issue.Status = newStatus;
            issue.Priority = newPriority;

            // Testing:
            this.issueRepo.UpdateIssue(issue);

            // Checking:
            Assert.IsNotNull(issue.IssueId);
            var retrievedIssue = this.issueRepo.GetIssueView
                (issue.IssueId.Value, Status.Closed)
                .Issue;
            Assert.AreEqual(newPriority, retrievedIssue.Priority);
            Assert.AreEqual(newTitle, retrievedIssue.Title);
            Assert.AreEqual(newStatus, retrievedIssue.Status);
        }

        [Test]
        public void DeleteIssue_Default_OK()
        {
            int projectId = projectRepo
                .GetFirstProject()
                .ProjectId;

            // Setting up:
            Issue issue = Issue.ConstructIssue
                (projectId
                , "DeleteIssue"
                , Status.Closed
                , random.Next(100));
            issueRepo.InsertIssue(issue);
            Assert.IsNotNull(issue.IssueId);

            // Testing:
            issueRepo.DeleteIssue(issue.IssueId.Value);

            // Checking:
            var retrievedIssue = issueRepo.GetIssueView
                (issue.IssueId.Value, Status.Closed)
                .Issue;
            Assert.IsNull(retrievedIssue);
        }

        [Test]
        [ExpectedException(typeof(RepositoryException))]
        public void DeleteIssue_NonExisting()
        {
            // Testing:
            issueRepo.DeleteIssue(-1);
        }

        [Test]
        [ExpectedException(typeof(RepositoryException))]
        public void DeleteIssue_WithTasks()
        {
            // Setting up:
            Issue issue = Issue.ConstructIssue
                (3
                , "DeleteIssue"
                , Status.Closed
                , random.Next(100));
            issueRepo.InsertIssue(issue);
            Assert.IsNotNull(issue.IssueId);
            Task task = GetTask(issue, Status.Open);
            taskRepo.InsertTask(task);
            Assert.IsNotNull(task.TaskId);

            // Testing:
            issueRepo.DeleteIssue(issue.IssueId.Value);

            // Checking:
            var retrievedIssue = issueRepo.GetIssueView
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
            var issue = issueRepo.GetIssues
                (null, status).FirstOrDefault();
            if (issue == null)
            {
                return;
            }

            int issueId = issue.IssueId.Value;
            FuncToSpeedTest tf = () =>
            {
                issueRepo.GetIssueView(issueId, status);
                issueRepo.GetIssueView(issueId, status);
                issueRepo.GetIssueView(issueId, status);
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
                projectService.GetTeamEvaluation
                    (null
                    , null
                    , numberOfSimulations
                    , maxNumberOfHistoricalData
                    , StandardNumberOfHoursPerDay);
                projectService.GetTeamEvaluation
                    (null
                    , null
                    , numberOfSimulations
                    , maxNumberOfHistoricalData
                    , StandardNumberOfHoursPerDay);
                projectService.GetTeamEvaluation
                    (null
                    , null
                    , numberOfSimulations
                    , maxNumberOfHistoricalData
                    , StandardNumberOfHoursPerDay);
            };
            string testTitle = string.Format("GetProjectEvaluation-{0}-{1}"
                , numberOfSimulations
                , maxNumberOfHistoricalData);
            TestSpeed(testTitle, tf);
        }

        public void GetIssues_Speed()
        {
            int projectId = projectRepo.GetFirstProject().ProjectId;
            FuncToSpeedTest tf = () =>
            {
                issueRepo.GetIssues(null, Status.Any);
                issueRepo.GetIssues(null, Status.Closed);
                issueRepo.GetIssues(projectId, Status.Any);
            };
            TestSpeed("GetIssues", tf);
        }

        public void GetTasks_Speed()
        {
            FuncToSpeedTest tf = () =>
                {
                    taskRepo.GetTasks(new TaskFilter { Status = Status.Any });
                    taskRepo.GetTasks(new TaskFilter { Status = Status.Closed });
                    taskRepo.GetTasks(new TaskFilter { Status = Status.Open });
                };
            TestSpeed("GetTasks", tf);
        }

        public void GetProjects_Speed()
        {
            FuncToSpeedTest tf = () =>
            {
                projectRepo.GetProjects(new Paging());
                projectRepo.GetProjects(new Paging());
                projectRepo.GetProjects(new Paging());
            };
            TestSpeed("GetProjects", tf);
        }

        private static void TestSpeed(string testTitle, FuncToSpeedTest tf)
        {
            SpeedTester.RunSpeedTest(testTitle, tf, 5);
        }

        #endregion

        private Task GetTask(Issue issue, Status taskStatus)
        {
            Task task = Task.ConstructTask
                (issue.IssueId.Value
                , "GetIssueView1"
                , taskStatus
                , 1
                , 1d
                , 0d
                , null
                , null
                , 1);
            return task;
        }
    }
}