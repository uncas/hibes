﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uncas.EBS.Domain.Simulation;

namespace Uncas.EBS.Domain.ViewModel
{
    public class Statistic<T>
    {
        public Statistic(IEnumerable<T> data
            , Func<T, double> transformToQuantity)
        {
            this.Data = data;
            this._transformToQuantity = transformToQuantity;
        }

        private Func<T, double> _transformToQuantity;

        public IEnumerable<T> Data { get; private set; }

        /// <summary>
        /// Gets the probabilities.
        /// </summary>
        /// <value>The probabilities.</value>
        public IList<IntervalProbability> Probabilities
        {
            get
            {
                // Always round up!
                int min = (int)Math.Ceiling(Min);
                int max = (int)Math.Ceiling(Max);
                int intervalWidth = 1;
                int maxIntervals = 7;
                if (max - min > maxIntervals)
                {
                    intervalWidth = (int)Math.Ceiling
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
                            <= (int)Math.Ceiling(_transformToQuantity(ps))
                        && (int)Math.Ceiling(_transformToQuantity(ps))
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

        public double Average
        {
            get
            {
                return this.Data.Average(ps => _transformToQuantity(ps));
            }
        }

        public double StandardDeviation
        {
            get
            {
                // Standard deviation:
                // sigma = sqrt[ sum[(value-average)^2] / N ]
                double average = this.Average;
                double sumOfSquareDifferences
                    = this.Data.Sum
                    (ps => Math.Pow(_transformToQuantity(ps) - average
                        , 2d));
                double standardDeviation
                    = Math.Sqrt(sumOfSquareDifferences
                    / this.Data.Count());
                return standardDeviation;
            }
        }

        public double Min
        {
            get
            {
                return this.Data
                    .Min(ps => _transformToQuantity(ps));
            }
        }

        public double Max
        {
            get
            {
                return this.Data
                    .Max(ps => _transformToQuantity(ps));
            }
        }

        public int Count
        {
            get
            {
                return this.Data.Count();
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Average={0}\nStandardDeviation={1}\n"
                , this.Average, this.StandardDeviation);
            foreach (var ip in this.Probabilities)
            {
                sb.AppendLine(ip.ToString());
            }
            return sb.ToString();
        }
    }
}