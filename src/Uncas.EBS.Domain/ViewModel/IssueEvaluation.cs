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
        /// <param name="evaluation">The evaluation.</param>
        public IssueEvaluation(Issue issue
            , int numberOfOpenTasks
            , double evaluation)
        {
            this.Issue = issue;
            this.AddEvaluation(evaluation);
            this.NumberOfOpenTasks = numberOfOpenTasks;
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
        public double Average
        {
            get
            {
                return this._sum / this._count 
                    / ProjectEvaluation.NumberOfHoursPerDay;
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