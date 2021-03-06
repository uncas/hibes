﻿using System;

namespace Uncas.EBS.Domain.ViewModel
{
    /// <summary>
    /// Represents an evaluation of an issue.
    /// </summary>
    public class IssueEvaluation
    {
        #region Constructors


        /// <summary>
        /// Initializes a new instance of the <see cref="IssueEvaluation"/> class.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <param name="numberOfOpenTasks">The number of open tasks.</param>
        /// <param name="elapsed">The number of elapsed hours.</param>
        /// <param name="evaluation">The evaluation.</param>
        /// <param name="standardNumberOfHoursPerDay">The standard number of hours per day.</param>
        public IssueEvaluation
            (IssueDetails issue
            , int numberOfOpenTasks
            , double? elapsed
            , double evaluation
            , double standardNumberOfHoursPerDay)
        {
            if (issue == null
                || numberOfOpenTasks < 0
                || elapsed < 0d
                || evaluation < 0d)
            {
                throw new ArgumentException
                    ("Invalid issue evaluation");
            }

            this.Issue = issue;
            this.NumberOfOpenTasks = numberOfOpenTasks;
            this.Elapsed = elapsed;
            this.AddEvaluation(evaluation);
            this.standardNumberOfHoursPerDay
                = standardNumberOfHoursPerDay;
        }


        #endregion Constructors



        #region Public properties


        /// <summary>
        /// Gets the issue.
        /// </summary>
        /// <value>The issue.</value>
        public IssueDetails Issue { get; private set; }

        /// <summary>
        /// Gets the number of open tasks.
        /// </summary>
        /// <value>The number of open tasks.</value>
        public int NumberOfOpenTasks { get; private set; }

        /// <summary>
        /// Gets the issue id.
        /// </summary>
        /// <value>The issue id.</value>
        public int IssueId
        {
            get
            {
                return this.Issue.IssueId.Value;
            }
        }

        /// <summary>
        /// Gets the name of the project.
        /// </summary>
        /// <value>The name of the project.</value>
        public string ProjectName
        {
            get
            {
                return this.Issue.ProjectName;
            }
        }

        /// <summary>
        /// Gets the issue title.
        /// </summary>
        /// <value>The issue title.</value>
        public string IssueTitle
        {
            get
            {
                return this.Issue.Title;
            }
        }

        /// <summary>
        /// Gets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public int Priority
        {
            get
            {
                return this.Issue.Priority;
            }
        }

        /// <summary>
        /// Gets the average days remaining.
        /// </summary>
        /// <value>The average days remaining.</value>
        public double? Average
        {
            get
            {
                if (this.NumberOfOpenTasks > 0)
                {
                    return this.sum / this.count
                        / this.standardNumberOfHoursPerDay;
                }
                else
                {
                    // If there are no tasks, the average is not well-defined:
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the elapsed hours.
        /// </summary>
        /// <value>The elapsed hours.</value>
        public double? Elapsed { get; private set; }

        /// <summary>
        /// Gets the elapsed days.
        /// </summary>
        /// <value>The elapsed days.</value>
        public double? ElapsedDays
        {
            get
            {
                return this.Elapsed
                    / this.standardNumberOfHoursPerDay;
            }
        }

        /// <summary>
        /// Gets the evaluated total days for the issue.
        /// </summary>
        /// <value>The evaluated total days for the issue.</value>
        public double? TotalDays
        {
            get
            {
                return this.Average + this.ElapsedDays;
            }
        }

        /// <summary>
        /// Gets the progress (elapsed divided by estimated total).
        /// </summary>
        /// <value>The progress.</value>
        public double? Progress
        {
            get
            {
                if (this.Average.HasValue)
                {
                    return this.Elapsed
                        / (this.Elapsed
                        + (this.Average
                        * this.standardNumberOfHoursPerDay));
                }
                else
                {
                    return null;
                }
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
            this.count++;
            this.sum += evaluation;
        }


        #endregion



        #region Private fields and properties


        private double standardNumberOfHoursPerDay;

        private double sum;

        private int count;


        #endregion
    }
}