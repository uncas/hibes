using System;
using System.Linq;
using System.Web.UI.WebControls;
using Uncas.EBS.UI.Helpers;

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

            StyleHelpers.SetChartStyles(chartCompletionDateConfidences);
            StyleHelpers.SetChartStyles(chartDateRanges);
            StyleHelpers.SetChartStyles(chartProbabilities);
        }

        private void ShowDateRanges()
        {
            // UNDONE: ShowDateRanges not implemented;

            // HACK: Needs to input correct parameters:
            var projRepo = new Controllers.ProjectController();
            var result = projRepo
                .GetSelectedCompletionDateConfidences(null, null);
            chartDateRanges.Series["Tasks"].Points.AddXY
                (1
                , result.FirstOrDefault().Date
                , result.Skip(1).FirstOrDefault().Date
                , result.Skip(2).FirstOrDefault().Date);

            throw new NotImplementedException();
        }
    }
}