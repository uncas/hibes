using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.Domain.ViewModel
{
    /// <summary>
    /// Represents an estimate per person.
    /// </summary>
    public class PersonEstimate
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonEstimate"/> class.
        /// </summary>
        /// <param name="personView">The person view.</param>
        /// <param name="evaluations">The evaluations.</param>
        public PersonEstimate
            (PersonView personView
            , IList<double> evaluations)
        {
            this._personView = personView;
            this._evaluations = evaluations;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the selected completion date confidences.
        /// </summary>
        /// <param name="levels">The confidence levels.</param>
        /// <returns>A list of completion date confidences.</returns>
        public IList<CompletionDateConfidence>
            GetSelectedCompletionDateConfidences
            (ConfidenceLevels levels)
        {
            return GetCompletionDateConfidences()
                .Where(cdc => cdc.Probability == levels.Low
                    || cdc.Probability == levels.Medium
                    || cdc.Probability == levels.High)
                .ToList();
        }

        /// <summary>
        /// Gets the completion date confidences.
        /// </summary>
        /// <returns>A list of completion date confidences.</returns>
        /// <value>The completion date confidences.</value>
        /// TODO: [Obsolete("Use overload with paging instead")]
        [SuppressMessage("Microsoft.Design"
            , "CA1024:UsePropertiesWhereAppropriate"
            , Justification = "This is a relatively complex calculation...")]
        public IList<CompletionDateConfidence>
            GetCompletionDateConfidences()
        {
            // Prepares the list of results:
            var result = new List<CompletionDateConfidence>();

            // Orders the evaluations according to the number of hours
            // with the lowest number of hours first:
            var orderedEvaluations
                = this._evaluations.OrderBy(d => d);

            // Gets an estimated date for each whole percentage:
            int count = orderedEvaluations.Count();
            for (int percentage = 1; percentage <= 100; percentage++)
            {
                int countForPercentage = count * percentage / 100;
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

                // Adds the date for the given percentage:
                result.Add(new CompletionDateConfidence
                {
                    Probability = percentage / 100d,
                    Date = date
                });
            }

            return result;
        }

        #endregion

        #region Private fields

        private PersonView _personView { get; set; }

        private IList<double> _evaluations { get; set; }

        #endregion
    }
}