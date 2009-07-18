using System.Linq;
using NUnit.Framework;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;

namespace Uncas.EBS.IntegrationTests
{
    [SetUpFixture]
    public class TestApp
    {
        internal static IRepositoryFactory Repositories
            = new DAL.RepositoryFactory();

        private ITaskRepository _taskRepo
            = TestApp.Repositories.TaskRepository;

        private IIssueRepository _issueRepo
            = TestApp.Repositories.IssueRepository;

        private IProjectRepository _projectRepo
            = TestApp.Repositories.ProjectRepository;

        [SetUp]
        public void SetUpIntegratedComponents()
        {
            TearDownDatabase();

            SetUpDatabase();
        }

        [TearDown]
        public void TearDownIntegratedComponents()
        {
            TearDownDatabase();

            SetUpDatabase();
        }

        private void SetUpDatabase()
        {
            Repositories.ProjectRepository.InsertProject("My project");

            var project = _projectRepo.GetProjects().FirstOrDefault();

            Issue closedIssue = new Issue
            {
                RefProjectId = project.ProjectId,
                Title = "Issue 1",
                Priority = 1,
                Status = Status.Closed
            };

            _issueRepo.InsertIssue(closedIssue);

            Issue issueInProgress = new Issue
            {
                RefProjectId = project.ProjectId,
                Title = "Issue 2",
                Priority = 2,
                Status = Status.Open
            };

            _issueRepo.InsertIssue(issueInProgress);

            Task newTask = new Task
            {
                CurrentEstimate = 1d,
                Description = "First task",
                RefIssueId = issueInProgress.IssueId.Value,
                OriginalEstimate = 1d,
                Status = Status.Open
            };

            _taskRepo.InsertTask(newTask);

            Task closedTask = new Task
            {
                CurrentEstimate = 1d,
                Description = "Second task",
                RefIssueId = issueInProgress.IssueId.Value,
                OriginalEstimate = 2d,
                Elapsed = 1d,
                Status = Status.Closed
            };

            _taskRepo.InsertTask(closedTask);

            Issue newIssue = new Issue
            {
                RefProjectId = project.ProjectId,
                Title = "Issue 3",
                Priority = 3,
                Status = Status.Open
            };

            _issueRepo.InsertIssue(newIssue);
        }

        private void TearDownDatabase()
        {
            foreach (var task in
                Repositories.TaskRepository.GetTasks
                (Status.Any, 100000))
            {
                Repositories.TaskRepository.DeleteTask
                    (task.TaskId.Value);
            }

            foreach (var issue in
                Repositories.IssueRepository.GetIssues
                (null, Status.Any))
            {
                Repositories.IssueRepository.DeleteIssue
                    (issue.IssueId.Value);
            }

            foreach (var project in
                Repositories.ProjectRepository.GetProjects())
            {
                Repositories.ProjectRepository.DeleteProject
                    (project.ProjectId);
            }
        }
    }
}