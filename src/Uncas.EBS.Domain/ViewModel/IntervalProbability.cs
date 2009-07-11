namespace Uncas.EBS.Domain.ViewModel
{
    /// <summary>
    /// Handles the probability to be in a given interval.
    /// </summary>
    public class IntervalProbability
    {
        /// <summary>
        /// Gets or sets the lower limit of the interval.
        /// </summary>
        /// <value>The lower.</value>
        public int Lower { get; set; }

        /// <summary>
        /// Gets or sets the upper limit of the interval.
        /// </summary>
        /// <value>The upper.</value>
        public int Upper { get; set; }

        /// <summary>
        /// Gets or sets the probability.
        /// </summary>
        /// <value>The probability.</value>
        public double Probability { get; set; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}-{1}: {2:P1}"
                , this.Lower, this.Upper, this.Probability);
        }
    }
}