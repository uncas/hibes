using System.Collections.Generic;
using System.Linq;
using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.Domain.ViewModel
{
    public class ProjectEvaluation
    {
        private IList<double> _evaluations = new List<double>();

        private IDictionary<Issue, IssueEvaluation> _issueEvaluations
            = new Dictionary<Issue, IssueEvaluation>();

        public Statistic<double> Statistics
        {
            get
            {
                return new Statistic<double>(_evaluations
                    , (double evaluation) => evaluation);
            }
        }

        public double Average
        {
            get
            {
                return this.Statistics.Average;
            }
        }

        public double StandardDeviation
        {
            get
            {
                return this.Statistics.StandardDeviation;
            }
        }

        public int NumberOfOpenIssues
        {
            get
            {
                return this._issueEvaluations.Count;
            }
        }

        public int NumberOfOpenTasks
        {
            get
            {
                return _issueEvaluations.Sum(i => i.Value.NumberOfOpenTasks);
            }
        }

        public void AddEvaluation(double evaluation)
        {
            this._evaluations.Add(evaluation);
        }

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

        public IList<IssueEvaluation> GetIssueEvaluations()
        {
            return _issueEvaluations.Select(i => i.Value).ToList();
        }
    }
}