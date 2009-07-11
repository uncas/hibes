using System;
using System.ComponentModel.DataAnnotations;

namespace Uncas.EBS.Domain.Model
{
    /// <summary>
    /// Represents a task.
    /// </summary>
    public class Task
    {
        // TODO: REFACTOR: Make this constructor private.
        /// <summary>
        /// Initializes a new instance of the <see cref="Task"/> class.
        /// </summary>
        public Task()
        {
            this.CreatedDate = DateTime.Now;
            this.Sequence = 1;
            this.Status = Status.Open;
            this.Elapsed = 0d;
        }

        /// <summary>
        /// Reconstructs the task.
        /// </summary>
        /// <param name="taskId">The task id.</param>
        /// <param name="refIssueId">The issue id.</param>
        /// <param name="description">The description.</param>
        /// <param name="status">The status.</param>
        /// <param name="sequence">The sequence.</param>
        /// <param name="originalEstimate">The original estimate.</param>
        /// <param name="currentEstimate">The current estimate.</param>
        /// <param name="elapsed">The elapsed.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="createdDate">The created date.</param>
        /// <returns></returns>
        public static Task ReconstructTask
            (int taskId
            , int refIssueId
            , string description
            , Status status
            , int sequence
            , double originalEstimate
            , double currentEstimate
            , double elapsed
            , DateTime? startDate
            , DateTime? endDate
            , DateTime createdDate
            )
        {
            Task task = new Task();
            task.TaskId = taskId;
            task.CreatedDate = createdDate;
            task.CurrentEstimate
                = currentEstimate;
            task.Description = description;
            task.Elapsed = elapsed;
            task.EndDate = endDate;
            task.OriginalEstimate
                = originalEstimate;
            task.RefIssueId = refIssueId;
            task.Sequence = sequence;
            task.StartDate = startDate;
            task.Status = status;
            return task;
        }

        /// <summary>
        /// Reconstructs the task to update.
        /// </summary>
        /// <param name="taskId">The task id.</param>
        /// <param name="description">The description.</param>
        /// <param name="status">The status.</param>
        /// <param name="sequence">The sequence.</param>
        /// <param name="currentEstimate">The current estimate.</param>
        /// <param name="elapsed">The elapsed.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public static Task ReconstructTaskToUpdate
            (int taskId
            , string description
            , Status status
            , int sequence
            , double currentEstimate
            , double elapsed
            , DateTime? startDate
            , DateTime? endDate
            )
        {
            Task task = new Task();
            task.TaskId = taskId;
            task.CurrentEstimate
                = currentEstimate;
            task.Description = description;
            task.Elapsed = elapsed;
            task.EndDate = endDate;
            task.Sequence = sequence;
            task.StartDate = startDate;
            task.Status = status;
            return task;
        }

        /// <summary>
        /// Constructs the task.
        /// </summary>
        /// <param name="refIssueId">The issue id.</param>
        /// <param name="description">The description.</param>
        /// <param name="status">The status.</param>
        /// <param name="sequence">The sequence.</param>
        /// <param name="originalEstimate">The original estimate.</param>
        /// <param name="elapsed">The elapsed.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public static Task ConstructTask
            (int refIssueId
            , string description
            , Status status
            , int sequence
            , double originalEstimate
            , double elapsed
            , DateTime? startDate
            , DateTime? endDate
            )
        {
            Task task = new Task();
            task.CurrentEstimate
                = originalEstimate;
            task.Description = description;
            task.Elapsed = elapsed;
            task.EndDate = endDate;
            task.OriginalEstimate
                = originalEstimate;
            task.RefIssueId = refIssueId;
            task.Sequence = sequence;
            task.StartDate = startDate;
            task.Status = status;
            return task;
        }

        /// <summary>
        /// Gets or sets the task id.
        /// </summary>
        /// <value>The task id.</value>
        public int? TaskId { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The created date.</value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the issue id.
        /// </summary>
        /// <value>The issue id.</value>
        public int RefIssueId { get; set; }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>The sequence.</value>
        public int Sequence { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [Required]
        public string Description { get; set; }

        private Status _status = Status.Open;
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public Status Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (value != Status.Any)
                {
                    _status = value;
                }
            }
        }

        private double _originalEstimate = 0d;
        /// <summary>
        /// Gets or sets the original estimate in hours.
        /// </summary>
        /// <value>The original estimate in hours.</value>
        public double OriginalEstimate
        {
            get
            {
                return _originalEstimate;
            }
            set
            {
                if (value <= 0d)
                    throw new ArgumentOutOfRangeException();
                else
                    _originalEstimate = value;
            }
        }
        /// <summary>
        /// Gets or sets the current estimate in hours.
        /// </summary>
        /// <value>The current estimate in hours.</value>
        public double CurrentEstimate { get; set; }

        /// <summary>
        /// Gets or sets the elapsed hours.
        /// </summary>
        /// <value>The elapsed hours.</value>
        public double Elapsed { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets the speed.
        /// </summary>
        /// <remarks>
        /// This is calculated as the original estimate in hours
        /// divided by the elapsed hours for closed tasks.
        /// </remarks>
        /// <value>The speed.</value>
        public double? Speed
        {
            get
            {
                if (this.Status == Status.Closed
                    && this.Elapsed > 0d)
                    return this.OriginalEstimate / this.Elapsed;
                else
                    return null;
            }
        }

        /// <summary>
        /// Gets the remaining hours.
        /// </summary>
        /// <value>The remaining hours.</value>
        public double Remaining
        {
            get
            {
                return this.CurrentEstimate - this.Elapsed;
            }
        }

        // TODO: PERSON: There should be a Task.RefPersonId property.
    }
}