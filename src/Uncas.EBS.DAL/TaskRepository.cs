using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.ViewModel;
using Model = Uncas.EBS.Domain.Model;

namespace Uncas.EBS.DAL
{
    public class TaskRepository : BaseRepository, ITaskRepository
    {
        #region ITaskRepository Members

        public void InsertTask(Model.Task task)
        {
            if (string.IsNullOrEmpty(task.Description))
            {
                throw new RepositoryException("Task description required.");
            }
            var dbTask = GetDbTaskFromModelTask(task);
            DB.Tasks.InsertOnSubmit(dbTask);
            base.SubmitChanges();
            task.TaskId = dbTask.TaskId;
        }

        public void UpdateTask(Model.Task task)
        {
            if (!task.TaskId.HasValue)
            {
                throw new RepositoryException("TaskId must have a value");
            }
            var dbTask = GetTask(task.TaskId.Value);
            if (dbTask == null)
            {
                throw new RepositoryException("Task not found in database");
            }
            UpdateValuesToDbTask(dbTask, task);
            base.SubmitChanges();
        }

        public void DeleteTask(int taskId)
        {
            DB.Tasks.DeleteOnSubmit(GetTask(taskId));
            base.SubmitChanges();
        }

        public IList<Model.Task> GetTasks
            (Model.Status status
            , int? maxCount)
        {
            var result = DB.Tasks.AsQueryable<Task>();
            if (status != Model.Status.Any)
            {
                result = result
                    .Where(t => t.RefStatusId == (int)status);
            }
            result = result.OrderBy(t => t.Sequence);
            if (maxCount.HasValue)
            {
                result = result.Take(maxCount.Value);
            }
            return result
                .Select(t => GetModelTaskFromDbTask(t))
                .ToList();
        }

        #endregion

        private Task GetTask(int taskId)
        {
            return DB.Tasks
                .Where(t => t.TaskId == taskId)
                .SingleOrDefault();
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
        internal static TaskDetails GetTaskDetailsFromDbTask
            (Task dbTask)
        {
            return new TaskDetails
                {
                    TaskId = dbTask.TaskId,
                    RefIssueId = dbTask.RefIssueId,
                    Description = dbTask.Description,
                    Status = (Model.Status)dbTask.RefStatusId,
                    Sequence = dbTask.Sequence,
                    OriginalEstimate
                        = (double)dbTask.OriginalEstimateInHours,
                    CurrentEstimate
                        = (double)dbTask.CurrentEstimateInHours,
                    Elapsed = (double)dbTask.ElapsedHours,
                    StartDate = dbTask.StartDate,
                    EndDate = dbTask.EndDate,
                    CreatedDate = dbTask.CreatedDate,
                    RefPersonId = dbTask.RefPersonId,
                    PersonName = dbTask.Person.PersonName
                };
        }

        [SuppressMessage("Microsoft.Performance"
            , "CA1811:AvoidUncalledPrivateCode"
            , Justification = "Called in Linq. Why not visible to FxCop?")]
        private static Model.Task GetModelTaskFromDbTask(Task dbTask)
        {
            return Model.Task.ReconstructTask
                (dbTask.TaskId
                , dbTask.RefIssueId
                , dbTask.Description
                , (Model.Status)dbTask.RefStatusId
                , dbTask.Sequence
                , (double)dbTask.OriginalEstimateInHours
                , (double)dbTask.CurrentEstimateInHours
                , (double)dbTask.ElapsedHours
                , dbTask.StartDate
                , dbTask.EndDate
                , dbTask.CreatedDate
                , dbTask.RefPersonId
                );
        }

        private static Task GetDbTaskFromModelTask(Model.Task task)
        {
            Task dbTask = new Task();
            AssignValuesToDbTask(dbTask, task);
            if (task.TaskId.HasValue)
            {
                dbTask.TaskId = task.TaskId.Value;
            }
            return dbTask;
        }

        private static void AssignValuesToDbTask(Task dbTask
            , Model.Task task)
        {
            // Never changed:
            dbTask.OriginalEstimateInHours
                = (decimal)task.OriginalEstimate;
            dbTask.RefIssueId = task.RefIssueId;
            dbTask.CreatedDate = task.CreatedDate;

            // These might change later:
            UpdateValuesToDbTask(dbTask, task);
        }

        private static void UpdateValuesToDbTask
            (Task dbTask
            , Model.Task task)
        {
            dbTask.CurrentEstimateInHours
                = (decimal)task.CurrentEstimate;
            dbTask.Description = task.Description;
            dbTask.ElapsedHours = (decimal)task.Elapsed;
            dbTask.EndDate = task.EndDate;
            dbTask.Sequence = task.Sequence;
            dbTask.StartDate = task.StartDate;
            dbTask.RefStatusId = (int)task.Status;
            dbTask.RefPersonId = task.RefPersonId;
        }
    }
}