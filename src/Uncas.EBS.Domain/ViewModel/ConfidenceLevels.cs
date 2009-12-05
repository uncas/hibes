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
        /// <param name="low">The lower fraction.</param>
        /// <param name="medium">The medium fraction.</param>
        /// <param name="high">The higher fraction.</param>
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
        /// <value>The lower level.</value>
        public double Low { get; set; }

        /// <summary>
        /// Gets or sets the medium.
        /// </summary>
        /// <value>The medium level.</value>
        public double Medium { get; set; }

        /// <summary>
        /// Gets or sets the high.
        /// </summary>
        /// <value>The higher level.</value>
        public double High { get; set; }
    }
}