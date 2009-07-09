using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Uncas.EBS.Domain.ViewModel;
using Uncas.EBS.UI.Helpers;

namespace Uncas.EBS.UI
{
    public partial class Estimates : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            lbDownloadLatex.Click += new EventHandler(lbDownloadLatex_Click);
            lbDownloadWord.Click += new EventHandler(lbDownloadWord_Click);
            lbDownloadExcel.Click += new EventHandler(lbDownloadExcel_Click);
            gvIssues.RowDataBound += new GridViewRowEventHandler(gvIssues_RowDataBound);
        }

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

        void gvIssues_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var issueDetails = (IssueEvaluation)e.Row.DataItem;
                double? fractionElapsed = issueDetails.Progress;
                StyleHelpers.SetIssueRowStyle(e.Row, fractionElapsed);
            }
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

        private int? SelectedProjectId
        {
            get
            {
                return psProjects.ProjectId;
            }
        }

        private int? SelectedMaxPriority
        {
            get
            {
                return nbMaxPriority.Number;
            }
        }

        void lbDownloadLatex_Click(object sender, EventArgs e)
        {
            LatexHelpers latexHelpers = new LatexHelpers();
            latexHelpers.DownloadLatexFromEstimate
                (SelectedProjectId
                , SelectedMaxPriority
                , Response);
        }

        OfficeHelpers _officeHelpers = new OfficeHelpers();

        void lbDownloadWord_Click(object sender, EventArgs e)
        {
            _officeHelpers.DownloadWord(ph1, "estimates", Response);
        }

        void lbDownloadExcel_Click(object sender, EventArgs e)
        {
            _officeHelpers.DownloadExcel(ph1, "estimates", Response);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
    }
}