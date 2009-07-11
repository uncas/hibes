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
            Issue issue = new Issue
            {
                RefProjectId = projectId,
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
            var project = _projectRepo.GetProjects().FirstOrDefault();

            // Setting up:
            Issue issue = new Issue
            {
                RefProjectId = project.ProjectId,
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
        public void UpdateTask_ChangedDescription_ChangedOK()
        {
            var project = _projectRepo.GetProjects().FirstOrDefault();

            // Setting up:
            Issue issue = new Issue
            {
                RefProjectId = project.ProjectId,
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
            var project = _projectRepo.GetProjects().FirstOrDefault();

            // Setting up:
            Issue issue = new Issue
            {
                RefProjectId = project.ProjectId,
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
            int projectId = _projectRepo
                .GetProjects().FirstOrDefault()
                .ProjectId;

            // Setting up:
            Issue issue = new Issue
            {
                RefProjectId = projectId,
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