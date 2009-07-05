using System;
using Uncas.EBS.UI.Helpers;

namespace Uncas.EBS.UI
{
    public partial class Speeds : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var chartArea = chartClosedTasks.ChartAreas[0];
            chartArea.AxisY.Title = Resources.Phrases.Original;
            chartArea.AxisX.Title = Resources.Phrases.Elapsed;

            StyleHelpers.SetChartStyles(chartClosedTasks);
        }
    }
}