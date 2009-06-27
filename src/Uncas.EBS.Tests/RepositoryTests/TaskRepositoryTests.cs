using System.Linq;
using NUnit.Framework;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;

namespace Uncas.EBS.Tests.RepositoryTests
{
    [TestFixture]
    public class TaskRepositoryTests
    {
        private ITaskRepository _taskRepo
            = TestApp.Repositories.TaskRepository;

        private IIssueRepository _issueRepo
            = TestApp.Repositories.IssueRepository;

        [Test]
        public void InsertTask()
        {
            // Setting up:
            Issue issue = new Issue
            {
                ProjectName = "xxx",
                Title = "Abc",
            };
            _issueRepo.InsertIssue(issue);
            Task task = new Task
            {
                CurrentEstimate = 1d,
                Description = "ASD",
                RefIssueId = issue.IssueId.Value,
                OriginalEstimate = 1d,
            };

            // Testing:
            _taskRepo.InsertTask(task);

            // Checking:
            Assert.IsNotNull(task.TaskId);
        }

        [Test]
        [ExpectedException(typeof(RepositoryException))]
        public void InsertTask_EmptyDescription()
        {
            // Setting up:
            Issue issue = new Issue
            {
                ProjectName = "xxx",
                Title = "Abc",
            };
            _issueRepo.InsertIssue(issue);
            Task task = new Task
            {
                CurrentEstimate = 1d,
                Description = "",
                RefIssueId = issue.IssueId.Value,
                OriginalEstimate = 1d,
            };

            // Testing:
            _taskRepo.InsertTask(task);

            // Checking:
            Assert.IsNotNull(task.TaskId);
        }

        [Test]
        public void UpdateTask()
        {
            // Setting up:
            Issue issue = new Issue
            {
                ProjectName = "xxx",
                Title = "Abc",
            };
            _issueRepo.InsertIssue(issue);
            Task task = new Task
            {
                CurrentEstimate = 1d,
                Description = "ASD",
                RefIssueId = issue.IssueId.Value,
                OriginalEstimate = 1d,
            };
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
            // Setting up:
            Issue issue = new Issue
            {
                ProjectName = "xxx",
                Title = "Abc",
            };
            _issueRepo.InsertIssue(issue);
            Task task = new Task
            {
                CurrentEstimate = 1d,
                Description = "ASD",
                RefIssueId = issue.IssueId.Value,
                OriginalEstimate = 1d,
            };
            _taskRepo.InsertTask(task);
            task.Description = "New description";
            Task taskToUpdate = new Task
            {
                TaskId = task.TaskId,
                Status = task.Status,
                StartDate = task.StartDate,
                Sequence = task.Sequence,
                EndDate = task.EndDate,
                Elapsed = task.Elapsed,
                Description = task.Description,
                CurrentEstimate = task.CurrentEstimate,
                OriginalEstimate = task.OriginalEstimate,
                // No need to change this: RefIssueId
            };

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
        public void DeleteTask()
        {
            // Setting up:
            Issue issue = new Issue
            {
                ProjectName = "xxx",
                Title = "Abc",
            };
            _issueRepo.InsertIssue(issue);
            Task task = new Task
            {
                CurrentEstimate = 1d,
                Description = "ASD",
                RefIssueId = issue.IssueId.Value,
                OriginalEstimate = 1d,
            };
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