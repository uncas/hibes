using System.Linq;
using NUnit.Framework;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;

namespace Uncas.EBS.IntegrationTests.RepositoryTests
{
    [TestFixture]
    public class TaskRepositoryTests
    {
        private ITaskRepository taskRepo
            = TestApp.Repositories.TaskRepository;

        private IIssueRepository issueRepo
            = TestApp.Repositories.IssueRepository;

        private IProjectRepository projectRepo
            = TestApp.Repositories.ProjectRepository;

        [Test]
        public void InsertTask()
        {
            int projectId = projectRepo
                .GetFirstProject()
                .ProjectId;

            // Setting up:
            Issue issue = Issue.ConstructIssue
                (projectId
                , "InsertTask"
                , Status.Open
                , 1);
            issueRepo.InsertIssue(issue);
            Task task = TestApp.GetTask
                (issue.IssueId.Value
                , 1d
                , 1
                , Status.Open
                , 0d
                , 1);

            // Testing:
            taskRepo.InsertTask(task);

            // Checking:
            Assert.IsNotNull(task.TaskId);
        }

        [Test]
        [ExpectedException(typeof(RepositoryException))]
        public void InsertTask_EmptyDescription()
        {
            var project = projectRepo.GetFirstProject();

            // Setting up:
            Issue issue = Issue.ConstructIssue
                (project.ProjectId
                , "InsertTask_EmptyDescription"
                , Status.Open
                , 13);
            issueRepo.InsertIssue(issue);
            Task task = Task.ConstructTask
                (issue.IssueId.Value
                , string.Empty
                , Status.Open
                , 13
                , 1d
                , 0d
                , null
                , null
                , 1);

            // Testing:
            taskRepo.InsertTask(task);

            // Checking:
            Assert.IsNotNull(task.TaskId);
        }

        [Test]
        public void UpdateTask_ChangedDescription_ChangedOK()
        {
            var project = projectRepo.GetFirstProject();

            // Setting up:
            Issue issue = Issue.ConstructIssue
                (project.ProjectId
                , "UpdateTask_ChangedDescription_ChangedOK"
                , Status.Open
                , 14);
            issueRepo.InsertIssue(issue);
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
            taskRepo.InsertTask(task);
            task.Description = "New description";

            // Testing:
            taskRepo.UpdateTask(task);

            // Checking:
            var issueView = issueRepo.GetIssueView
                (issue.IssueId.Value, Status.Any);
            var retrievedTask = issueView.Tasks
                .Where(t => t.TaskId.Value == task.TaskId.Value);
            Assert.IsNotNull(retrievedTask);
        }

        [Test]
        public void UpdateTask_NullIssue()
        {
            var project = projectRepo.GetFirstProject();

            // Setting up:
            Issue issue = Issue.ConstructIssue
                (project.ProjectId
                , "UpdateTask_NullIssue"
                , Status.Open
                , 16);
            issueRepo.InsertIssue(issue);
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
            taskRepo.InsertTask(task);
            task.Description = "New description";

            // Testing:
            taskRepo.UpdateTask(task);

            // Checking:
            var issueView = issueRepo.GetIssueView
                (issue.IssueId.Value, Status.Any);
            var retrievedTask = issueView.Tasks
                .Where(t => t.TaskId.Value == task.TaskId.Value);
            Assert.IsNotNull(retrievedTask);
        }

        [Test]
        public void DeleteTask_Default_OK()
        {
            int projectId = projectRepo
                .GetFirstProject()
                .ProjectId;

            // Setting up:
            Issue issue = Issue.ConstructIssue
                (projectId
                , "DeleteTask"
                , Status.Open
                , 19);
            issueRepo.InsertIssue(issue);
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
            taskRepo.InsertTask(task);

            // Testing:
            taskRepo.DeleteTask(task.TaskId.Value);

            // Checking:
            var issueView = issueRepo.GetIssueView
                (issue.IssueId.Value, Status.Any);
            var retrievedTask = issueView.Tasks
                .Where(t => t.TaskId.Value == task.TaskId.Value)
                .SingleOrDefault();
            Assert.IsNull(retrievedTask);
        }
    }
}