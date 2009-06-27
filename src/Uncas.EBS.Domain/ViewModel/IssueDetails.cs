using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.Domain.ViewModel
{
    public class IssueDetails : Issue
    {
        public int NumberOfTasks { get; set; }

        public double? Remaining { get; set; }

        public double? Elapsed { get; set; }

        public double? Total
        {
            get
            {
                if (this.Elapsed.HasValue)
                {
                    double total = this.Elapsed.Value;
                    if (this.Remaining.HasValue)
                    {
                        total += this.Remaining.Value;
                    }
                    return total;
                }
                else
                {
                    return null;
                }
            }
        }

        public double? FractionElapsed
        {
            get
            {
                if (this.Elapsed.HasValue)
                {
                    return this.Elapsed.Value
                        / this.Total.Value;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}