namespace Uncas.EBS.Domain.Simulation
{
    public class IntervalProbability
    {
        public int Lower { get; set; }
        public int Upper { get; set; }
        public double Probability { get; set; }

        public override string ToString()
        {
            return string.Format("{0}-{1}: {2:P1}"
                , this.Lower, this.Upper, this.Probability);
        }
    }
}