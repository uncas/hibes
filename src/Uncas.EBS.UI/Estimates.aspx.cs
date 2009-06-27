using System;

namespace Uncas.EBS.UI
{
    public partial class Estimates : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var chartArea = chartProbabilities.ChartAreas[0];
            chartArea.AxisY.Title = Resources.Phrases.Probability;
            chartArea.AxisX.Title = Resources.Phrases.Remaining;
        }
    }
}