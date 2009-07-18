using System;
using System.Collections.Generic;
using System.Linq;
using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.Domain.ViewModel
{
    /// <summary>
    /// Represents an estimate per person.
    /// </summary>
    public class PersonEstimate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonEstimate"/> class.
        /// </summary>
        /// <param name="personView">The person view.</param>
        /// <param name="evaluations">The evaluations.</param>
        public PersonEstimate(PersonView personView
            , IList<double> evaluations)
        {
            this._personView = personView;
            this._evaluations = evaluations;
        }

        private PersonView _personView { get; set; }

        private IList<double> _evaluations { get; set; }

        /// <summary>
        /// Gets the selected completion date confidences.
        /// </summary>
        /// <returns></returns>
        public IList<CompletionDateConfidence>
            GetSelectedCompletionDateConfidences()
        {
            return GetCompletionDateConfidences()
                .Where(cdc => cdc.Probability == 0.05d
                    || cdc.Probability == 0.5d
                    || cdc.Probability == 0.95d)
                .ToList();
        }

        /// <summary>
        /// Gets the completion date confidences.
        /// </summary>
        /// <value>The completion date confidences.</value>
        public IList<CompletionDateConfidence>
            GetCompletionDateConfidences()
        {
            var result = new List<CompletionDateConfidence>();
            IEnumerable<double> orderedEvaluations =
                this._evaluations.OrderBy(d => d);
            int count = orderedEvaluations.Count();
            for (int percentage = 1; percentage <= 100; percentage++)
            {
                int countForPercentage = (count * percentage) / 100;
                double hoursAtThisPercentage = orderedEvaluations
                    .Skip(countForPercentage - 1).FirstOrDefault();

                // See how many days ahead this amounts to:
                // Run forward one day at a time, beginning tomorrow
                // and sum the number of hours.
                // When the sum equals hoursAtThisPercentage
                // we take the day as the completion date:
                DateTime date = DateTime.Now.Date;
                double sumOfHours = 0d;
                while (sumOfHours <= hoursAtThisPercentage)
                {
                    date = date.AddDays(1d);
                    if (this._personView.IsAtWork(date.DayOfWeek)
                        && !this._personView.PersonOffs
                            .IsPersonOff(date))
                    {
                        sumOfHours += this._personView.HoursPerDay;
                    }
                }

                result.Add(new CompletionDateConfidence
                {
                    Probability = percentage / 100d,
                    Date = date
                });
            }
            return result;
        }
    }
}