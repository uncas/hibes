using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.Domain.ViewModel
{
    /// <summary>
    /// Represents an evaluation / estimate of an entire project.
    /// </summary>
    public class ProjectEvaluation
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="ProjectEvaluation"/> class.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <param name="standardNumberOfHoursPerDay">
        /// The standard number of hours per day.</param>
        public ProjectEvaluation
            (
                PersonView person
                , double standardNumberOfHoursPerDay
            )
        {
            this.Person = person;
            this._standardNumberOfHoursPerDay
                = standardNumberOfHoursPerDay;
        }

        #endregion



        #region Public properties


        /// <summary>
        /// Gets the statistics.
        /// </summary>
        /// <value>The statistics.</value>
        public Statistic<double> Statistics
        {
            get
            {
                return new Statistic<double>
                    (
                    _evaluations
                    , (double evaluation)
                        => evaluation
                        / _standardNumberOfHoursPerDay
                    );
            }
        }


        /// <summary>
        /// Gets the average number of remaining days.
        /// </summary>
        /// <value>The average number of remaining days.</value>
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
                    // If there are no open tasks, 
                    // the average is ill-defined:
                    return null;
                }
            }
        }


        /// <summary>
        /// Gets the standard deviation of remaining days.
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
        /// <value>The elapsed days.</value>
        public double? Elapsed
        {
            get
            {
                return this._issueEvaluations
                    .Select(i => i.Value)
                    .Sum(i => i.Elapsed)
                    / _standardNumberOfHoursPerDay;
            }
        }


        /// <summary>
        /// Gets the progressed fraction.
        /// </summary>
        /// <value>The progressed fraction.</value>
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
                return _issueEvaluations.Sum
                    (i => i.Value.NumberOfOpenTasks);
            }
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


        #endregion



        #region Public methods


        /// <summary>
        /// Gets the selected completion date confidences.
        /// </summary>
        /// <returns></returns>
        public IList<CompletionDateConfidence>
            GetSelectedCompletionDateConfidences(ConfidenceLevels levels)
        {
            return PersonEstimate
                .GetSelectedCompletionDateConfidences(levels);
        }


        /// <summary>
        /// Gets the person confidence dates.
        /// </summary>
        /// <returns></returns>
        public PersonConfidenceDates GetPersonConfidenceDates
            (ConfidenceLevels levels)
        {
            var selectedCompletionDateConfidences
                = GetSelectedCompletionDateConfidences(levels);

            return new PersonConfidenceDates
                (this.Person.PersonId
                , this.Person.PersonName
                , selectedCompletionDateConfidences[0].Date
                , selectedCompletionDateConfidences[1].Date
                , selectedCompletionDateConfidences[2].Date);
        }


        /// <summary>
        /// Gets the completion date confidences.
        /// </summary>
        /// <value>The completion date confidences.</value>
        [SuppressMessage("Microsoft.Design"
            , "CA1024:UsePropertiesWhereAppropriate")]
        public IList<CompletionDateConfidence>
            GetCompletionDateConfidences()
        {
            return this.PersonEstimate
                .GetCompletionDateConfidences();
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
        /// <param name="numberOfOpenTasksForThisIssue">
        /// The number of open tasks for this issue.</param>
        /// <param name="elapsed">The number of elapsed hours.</param>
        /// <param name="evaluation">The evaluation.</param>
        public void AddIssueEvaluation
            (
            IssueDetails issue
            , int numberOfOpenTasksForThisIssue
            , double? elapsed
            , double evaluation
            )
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
                        , this._standardNumberOfHoursPerDay
                        ));
            }
        }


        /// <summary>
        /// Gets the issue evaluations.
        /// </summary>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design"
            , "CA1024:UsePropertiesWhereAppropriate")]
        public IList<IssueEvaluation> GetIssueEvaluations()
        {
            return _issueEvaluations.Select(i => i.Value).ToList();
        }


        /// <summary>
        /// Gets the name of the person.
        /// </summary>
        /// <value>The name of the person.</value>
        public string PersonName
        {
            get
            {
                return this.Person.PersonName;
            }
        }


        #endregion



        #region Private fields and properties


        /// <summary>
        /// Standard number of hours per day.
        /// </summary>
        private double _standardNumberOfHoursPerDay;


        private IList<double> _evaluations = new List<double>();


        private IDictionary<Issue, IssueEvaluation> _issueEvaluations
            = new Dictionary<Issue, IssueEvaluation>();


        /// <summary>
        /// Gets or sets the person, if any,
        /// specific to the project evaluation.
        /// </summary>
        /// <value>The person.</value>
        private PersonView Person { get; set; }


        private PersonEstimate PersonEstimate
        {
            get
            {
                return new PersonEstimate
                    (this.Person, this._evaluations);
            }
        }


        #endregion

    }
}