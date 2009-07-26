namespace Uncas.EBS.Domain.ViewModel
{
    /// <summary>
    /// Represents confidence levels.
    /// </summary>
    public class ConfidenceLevels
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfidenceLevels"/> class.
        /// </summary>
        /// <param name="low">The low.</param>
        /// <param name="medium">The medium.</param>
        /// <param name="high">The high.</param>
        public ConfidenceLevels
            (double low
            , double medium
            , double high)
        {
            this.Low = low;
            this.Medium = medium;
            this.High = high;
        }

        /// <summary>
        /// Gets or sets the low.
        /// </summary>
        /// <value>The low.</value>
        public double Low { get; set; }

        /// <summary>
        /// Gets or sets the medium.
        /// </summary>
        /// <value>The medium.</value>
        public double Medium { get; set; }

        /// <summary>
        /// Gets or sets the high.
        /// </summary>
        /// <value>The high.</value>
        public double High { get; set; }
    }
}