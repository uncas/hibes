using Uncas.EBS.Domain.Model;
using System.Collections.Generic;

namespace Uncas.EBS.Domain.Repository
{
    /// <summary>
    /// Handles storage of tasks.
    /// </summary>
    public interface ITaskRepository
    {
        // TODO: Refactor: Combine the two Get methods below:
        // Maybe create and use TaskSpecification: Status, MaxCount.

        /// <summary>
        /// Gets the tasks by status.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        IList<Task> GetTasksByStatus(Status status);

        /// <summary>
        /// Gets the tasks.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="maxCount">The max count.</param>
        /// <returns></returns>
        IList<Task> GetTasks(Status status, int maxCount);

        /// <summary>
        /// Inserts the task.
        /// </summary>
        /// <param name="task">The task.</param>
        void InsertTask(Task task);

        /// <summary>
        /// Updates the task.
        /// </summary>
        /// <param name="task">The task.</param>
        void UpdateTask(Task task);

        /// <summary>
        /// Deletes the task.
        /// </summary>
        /// <param name="taskId">The task id.</param>
        void DeleteTask(int taskId);
    }
}