﻿using System;
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
        /// <param name="standardNumberOfHoursPerDay">The standard number of hours per day.</param>
        public ProjectEvaluation
            (PersonView person
            , double standardNumberOfHoursPerDay)
        {
            this.Person = person;
            this.standardNumberOfHoursPerDay
                = standardNumberOfHoursPerDay;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the statistics for days remaining.
        /// </summary>
        /// <value>The statistics for days remaining.</value>
        public Statistic<double> Statistics
        {
            get
            {
                return new Statistic<double>
                    (evaluations
                    , (double evaluation)
                        => evaluation
                        / standardNumberOfHoursPerDay);
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
                return this.issueEvaluations
                    .Select(i => i.Value)
                    .Sum(i => i.Elapsed)
                    / standardNumberOfHoursPerDay;
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
                return this.issueEvaluations.Count;
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
                return issueEvaluations.Sum
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
                // Takes the estimate for the medium completion date:
                DateTime mediumCompletionDate =
                    GetCompletionDateConfidences()
                    .Where(cdc => cdc.Probability == 0.5d)
                    .FirstOrDefault()
                    .Date;

                // And rounds up the number of days till that medium completion date:
                return (int)Math.Ceiling
                    (mediumCompletionDate
                    .Subtract(DateTime.Now)
                    .TotalDays);
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Adds the evaluation.
        /// </summary>
        /// <param name="evaluation">The evaluation.</param>
        public void AddEvaluation(double evaluation)
        {
            this.evaluations.Add(evaluation);
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
            (IssueDetails issue
            , int numberOfOpenTasksForThisIssue
            , double? elapsed
            , double evaluation)
        {
            if (issueEvaluations.ContainsKey(issue))
            {
                issueEvaluations[issue].AddEvaluation(evaluation);
            }
            else
            {
                var issueEvaluation
                    = new IssueEvaluation
                        (issue
                        , numberOfOpenTasksForThisIssue
                        , elapsed
                        , evaluation
                        , this.standardNumberOfHoursPerDay);
                issueEvaluations.Add
                    (issue
                    , issueEvaluation);
            }
        }

        /// <summary>
        /// Gets the selected completion date confidences.
        /// </summary>
        /// <param name="levels">The levels.</param>
        /// <returns>A list of completion date confidences.</returns>
        public IList<CompletionDateConfidence>
            GetSelectedCompletionDateConfidences
            (ConfidenceLevels levels)
        {
            return PersonEstimate
                .GetSelectedCompletionDateConfidences(levels);
        }

        /// <summary>
        /// Gets the person confidence dates.
        /// </summary>
        /// <param name="levels">The levels.</param>
        /// <returns>The person confidence dates.</returns>
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
        /// <returns>A list of completion date confidences.</returns>
        /// <value>The completion date confidences.</value>
        /// TODO: [Obsolete("Use overload with paging instead")]
        [SuppressMessage("Microsoft.Design"
            , "CA1024:UsePropertiesWhereAppropriate"
            , Justification = "This is a relatively complex calculation...")]
        public IList<CompletionDateConfidence>
            GetCompletionDateConfidences()
        {
            return this.PersonEstimate
                .GetCompletionDateConfidences();
        }

        /// <summary>
        /// Gets the issue evaluations.
        /// </summary>
        /// <returns>A list of issue evaluations.</returns>
        /// TODO: [Obsolete("Use overload with paging instead")]
        [SuppressMessage("Microsoft.Design"
            , "CA1024:UsePropertiesWhereAppropriate"
            , Justification = "This is a relatively complex calculation...")]
        public IList<IssueEvaluation> GetIssueEvaluations()
        {
            return issueEvaluations.Select(i => i.Value).ToList();
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
        private double standardNumberOfHoursPerDay;


        private IList<double> evaluations = new List<double>();


        private IDictionary<Issue, IssueEvaluation> issueEvaluations
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
                    (this.Person, this.evaluations);
            }
        }


        #endregion
    }
}