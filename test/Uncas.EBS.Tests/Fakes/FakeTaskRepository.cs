using System;
using System.Collections.Generic;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.Tests.Fakes
{
    internal class FakeTaskRepository : ITaskRepository
    {
        #region ITaskRepository Members

        public IList<Task> GetTasks(TaskFilter filter)
        {
            return new List<Task>();
        }

        public void InsertTask(Task task)
        {
            throw new NotImplementedException();
        }

        public void UpdateTask(Task task)
        {
            throw new NotImplementedException();
        }

        public void DeleteTask(int taskId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
