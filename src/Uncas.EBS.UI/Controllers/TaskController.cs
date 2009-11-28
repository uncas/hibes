using System;
using System.Collections.Generic;
using System.Linq;
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

        public IList<TaskDetails> GetTasks
            (int issueId
            , Status status)
        {
            var issueView = _issueRepo.GetIssueView
                (issueId, status);
            return issueView.Tasks;
        }

        public void InsertTask(string description
            , double originalEstimate
            , double elapsed
            , DateTime? startDate
            , DateTime? endDate
            , int sequence
            , Status status
            , int refIssueId
            , int refPersonId
            )
        {
            Task task = Task.ConstructTask
                (refIssueId
                , description
                , status
                , sequence
                , originalEstimate
                , elapsed
                , startDate
                , endDate
                , refPersonId);
            _taskRepo.InsertTask(task);
        }

        public void UpdateTask(
            int taskId
            , string description
            , double currentEstimate
            , double elapsed
            , DateTime? startDate
            , DateTime? endDate
            , int sequence
            , Status status
            , int refPersonId
            )
        {
            Task task = Task.ReconstructTaskToUpdate
                (taskId
                , description
                , status
                , sequence
                , currentEstimate
                , elapsed
                , startDate
                , endDate
                , refPersonId);
            _taskRepo.UpdateTask(task);
        }

        public void DeleteTask(int taskId)
        {
            _taskRepo.DeleteTask(taskId);
        }

        public IList<Task> GetClosedTasks(int? refPersonId)
        {
            // TODO: REFACTOR: Move this into repository:
            var result = _taskRepo.GetTasks(Status.Closed, null);
            if (refPersonId.HasValue)
            {
                return result.Where
                    (p => p.RefPersonId == refPersonId.Value)
                    .ToList();
            }
            else
            {
                return result;
            }
        }
    }
}