using System;
using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.Domain.ViewModel
{
    /// <summary>
    /// Represents details about an issue.
    /// </summary>
    /// <remarks>
    /// Adds additional properties on top of an issue.
    /// </remarks>
    public class IssueDetails : Issue
    {
        #region Constructors

        /// <summary>
        /// Reconstructs the issue details.
        /// </summary>
        /// <param name="issueId">The issue id.</param>
        /// <param name="createdDate">The created date.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="status">The status.</param>
        /// <param name="title">The title.</param>
        /// <param name="numberOfTasks">The number of tasks.</param>
        /// <param name="remaining">The remaining.</param>
        /// <param name="elapsed">The elapsed.</param>
        /// <returns></returns>
        public static IssueDetails ReconstructIssueDetails
            (
                int issueId
                , DateTime createdDate
                , int priority
                , string projectName
                , Status status
                , string title
                , int numberOfTasks
                , double? remaining
                , double? elapsed
            )
        {
            return new IssueDetails
            {
                IssueId = issueId,
                CreatedDate = createdDate,
                Priority = priority,
                ProjectName = projectName,
                Status = status,
                Title = title,
                NumberOfTasks = numberOfTasks,
                Remaining = remaining,
                Elapsed = elapsed
            };
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        /// <value>The name of the project.</value>
        public string ProjectName { get; set; }

        /// <summary>
        /// Gets or sets the number of tasks.
        /// </summary>
        /// <value>The number of tasks.</value>
        public int NumberOfTasks { get; set; }

        /// <summary>
        /// Gets or sets the remaining.
        /// </summary>
        /// <value>The remaining.</value>
        public double? Remaining { get; set; }

        /// <summary>
        /// Gets or sets the elapsed.
        /// </summary>
        /// <value>The elapsed.</value>
        public double? Elapsed { get; set; }

        /// <summary>
        /// Gets the total.
        /// </summary>
        /// <value>The total.</value>
        public double? Total
        {
            get
            {
                if (this.Elapsed.HasValue)
                {
                    double total = this.Elapsed.Value;
                    if (this.Remaining.HasValue)
                    {
                        total += this.Remaining.Value;
                    }
                    return total;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the fraction elapsed.
        /// </summary>
        /// <value>The fraction elapsed.</value>
        public double? FractionElapsed
        {
            get
            {
                if (this.Total.HasValue
                    && this.Elapsed.HasValue
                    && this.Total.Value != 0d)
                {
                    return this.Elapsed.Value
                        / this.Total.Value;
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion
    }
}