using System.Linq;
using NUnit.Framework;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;

namespace Uncas.EBS.IntegrationTests.RepositoryTests
{
    [TestFixture]
    public class TaskRepositoryTests
    {
        private ITaskRepository _taskRepo
            = TestApp.Repositories.TaskRepository;

        private IIssueRepository _issueRepo
            = TestApp.Repositories.IssueRepository;

        private IProjectRepository _projectRepo
            = TestApp.Repositories.ProjectRepository;

        [Test]
        public void InsertTask()
        {
            int projectId = _projectRepo
                .GetProjects().FirstOrDefault()
                .ProjectId;

            // Setting up:
            Issue issue = Issue.ConstructIssue
                (projectId
                , "InsertTask"
                , Status.Open
                , 1);
            _issueRepo.InsertIssue(issue);
            Task task = TestApp.GetTask
                (issue.IssueId.Value
                , 1d
                , 1
                , Status.Open
                , 0d
                , 1);

            // Testing:
            _taskRepo.InsertTask(task);

            // Checking:
            Assert.IsNotNull(task.TaskId);
        }

        [Test]
        [ExpectedException(typeof(RepositoryException))]
        public void InsertTask_EmptyDescription()
        {
            var project = _projectRepo.GetProjects().FirstOrDefault();

            // Setting up:
            Issue issue = Issue.ConstructIssue
                (project.ProjectId
                , "InsertTask_EmptyDescription"
                , Status.Open
                , 13);
            _issueRepo.InsertIssue(issue);
            Task task = Task.ConstructTask
                (issue.IssueId.Value
                , ""
                , Status.Open
                , 13
                , 1d
                , 0d
                , null
                , null
                , 1);

            // Testing:
            _taskRepo.InsertTask(task);

            // Checking:
            Assert.IsNotNull(task.TaskId);
        }

        [Test]
        public void UpdateTask_ChangedDescription_ChangedOK()
        {
            var project = _projectRepo.GetProjects().FirstOrDefault();

            // Setting up:
            Issue issue = Issue.ConstructIssue
                (project.ProjectId
                , "UpdateTask_ChangedDescription_ChangedOK"
                , Status.Open
                , 14);
            _issueRepo.InsertIssue(issue);
            Task task = Task.ConstructTask
                (issue.IssueId.Value
                , "ASD"
                , Status.Open
                , 19
                , 1d
                , 0d
                , null
                , null
                , 1);
            _taskRepo.InsertTask(task);
            task.Description = "New description";

            // Testing:
            _taskRepo.UpdateTask(task);

            // Checking:
            var issueView = _issueRepo.GetIssueView
                (issue.IssueId.Value, Status.Any);
            var retrievedTask = issueView.Tasks
                .Where(t => t.TaskId.Value == task.TaskId.Value);
            Assert.IsNotNull(retrievedTask);
        }

        [Test]
        public void UpdateTask_NullIssue()
        {
            // TODO: REFACTOR: Reduce number of statements and calls.

            var project = _projectRepo.GetProjects().FirstOrDefault();

            // Setting up:
            Issue issue = Issue.ConstructIssue
                (project.ProjectId
                , "UpdateTask_NullIssue"
                , Status.Open
                , 16);
            _issueRepo.InsertIssue(issue);
            Task task = Task.ConstructTask
                (issue.IssueId.Value
                , "ASD"
                , Status.Open
                , 23
                , 1d
                , 0d
                , null
                , null
                , 1);
            _taskRepo.InsertTask(task);
            task.Description = "New description";
            Task taskToUpdate = Task.ReconstructTaskToUpdate
                (task.TaskId.Value
                , task.Description
                , task.Status
                , task.Sequence
                , task.CurrentEstimate
                , task.Elapsed
                , task.StartDate
                , task.EndDate
                , task.RefPersonId);

            // Testing:
            _taskRepo.UpdateTask(taskToUpdate);

            // Checking:
            var issueView = _issueRepo.GetIssueView
                (issue.IssueId.Value, Status.Any);
            var retrievedTask = issueView.Tasks
                .Where(t => t.TaskId.Value == task.TaskId.Value);
            Assert.IsNotNull(retrievedTask);
        }

        [Test]
        public void DeleteTask_Default_OK()
        {
            int projectId = _projectRepo
                .GetProjects().FirstOrDefault()
                .ProjectId;

            // Setting up:
            Issue issue = Issue.ConstructIssue
                (projectId
                , "DeleteTask"
                , Status.Open
                , 19);
            _issueRepo.InsertIssue(issue);
            Task task = Task.ConstructTask
                (issue.IssueId.Value
                , "ASD"
                , Status.Open
                , 93
                , 1d
                , 0d
                , null
                , null
                , 1);
            _taskRepo.InsertTask(task);

            // Testing:
            _taskRepo.DeleteTask(task.TaskId.Value);

            // Checking:
            var issueView = _issueRepo.GetIssueView
                (issue.IssueId.Value, Status.Any);
            var retrievedTask = issueView.Tasks
                .Where(t => t.TaskId.Value == task.TaskId.Value)
                .SingleOrDefault();
            Assert.IsNull(retrievedTask);
        }
    }
}