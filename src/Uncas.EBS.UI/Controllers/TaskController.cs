using System;
using System.Collections.Generic;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.UI.Controllers
{
    /// <summary>
    /// Task repository - layer for the web application.
    /// </summary>
    public class TaskController
    {
        private IIssueRepository _issueRepo
            = App.Repositories.IssueRepository;

        private ITaskRepository _taskRepo
            = App.Repositories.TaskRepository;

        public IList<TaskDetails> GetTasks(int issueId, Status status)
        {
            var issueView = _issueRepo.GetIssueView
                (issueId, status);
            return issueView.Tasks;
        }

        public void InsertTask(string Description
            , double OriginalEstimate
            , double Elapsed
            , DateTime? StartDate
            , DateTime? EndDate
            , int Sequence
            , Status Status
            , int RefIssueId
            , int RefPersonId
            )
        {
            Task task = Task.ConstructTask
                (RefIssueId
                , Description
                , Status
                , Sequence
                , OriginalEstimate
                , Elapsed
                , StartDate
                , EndDate
                , RefPersonId);
            _taskRepo.InsertTask(task);
        }

        public void UpdateTask(
            int Original_TaskId
            , string Description
            , double CurrentEstimate
            , double Elapsed
            , DateTime? StartDate
            , DateTime? EndDate
            , int Sequence
            , Status Status
            , int RefPersonId
            )
        {
            Task task = Task.ReconstructTaskToUpdate
                (Original_TaskId
                , Description
                , Status
                , Sequence
                , CurrentEstimate
                , Elapsed
                , StartDate
                , EndDate
                , RefPersonId);
            _taskRepo.UpdateTask(task);
        }

        public void DeleteTask(int Original_TaskId)
        {
            _taskRepo.DeleteTask(Original_TaskId);
        }

        public IList<Task> GetClosedTasks()
        {
            return _taskRepo.GetTasksByStatus(Status.Closed);
        }
    }
}