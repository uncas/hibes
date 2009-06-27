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
                    , (double evaluation) => evaluation);
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