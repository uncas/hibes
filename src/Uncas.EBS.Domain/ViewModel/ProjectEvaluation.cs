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

        private IList<PersonEstimate> PersonEstimates
        {
            get
            {
                // HACK: PERSON: Implement this properly.
                var pe = new List<PersonEstimate>();
                pe.Add(new PersonEstimate
                    (PersonViews[0], this._evaluations));
                return pe;
            }
        }

        /// <summary>
        /// Gets the selected completion date confidences.
        /// </summary>
        /// <returns></returns>
        public IList<CompletionDateConfidence>
            GetSelectedCompletionDateConfidences()
        {
            // HACK: PERSON: Implement this properly.
            return PersonEstimates[0]
                .GetSelectedCompletionDateConfidences();
        }

        public PersonConfidenceDates GetPersonConfidenceDates()
        {
            return new PersonConfidenceDates
                (this.PersonViews[0].PersonId
                , this.PersonViews[0].PersonName
                , GetSelectedCompletionDateConfidences()[0].Date
                , GetSelectedCompletionDateConfidences()[1].Date
                , GetSelectedCompletionDateConfidences()[2].Date);
        }

        /// <summary>
        /// Gets the completion date for 5 percent confidence.
        /// </summary>
        /// <value>The completion date5.</value>
        public DateTime CompletionDate5
        {
            get
            {
                return GetSelectedCompletionDateConfidences()[0].Date;
            }
        }

        /// <summary>
        /// Gets the completion date for 50 percent confidence.
        /// </summary>
        /// <value>The completion date50.</value>
        public DateTime CompletionDate50
        {
            get
            {
                return GetSelectedCompletionDateConfidences()[1].Date;
            }
        }

        /// <summary>
        /// Gets the completion date for 95 percent confidence.
        /// </summary>
        /// <value>The completion date95.</value>
        public DateTime CompletionDate95
        {
            get
            {
                return GetSelectedCompletionDateConfidences()[2].Date;
            }
        }

        /// <summary>
        /// Gets the completion date confidences.
        /// </summary>
        /// <value>The completion date confidences.</value>
        public IList<CompletionDateConfidence>
            GetCompletionDateConfidences()
        {
            // HACK: PERSON: Implement this properly.
            return this.PersonEstimates[0]
                .GetCompletionDateConfidences();
        }

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