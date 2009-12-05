using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uncas.EBS.Domain.ViewModel
{
    /// <summary>
    /// Handles statistic for an iterator of some data.
    /// </summary>
    /// <typeparam name="T">The type of the data elements.</typeparam>
    public class Statistic<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Statistic&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="data">The data for the statistic.</param>
        /// <param name="transformToQuantity">The transform to quantity.</param>
        public Statistic
            (IEnumerable<T> data
            , Func<T, double> transformToQuantity)
        {
            this.Data = data;
            this._transformToQuantity = transformToQuantity;
        }

        private Func<T, double> _transformToQuantity;

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>The data for the statistic.</value>
        public IEnumerable<T> Data { get; private set; }

        /// <summary>
        /// Gets the probabilities.
        /// </summary>
        /// <value>The probabilities.</value>
        public IList<IntervalProbability> Probabilities
        {
            get
            {
                // Rounds up:
                Func<double, int> toInt =
                    (double number) => (int)Math.Ceiling(number);
                int min = toInt(Min);
                int max = toInt(Max);
                int intervalWidth = 1;
                int maxIntervals = 11;
                if (max - min > maxIntervals)
                {
                    intervalWidth = toInt
                        ((1d * (max - min))
                        / (1d * (maxIntervals - 1)));
                }
                var probabilities = new List<IntervalProbability>();
                for (int interval = min
                    ; interval <= max
                    ; interval += intervalWidth)
                {
                    int lower = interval;
                    int upper = lower + intervalWidth - 1;
                    int numberInInterval = this.Data.Count
                        (ps => lower
                            <= toInt(_transformToQuantity(ps))
                        && toInt(_transformToQuantity(ps))
                            <= upper);
                    double probability = (1d * numberInInterval)
                        / (1d * this.Count);
                    probabilities.Add(new IntervalProbability
                    {
                        Lower = lower,
                        Upper = upper,
                        Probability = probability
                    });
                }
                return probabilities;
            }
        }

        /// <summary>
        /// Gets the average.
        /// </summary>
        /// <value>The average.</value>
        public double Average
        {
            get
            {
                return this.Data.Average(ps => _transformToQuantity(ps));
            }
        }

        /// <summary>
        /// Gets the standard deviation.
        /// </summary>
        /// <value>The standard deviation.</value>
        public double StandardDeviation
        {
            get
            {
                // Standard deviation:
                // sigma = sqrt[ sum[(value-average)^2] / N ]
                double average = this.Average;
                double sumOfSquareDifferences
                    = this.Data.Sum
                    (ps => Math.Pow
                        (_transformToQuantity(ps) - average
                        , 2d));
                double standardDeviation
                    = Math.Sqrt(sumOfSquareDifferences
                    / this.Data.Count());
                return standardDeviation;
            }
        }

        /// <summary>
        /// Gets the min.
        /// </summary>
        /// <value>The minimum value.</value>
        public double Min
        {
            get
            {
                return this.Data
                    .Min(ps => _transformToQuantity(ps));
            }
        }

        /// <summary>
        /// Gets the max.
        /// </summary>
        /// <value>The maximum value.</value>
        public double Max
        {
            get
            {
                return this.Data
                    .Max(ps => _transformToQuantity(ps));
            }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get
            {
                return this.Data.Count();
            }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat
                ("Average={0}\nStandardDeviation={1}\n"
                , this.Average
                , this.StandardDeviation);
            foreach (var ip in this.Probabilities)
            {
                sb.AppendLine(ip.ToString());
            }
            return sb.ToString();
        }
    }
}