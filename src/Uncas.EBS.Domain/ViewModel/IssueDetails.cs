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
    }
}