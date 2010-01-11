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
        private IIssueRepository issueRepo
            = App.Repositories.IssueRepository;

        private ITaskRepository taskRepo
            = App.Repositories.TaskRepository;

        /// <summary>
        /// Gets the tasks.
        /// </summary>
        /// <param name="issueId">The issue id.</param>
        /// <param name="status">The status.</param>
        /// <returns>A list of task details.</returns>
        public IList<TaskDetails> GetTasks
            (int issueId
            , Status status)
        {
            var issueView = issueRepo.GetIssueView
                (issueId, status);
            return issueView.Tasks;
        }

        /// <summary>
        /// Inserts the task.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="originalEstimate">The original estimate.</param>
        /// <param name="elapsed">The elapsed.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="sequence">The sequence.</param>
        /// <param name="status">The status.</param>
        /// <param name="refIssueId">The ref issue id.</param>
        /// <param name="refPersonId">The ref person id.</param>
        public void InsertTask
            (string description
            , double originalEstimate
            , double elapsed
            , DateTime? startDate
            , DateTime? endDate
            , int sequence
            , Status status
            , int refIssueId
            , int refPersonId)
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
            taskRepo.InsertTask(task);
        }

        /// <summary>
        /// Updates the task.
        /// </summary>
        /// <param name="taskId">The task id.</param>
        /// <param name="description">The description.</param>
        /// <param name="currentEstimate">The current estimate.</param>
        /// <param name="elapsed">The elapsed.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="sequence">The sequence.</param>
        /// <param name="status">The status.</param>
        /// <param name="refPersonId">The ref person id.</param>
        public void UpdateTask
            (int taskId
            , string description
            , double currentEstimate
            , double elapsed
            , DateTime? startDate
            , DateTime? endDate
            , int sequence
            , Status status
            , int refPersonId)
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
            taskRepo.UpdateTask(task);
        }

        /// <summary>
        /// Deletes the task.
        /// </summary>
        /// <param name="taskId">The task id.</param>
        public void DeleteTask(int taskId)
        {
            taskRepo.DeleteTask(taskId);
        }

        /// <summary>
        /// Gets the closed tasks.
        /// </summary>
        /// <param name="refPersonId">The ref person id.</param>
        /// <returns>A list of tasks.</returns>
        public IList<Task> GetClosedTasks(int? refPersonId)
        {
            TaskFilter filter = new TaskFilter
            {
                PersonId = refPersonId,
                Status = Status.Closed
            };
            return taskRepo.GetTasks(filter);
        }
    }
}