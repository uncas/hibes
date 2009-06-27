using System.Collections.Generic;
using System.Linq;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.Domain.Simulation
{
    public class ProjectSimulations
    {
        public ProjectSimulations(IList<IssueView> issueViews)
        {
            this.EstimatedRemaining = new List<double>();
            this.IssueSimulations = new List<IssueSimulations>();
            foreach (var issueView in issueViews)
            {
                this.IssueSimulations.Add(new IssueSimulations
                {
                    Issue = issueView.Issue
                });
            }
        }

        public IList<IssueSimulations> IssueSimulations { get; set; }

        public IList<double> EstimatedRemaining { get; set; }

        public void AddProjectEstimate(double estimatedRemaining)
        {
            this.EstimatedRemaining.Add(estimatedRemaining);
        }

        public void AddIssueEstimate(Issue issue
            , double estimatedRemaining)
        {
            this.IssueSimulations
                .Where(i => i.Issue == issue)
                .SingleOrDefault()
                .AddEstimate(estimatedRemaining);
        }
    }

    public class IssueSimulations
    {
        public IssueSimulations()
        {
            this.NumberOfSimulations = 0;
            this.TotalRemaining = 0d;
        }

        public Issue Issue { get; set; }

        public double EstimatedRemaining
        {
            get
            {
                return this.TotalRemaining / this.NumberOfSimulations;
            }
        }

        public double TotalRemaining { get; set; }
        public int NumberOfSimulations { get; set; }

        public void AddEstimate(double estimatedRemaining)
        {
            this.TotalRemaining += estimatedRemaining;
            this.NumberOfSimulations++;
        }
    }
}