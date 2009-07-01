using System.Collections.Generic;
using System.Linq;
using Uncas.EBS.Domain.Model;
using System;

namespace Uncas.EBS.Domain.ViewModel
{
    /// <summary>
    /// Represents an evaluation / estimate of an entire project.
    /// </summary>
    public class ProjectEvaluation
    {
        /// <summary>
        /// The number of hours per day.
        /// </summary>
        public const double NumberOfHoursPerDay = 7.5d;

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
                    => evaluation / NumberOfHoursPerDay);
            }
        }

        /// <summary>
        /// Gets or sets the person offs.
        /// </summary>
        /// <value>The person offs.</value>
        public IList<PersonOff> PersonOffs { get; set; }

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
                _evaluations.OrderBy(d => d);
            int count = this.Statistics.Count;
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
                DateTime date = DateTime.Now;
                double sumOfHours = 0d;
                while (sumOfHours <= hoursAtThisPercentage)
                {
                    date = date.AddDays(1d);
                    if (date.DayOfWeek != DayOfWeek.Saturday
                        && date.DayOfWeek != DayOfWeek.Sunday
                        && !this.PersonOffs.IsPersonOff(date))
                    {
                        sumOfHours += NumberOfHoursPerDay;
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

        /// <summary>
        /// Gets the average.
        /// </summary>
        /// <value>The average.</value>
        public double Average
        {
            get
            {
                return this.Statistics.Average;
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
        /// <param name="evaluation">The evaluation.</param>
        public void AddIssueEvaluation(Issue issue
            , int numberOfOpenTasksForThisIssue
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
    }
}