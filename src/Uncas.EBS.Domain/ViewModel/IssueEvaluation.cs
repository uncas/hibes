using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.Domain.ViewModel
{
    /// <summary>
    /// Represents an evaluation / estimate of an issue.
    /// </summary>
    public class IssueEvaluation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IssueEvaluation"/> class.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <param name="numberOfOpenTasks">The number of open tasks.</param>
        /// <param name="elapsed">The number of elapsed hours.</param>
        /// <param name="evaluation">The evaluation.</param>
        public IssueEvaluation(Issue issue
            , int numberOfOpenTasks
            , double? elapsed
            , double evaluation)
        {
            this.Issue = issue;
            this.NumberOfOpenTasks = numberOfOpenTasks;
            this.Elapsed = elapsed;
            this.AddEvaluation(evaluation);
        }

        /// <summary>
        /// Gets or sets the issue.
        /// </summary>
        /// <value>The issue.</value>
        public Issue Issue { get; private set; }

        /// <summary>
        /// Gets or sets the number of open tasks.
        /// </summary>
        /// <value>The number of open tasks.</value>
        public int NumberOfOpenTasks { get; private set; }

        /// <summary>
        /// Gets the issue id.
        /// </summary>
        /// <value>The issue id.</value>
        public int IssueId
        {
            get
            {
                return this.Issue.IssueId.Value;
            }
        }

        /// <summary>
        /// Gets the name of the project.
        /// </summary>
        /// <value>The name of the project.</value>
        public string ProjectName
        {
            get
            {
                return this.Issue.ProjectName;
            }
        }

        /// <summary>
        /// Gets the issue title.
        /// </summary>
        /// <value>The issue title.</value>
        public string IssueTitle
        {
            get
            {
                return this.Issue.Title;
            }
        }

        /// <summary>
        /// Gets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public int Priority
        {
            get
            {
                return this.Issue.Priority;
            }
        }

        /// <summary>
        /// Gets the average days.
        /// </summary>
        /// <value>The average days.</value>
        public double? Average
        {
            get
            {
                if (this.NumberOfOpenTasks > 0)
                {
                    return this._sum / this._count
                        / ProjectEvaluation.NumberOfHoursPerDay;
                }
                else
                {
                    // If there are no tasks, the average is not well-defined:
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the elapsed hours.
        /// </summary>
        /// <value>The elapsed hours.</value>
        public double? Elapsed { get; private set; }

        /// <summary>
        /// Gets the progress (elapsed divided by estimated total).
        /// </summary>
        /// <value>The progress.</value>
        public double? Progress
        {
            get
            {
                if (this.Average.HasValue)
                {
                    return this.Elapsed
                        / (this.Elapsed
                        + this.Average * ProjectEvaluation.NumberOfHoursPerDay);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Adds the evaluation.
        /// </summary>
        /// <param name="evaluation">The evaluation.</param>
        public void AddEvaluation(double evaluation)
        {
            this._count++;
            this._sum += evaluation;
        }

        private double _sum = 0d;
        private int _count = 0;
    }
}