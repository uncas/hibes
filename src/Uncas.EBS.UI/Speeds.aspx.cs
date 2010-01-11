using System;
using Uncas.EBS.UI.Helpers;

namespace Uncas.EBS.UI
{
    /// <summary>
    /// Code behind for speeds page.
    /// </summary>
    public partial class Speeds : BasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            var chartArea = chartClosedTasks.ChartAreas[0];
            chartArea.AxisY.Title = Resources.Phrases.Original;
            chartArea.AxisX.Title = Resources.Phrases.Elapsed;

            StyleHelpers.SetChartStyles(chartClosedTasks);
        }
    }
}