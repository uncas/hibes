using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.ViewModel;
using Model = Uncas.EBS.Domain.Model;

namespace Uncas.EBS.DAL
{
    /// <summary>
    /// Handles storage of tasks.
    /// </summary>
    public class TaskRepository : BaseRepository, ITaskRepository
    {
        #region ITaskRepository Members

        /// <summary>
        /// Inserts the task.
        /// </summary>
        /// <param name="task">The task to be inserted.</param>
        public void InsertTask(Model.Task task)
        {
            if (string.IsNullOrEmpty(task.Description))
            {
                throw new RepositoryException("Task description required.");
            }

            var databaseTask = GetDbTaskFromModelTask(task);
            DB.Tasks.InsertOnSubmit(databaseTask);
            this.SubmitChanges();
            task.TaskId = databaseTask.TaskId;
        }

        /// <summary>
        /// Updates the task.
        /// </summary>
        /// <param name="task">The task to update.</param>
        public void UpdateTask(Model.Task task)
        {
            if (!task.TaskId.HasValue)
            {
                throw new RepositoryException("TaskId must have a value");
            }

            var databaseTask = GetTask(task.TaskId.Value);
            if (databaseTask == null)
            {
                throw new RepositoryException("Task not found in database");
            }

            UpdateValuesToDbTask(databaseTask, task);
            this.SubmitChanges();
        }

        /// <summary>
        /// Deletes the task.
        /// </summary>
        /// <param name="taskId">The task id.</param>
        public void DeleteTask(int taskId)
        {
            DB.Tasks.DeleteOnSubmit(GetTask(taskId));
            this.SubmitChanges();
        }

        /// <summary>
        /// Gets the tasks.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>A list of tasks.</returns>
        public IList<Model.Task> GetTasks(TaskFilter filter)
        {
            var result = DB.Tasks.AsQueryable<Task>();
            if (filter.Status != Model.Status.Any)
            {
                result = result
                    .Where(t => t.RefStatusId
                        == (int)filter.Status);
            }

            if (filter.PersonId.HasValue)
            {
                result = result
                    .Where(t => t.RefPersonId
                        == filter.PersonId.Value);
            }

            result = result.OrderBy(t => t.Sequence);
            if (filter.MaxCount.HasValue)
            {
                result = result.Take(filter.MaxCount.Value);
            }

            return result
                .Select(t => GetModelTaskFromDbTask(t))
                .ToList();
        }

        #endregion

        [SuppressMessage("Microsoft.Performance"
            , "CA1811:AvoidUncalledPrivateCode"
            , Justification = "Called in Linq. Why not visible to FxCop?")]
        internal static TaskDetails GetTaskDetailsFromDbTask
            (Task databaseTask)
        {
            return new TaskDetails
            {
                TaskId = databaseTask.TaskId,
                RefIssueId = databaseTask.RefIssueId,
                Description = databaseTask.Description,
                Status = (Model.Status)databaseTask.RefStatusId,
                Sequence = databaseTask.Sequence,
                OriginalEstimate
                    = (double)databaseTask.OriginalEstimateInHours,
                CurrentEstimate
                    = (double)databaseTask.CurrentEstimateInHours,
                Elapsed = (double)databaseTask.ElapsedHours,
                StartDate = databaseTask.StartDate,
                EndDate = databaseTask.EndDate,
                CreatedDate = databaseTask.CreatedDate,
                RefPersonId = databaseTask.RefPersonId,
                PersonName = databaseTask.Person.PersonName
            };
        }

        internal IQueryable<TaskDetails> GetTasks(int issueId
            , Model.Status status)
        {
            var result = DB.Tasks
                .Where(t => t.RefIssueId == issueId);
            if (status != Model.Status.Any)
            {
                result = result
                    .Where(t => t.RefStatusId == (int)status);
            }

            result = result.OrderBy(t => t.Sequence);
            return result.Select(t => GetTaskDetailsFromDbTask(t));
        }

        [SuppressMessage("Microsoft.Performance"
            , "CA1811:AvoidUncalledPrivateCode"
            , Justification = "Called in Linq. Why not visible to FxCop?")]
        private static Model.Task GetModelTaskFromDbTask(Task databaseTask)
        {
            return Model.Task.ReconstructTask
                (databaseTask.TaskId
                , databaseTask.RefIssueId
                , databaseTask.Description
                , (Model.Status)databaseTask.RefStatusId
                , databaseTask.Sequence
                , (double)databaseTask.OriginalEstimateInHours
                , (double)databaseTask.CurrentEstimateInHours
                , (double)databaseTask.ElapsedHours
                , databaseTask.StartDate
                , databaseTask.EndDate
                , databaseTask.CreatedDate
                , databaseTask.RefPersonId);
        }

        private static Task GetDbTaskFromModelTask(Model.Task task)
        {
            var databaseTask = new Task();
            AssignValuesToDbTask(databaseTask, task);
            if (task.TaskId.HasValue)
            {
                databaseTask.TaskId = task.TaskId.Value;
            }

            return databaseTask;
        }

        private static void AssignValuesToDbTask
            (Task databaseTask
            , Model.Task task)
        {
            // Never changed:
            databaseTask.OriginalEstimateInHours
                = (decimal)task.OriginalEstimate;
            databaseTask.RefIssueId = task.RefIssueId;
            databaseTask.CreatedDate = task.CreatedDate;

            // These might change later:
            UpdateValuesToDbTask(databaseTask, task);
        }

        private static void UpdateValuesToDbTask
            (Task databaseTask
            , Model.Task task)
        {
            databaseTask.CurrentEstimateInHours
                = (decimal)task.CurrentEstimate;
            databaseTask.Description = task.Description;
            databaseTask.ElapsedHours = (decimal)task.Elapsed;
            databaseTask.EndDate = task.EndDate;
            databaseTask.Sequence = task.Sequence;
            databaseTask.StartDate = task.StartDate;
            databaseTask.RefStatusId = (int)task.Status;
            databaseTask.RefPersonId = task.RefPersonId;
        }

        private Task GetTask(int taskId)
        {
            return DB.Tasks
                .Where(t => t.TaskId == taskId)
                .SingleOrDefault();
        }
    }
}