using Uncas.EBS.Domain.Model;
using System.Collections.Generic;

namespace Uncas.EBS.Domain.Repository
{
    public interface ITaskRepository
    {
        void InsertTask(Task task);
        void UpdateTask(Task task);
        void DeleteTask(int taskId);
        IList<Task> GetTasksByStatus(Status status);
    }
}