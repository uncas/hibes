using System.Collections.Generic;
using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.Domain.ViewModel
{
    /// <summary>
    /// Represents a view of an issue
    /// </summary>
    public class IssueView
    {
        /// <summary>
        /// Gets or sets the issue.
        /// </summary>
        /// <value>The issue.</value>
        public IssueDetails Issue { get; set; }

        /// <summary>
        /// Gets or sets the tasks.
        /// </summary>
        /// <value>The tasks.</value>
        public IList<TaskDetails> Tasks { get; set; }
    }
}