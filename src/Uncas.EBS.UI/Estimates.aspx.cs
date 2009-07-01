using System;
using System.Web.UI.WebControls;

namespace Uncas.EBS.UI
{
    public partial class Estimates : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var chartArea = chartProbabilities.ChartAreas[0];
            chartArea.AxisX.Title = Resources.Phrases.Remaining;
            chartArea.AxisY.Title = Resources.Phrases.Probability;

            chartArea = chartCompletionDateConfidences.ChartAreas[0];
            chartArea.AxisX.Title = Resources.Phrases.End;
            chartArea.AxisY.Title = Resources.Phrases.Probability;

            gvIssues.EmptyDataText = Resources.Phrases.NoIssues;

            Unit widthOfCharts = Unit.Pixel(400);
            chartCompletionDateConfidences.Width = widthOfCharts;
            chartProbabilities.Width = widthOfCharts;
        }
    }
}