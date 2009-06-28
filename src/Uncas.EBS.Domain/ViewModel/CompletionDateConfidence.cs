using System;

namespace Uncas.EBS.Domain.ViewModel
{
    /// <summary>
    /// Represents the probability for a completion date.
    /// </summary>
    public class CompletionDateConfidence
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the probability.
        /// </summary>
        /// <value>The probability.</value>
        public double Probability { get; set; }
    }
}