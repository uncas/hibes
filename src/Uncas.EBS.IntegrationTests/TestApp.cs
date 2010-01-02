using System;
using System.Linq;
using NUnit.Framework;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.IntegrationTests
{
    [SetUpFixture]
    public class TestApp
    {
        private static IRepositoryFactory _repositories
            = new DAL.RepositoryFactory();

        internal static IRepositoryFactory Repositories
        {
            get
            {
                return _repositories;
            }
        }

        private ITaskRepository _taskRepo
            = Repositories.TaskRepository;

        private IIssueRepository _issueRepo
            = Repositories.IssueRepository;

        private IProjectRepository _projectRepo
            = Repositories.ProjectRepository;

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

        private Random _rnd = new Random();

        private void AddIssue()
        {
            var project = _projectRepo.GetProjects().FirstOrDefault();

            int priority = _rnd.Next(100);

            Status status = GetStatus(priority);

            Issue issue = Issue.ConstructIssue
                (project.ProjectId
                , "Issue" + priority
                , status
                , priority);

            _issueRepo.InsertIssue(issue);

            if (issue.IssueId.HasValue)
            {
                for (int taskIndex = 0
                    ; taskIndex < 2 + _rnd.Next(3)
                    ; taskIndex++)
                {
                    AddTask(issue.IssueId.Value, status);
                }
            }
        }

        private static Status GetStatus(int priority)
        {
            Status status
                = priority % 2 == 0
                ? Status.Open
                : Status.Closed;
            return status;
        }

        private void AddTask(int issueId, Status issueStatus)
        {
            double originalEstimate
                = Math.Ceiling(_rnd.NextDouble() * 15d);

            int sequence = _rnd.Next(50);

            Status status
                = (issueStatus == Status.Closed
                ? Status.Closed
                : GetStatus(sequence));

            double elapsed = 0d;
            if (status == Status.Closed)
            {
                double speed
                    = 1d + 0.95d * (2d * _rnd.NextDouble() - 1d);
                elapsed
                    = originalEstimate / speed;
            }
            else if (_rnd.Next(10) < 1)
            {
                // Some open tasks are in progress:
                elapsed = _rnd.NextDouble() * 20d;
            }

            double currentEstimate = elapsed;

            if (status == Status.Open)
            {
                currentEstimate += _rnd.NextDouble() * 10d;
            }

            int personId = GetPersonId();

            Task task = GetTask
                (issueId
                , originalEstimate
                , sequence
                , status
                , elapsed
                , personId);

            task.CurrentEstimate = currentEstimate;

            _taskRepo.InsertTask(task);
        }

        private int GetPersonId()
        {
            return Repositories
                .PersonRepository
                .GetPersons()
                .FirstOrDefault()
                .PersonId;
        }

        internal static Task GetTask
            (int issueId
            , double originalEstimate
            , int sequence
            , Status status
            , double elapsed
            , int personId)
        {
            Task task = Task.ConstructTask
                (issueId
                , "Task " + sequence
                , status
                , sequence
                , originalEstimate
                , elapsed
                , null
                , null
                , personId
                );
            return task;
        }

        private void SetUpDatabase()
        {
            Repositories.ProjectRepository.InsertProject("My project");

            var project = _projectRepo.GetProjects().FirstOrDefault();

            for (int issueIndex = 0
                ; issueIndex < 15
                ; issueIndex++)
            {
                AddIssue();
            }
        }

        private void TearDownDatabase()
        {
            foreach (var task in
                Repositories.TaskRepository.GetTasks
                (TaskFilter.None))
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