using System.Collections.Generic;

namespace Uncas.EBS.Domain.ViewModel
{
    /// <summary>
    /// Represents a view of an issue.
    /// </summary>
    public class IssueView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IssueView"/> class.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <param name="tasks">The tasks.</param>
        public IssueView
            (IssueDetails issue
            , IList<TaskDetails> tasks)
        {
            this.Issue = issue;
            this.Tasks = new List<TaskDetails>();
            foreach (TaskDetails td in tasks)
            {
                this.Tasks.Add(td);
            }
        }

        /// <summary>
        /// Gets or sets the issue.
        /// </summary>
        /// <value>The issue.</value>
        public IssueDetails Issue { get; set; }

        /// <summary>
        /// Gets the tasks.
        /// </summary>
        /// <value>The tasks.</value>
        public IList<TaskDetails> Tasks
        {
            get;
            private set;
        }
    }
}