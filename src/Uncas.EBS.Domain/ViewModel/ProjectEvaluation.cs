using System;
using System.Collections.Generic;
using System.Linq;
using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.Domain.ViewModel
{
    /// <summary>
    /// Represents an evaluation / estimate of an entire project.
    /// </summary>
    public class ProjectEvaluation
    {
        /// <summary>
        /// The standard number of hours per day.
        /// </summary>
        public const double StandardNumberOfHoursPerDay = 7.5d;

        private IList<double> _evaluations = new List<double>();

        private IDictionary<Issue, IssueEvaluation> _issueEvaluations
            = new Dictionary<Issue, IssueEvaluation>();

        /// <summary>
        /// Gets the statistics.
        /// </summary>
        /// <value>The statistics.</value>
        public Statistic<double> Statistics
        {
            get
            {
                return new Statistic<double>(_evaluations
                    , (double evaluation)
                    => evaluation / StandardNumberOfHoursPerDay);
            }
        }

        /// <summary>
        /// Gets or sets the person views.
        /// </summary>
        /// <value>The person views.</value>
        public IList<PersonView> PersonViews { get; set; }

        #region // TODO: PERSON: Put in new class: PersonEstimate.

        // This is only relevant if there is a person given.
        // It needs some properties of the ProjectEvaluation class.
        // How to structure this?

        // class ProjectEvaluation
        // class PersonEstimate : ProjectEvaluation
        // class PersonEstimate
        // {
        //     ProjectEvaluation ProjEval { get; set; }
        // }

        // HACK: PERSON: Simulating internals of new class:

        private PersonView _personView
        {
            get
            {
                return this.PersonViews[0];
            }
        }

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
            // TODO: PERSON:
            // Depends on the following class members:
            //      _evaluations
            //      _personView -> already in 'class'

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

        #endregion

        /// <summary>
        /// Gets the average number of days.
        /// </summary>
        /// <value>The average.</value>
        public double? Average
        {
            get
            {
                if (this.NumberOfOpenTasks > 0)
                {
                    return this.Statistics.Average;
                }
                else
                {
                    // If there are no open tasks, the average is ill-defined:
                    return null;
                }
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
                return this.Statistics.StandardDeviation;
            }
        }

        /// <summary>
        /// Gets the elapsed days.
        /// </summary>
        /// <value>The elapsed.</value>
        public double? Elapsed
        {
            get
            {
                return this._issueEvaluations
                    .Select(i => i.Value)
                    .Sum(i => i.Elapsed)
                    / StandardNumberOfHoursPerDay;
            }
        }

        /// <summary>
        /// Gets the progress.
        /// </summary>
        /// <value>The progress.</value>
        public double? Progress
        {
            get
            {
                return this.Elapsed
                    / (this.Elapsed
                    + this.Average);
            }
        }

        /// <summary>
        /// Gets the number of open issues.
        /// </summary>
        /// <value>The number of open issues.</value>
        public int NumberOfOpenIssues
        {
            get
            {
                return this._issueEvaluations.Count;
            }
        }

        /// <summary>
        /// Gets the number of open tasks.
        /// </summary>
        /// <value>The number of open tasks.</value>
        public int NumberOfOpenTasks
        {
            get
            {
                return _issueEvaluations.Sum(i => i.Value.NumberOfOpenTasks);
            }
        }

        /// <summary>
        /// Adds the evaluation.
        /// </summary>
        /// <param name="evaluation">The evaluation.</param>
        public void AddEvaluation(double evaluation)
        {
            this._evaluations.Add(evaluation);
        }

        /// <summary>
        /// Adds the issue evaluation.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <param name="numberOfOpenTasksForThisIssue">The number of open tasks for this issue.</param>
        /// <param name="elapsed">The number of elapsed hours.</param>
        /// <param name="evaluation">The evaluation.</param>
        public void AddIssueEvaluation(Issue issue
            , int numberOfOpenTasksForThisIssue
            , double? elapsed
            , double evaluation)
        {
            if (_issueEvaluations.ContainsKey(issue))
            {
                _issueEvaluations[issue].AddEvaluation(evaluation);
            }
            else
            {
                _issueEvaluations.Add(issue
                    , new IssueEvaluation(issue
                        , numberOfOpenTasksForThisIssue
                        , elapsed
                        , evaluation
                        ));
            }
        }

        /// <summary>
        /// Gets the issue evaluations.
        /// </summary>
        /// <returns></returns>
        public IList<IssueEvaluation> GetIssueEvaluations()
        {
            return _issueEvaluations.Select(i => i.Value).ToList();
        }

        /// <summary>
        /// Gets the days remaining.
        /// </summary>
        /// <value>The days remaining.</value>
        public int DaysRemaining
        {
            get
            {
                DateTime mediumCompletionDate =
                    GetCompletionDateConfidences()
                    .Where(cdc => cdc.Probability == 0.5d)
                    .FirstOrDefault()
                    .Date;
                return (int)Math.Ceiling
                    (mediumCompletionDate
                    .Subtract(DateTime.Now)
                    .TotalDays);
            }
        }
    }
}