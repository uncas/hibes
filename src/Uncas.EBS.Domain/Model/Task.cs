using System;

namespace Uncas.EBS.Domain.Model
{
    /// <summary>
    /// Represents a task.
    /// </summary>
    public class Task
    {
        #region Constructors and factory methods

        /// <summary>
        /// Initializes a new instance of the <see cref="Task"/> class.
        /// </summary>
        protected Task()
        {
            this.CreatedDate = DateTime.Now;
            this.Sequence = 1;
            this.Status = Status.Open;
            this.Elapsed = 0d;
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
        /// <param name="refPersonId">The person id.</param>
        /// <returns>The constructed task.</returns>
        public static Task ConstructTask
            (int refIssueId
            , string description
            , Status status
            , int sequence
            , double originalEstimate
            , double elapsed
            , DateTime? startDate
            , DateTime? endDate
            , int refPersonId)
        {
            Task task = new Task();
            task.CurrentEstimate = originalEstimate;
            task.OriginalEstimate = originalEstimate;
            task.Description = description;
            task.Elapsed = elapsed;
            task.EndDate = endDate;
            task.RefIssueId = refIssueId;
            task.Sequence = sequence;
            task.StartDate = startDate;
            task.Status = status;
            task.RefPersonId = refPersonId;
            return task;
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
        /// <param name="refPersonId">The person id.</param>
        /// <returns>The reconstructed task.</returns>
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
            , int refPersonId)
        {
            Task task = new Task();
            task.TaskId = taskId;
            task.CreatedDate = createdDate;
            task.CurrentEstimate = currentEstimate;
            task.Description = description;
            task.Elapsed = elapsed;
            task.EndDate = endDate;
            task.OriginalEstimate = originalEstimate;
            task.RefIssueId = refIssueId;
            task.Sequence = sequence;
            task.StartDate = startDate;
            task.Status = status;
            task.RefPersonId = refPersonId;
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
        /// <param name="refPersonId">The person id.</param>
        /// <returns>The reconstructed task.</returns>
        public static Task ReconstructTaskToUpdate
            (int taskId
            , string description
            , Status status
            , int sequence
            , double currentEstimate
            , double elapsed
            , DateTime? startDate
            , DateTime? endDate
            , int refPersonId)
        {
            Task task = new Task();
            task.TaskId = taskId;
            task.CurrentEstimate = currentEstimate;
            task.Description = description;
            task.Elapsed = elapsed;
            task.EndDate = endDate;
            task.Sequence = sequence;
            task.StartDate = startDate;
            task.Status = status;
            task.RefPersonId = refPersonId;
            return task;
        }

        #endregion

        #region Properties (14)

        #region Administrative properties (2)

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

        #endregion

        #region Attributes (2)

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the issue id.
        /// </summary>
        /// <value>The issue id.</value>
        public int RefIssueId { get; set; }

        #endregion

        #region Work on the task (3)

        /// <summary>
        /// Gets or sets the person id.
        /// </summary>
        /// <value>The person id.</value>
        public int RefPersonId { get; set; }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>The sequence.</value>
        public int Sequence { get; set; }

        private Status status = Status.Open;

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public Status Status
        {
            get
            {
                return status;
            }

            set
            {
                if (value != Status.Any)
                {
                    status = value;
                }
            }
        }

        #endregion

        #region Estimates (2)

        private double originalEstimate;

        /// <summary>
        /// Gets or sets the original estimate in hours.
        /// </summary>
        /// <value>The original estimate in hours.</value>
        public double OriginalEstimate
        {
            get
            {
                return originalEstimate;
            }

            set
            {
                if (value <= 0d)
                {
                    throw new ArgumentOutOfRangeException
                        ("value"
                        , "Original estimate must be non-negative");
                }
                else
                {
                    originalEstimate = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the current estimate in hours.
        /// </summary>
        /// <value>The current estimate in hours.</value>
        public double CurrentEstimate { get; set; }

        #endregion

        // TODO: REFACTOR: Put action properties in TaskDetails.
        #region Action aggregates (5)

        #region Progress (3)

        private double elapsed;

        /// <summary>
        /// Gets or sets the elapsed hours.
        /// </summary>
        /// <value>The elapsed hours.</value>
        public double Elapsed
        {
            get
            {
                return elapsed;
            }

            set
            {
                elapsed = value;

                // Estimate cannot be lower than the elapsed time:
                if (this.CurrentEstimate < this.Elapsed)
                {
                    this.CurrentEstimate = this.Elapsed;
                }
            }
        }

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

        #endregion

        #region Derived properties (2)

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
                {
                    return this.OriginalEstimate / this.Elapsed;
                }
                else
                {
                    return null;
                }
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

        #endregion

        #endregion

        #endregion
    }
}