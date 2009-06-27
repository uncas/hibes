using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.Domain.ViewModel
{
    public class IssueEvaluation
    {
        public IssueEvaluation(Issue issue
            , int numberOfOpenTasks
            , double evaluation)
        {
            this.Issue = issue;
            this.AddEvaluation(evaluation);
            this.NumberOfOpenTasks = numberOfOpenTasks;
        }

        public Issue Issue { get; private set; }

        public int NumberOfOpenTasks { get; private set; }

        public int IssueId
        {
            get
            {
                return this.Issue.IssueId.Value;
            }
        }

        public string ProjectName
        {
            get
            {
                return this.Issue.ProjectName;
            }
        }

        public string IssueTitle
        {
            get
            {
                return this.Issue.Title;
            }
        }

        public int Priority
        {
            get
            {
                return this.Issue.Priority;
            }
        }

        public double Average
        {
            get
            {
                return this._sum / this._count;
            }
        }

        public void AddEvaluation(double evaluation)
        {
            this._count++;
            this._sum += evaluation;
        }

        private double _sum = 0d;
        private int _count = 0;
    }
}