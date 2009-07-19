using System;
using System.Collections.Generic;
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
            db.Tasks.InsertOnSubmit(dbTask);
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
            db.Tasks.DeleteOnSubmit(GetTask(taskId));
            base.SubmitChanges();
        }

        public IList<Model.Task> GetTasks
            (Model.Status status
            , int? maxCount)
        {
            var result = db.Tasks.AsQueryable<Task>();
            if (status != Model.Status.Any)
            {
                result = result
                    .Where(t => t.RefStatusId == (int)status);
            }
            result = result.OrderByDescending(t => t.EndDate);
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
            return db.Tasks
                .Where(t => t.TaskId == taskId)
                .SingleOrDefault();
        }

        internal IQueryable<TaskDetails> GetTasks(int issueId
            , Model.Status status)
        {
            var result = db.Tasks
                .Where(t => t.RefIssueId == issueId);
            if (status != Model.Status.Any)
            {
                result = result
                    .Where(t => t.RefStatusId == (int)status);
            }
            return result.Select(t => GetTaskDetailsFromDbTask(t));
        }

        private static Random _rnd = new Random(1);

        internal TaskDetails GetTaskDetailsFromDbTask(Task dbTask)
        {
            int count = PersonRepository.PersonViews.Count;
            var personView
                = PersonRepository.PersonViews[_rnd.Next(count)];
            // TODO: PERSON: Retrieve person id and name from dbTask:
            return new TaskDetails
                {
                    TaskId = dbTask.TaskId
                ,
                    RefIssueId = dbTask.RefIssueId
                ,
                    Description = dbTask.Description
                ,
                    Status = (Model.Status)dbTask.RefStatusId
                ,
                    Sequence = dbTask.Sequence
                ,
                    OriginalEstimate = (double)dbTask.OriginalEstimateInHours
                ,
                    CurrentEstimate = (double)dbTask.CurrentEstimateInHours
                ,
                    Elapsed = (double)dbTask.ElapsedHours
                ,
                    StartDate = dbTask.StartDate
                ,
                    EndDate = dbTask.EndDate
                ,
                    CreatedDate = dbTask.CreatedDate
                ,
                    RefPersonId = personView.PersonId
                ,
                    PersonName = personView.PersonName
                };
        }

        internal Model.Task GetModelTaskFromDbTask(Task dbTask)
        {
            // TODO: PERSON: Retrieve person id from dbTask:
            int count = PersonRepository.PersonViews.Count;
            var personView
                = PersonRepository.PersonViews[_rnd.Next(count)];
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
                , personView.PersonId
                );
        }

        internal Task GetDbTaskFromModelTask(Model.Task task)
        {
            Task dbTask = new Task();
            AssignValuesToDbTask(dbTask, task);
            if (task.TaskId.HasValue)
            {
                dbTask.TaskId = task.TaskId.Value;
            }
            return dbTask;
        }

        private void AssignValuesToDbTask(Task dbTask
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

        private void UpdateValuesToDbTask(Task dbTask
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
            // TODO: PERSON: Save Task.RefPersonId to database.
            // dbTask.RefPersonId = task.RefPersonId;
        }
    }
}