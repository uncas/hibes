﻿using System;
using System.Collections.Generic;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.Domain.Repository
{
    /// <summary>
    /// Handles storage of tasks.
    /// </summary>
    public interface ITaskRepository
    {
        /// <summary>
        /// Gets the tasks.
        /// </summary>
        /// <param name="filter">The filter.</param>
        IList<Task> GetTasks(TaskFilter filter);

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