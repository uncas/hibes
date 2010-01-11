using System;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using Uncas.EBS.Domain.ViewModel;
using Uncas.EBS.UI.Helpers;

namespace Uncas.EBS.UI
{
    /// <summary>
    /// Code behind for estimates page.
    /// </summary>
    public partial class Estimates : BasePage
    {
        private OfficeHelpers officeHelpers = new OfficeHelpers();

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

        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            lbDownloadWord.Click += new EventHandler(DownloadWordButton_Click);
            lbDownloadExcel.Click += new EventHandler(DownloadExcelButton_Click);
            gvIssues.RowDataBound
                += new GridViewRowEventHandler(IssuesGridView_RowDataBound);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            var chartArea = chartProbabilities.ChartAreas[0];
            chartArea.AxisX.Title = Resources.Phrases.Remaining;
            chartArea.AxisY.Title = Resources.Phrases.Probability;

            chartArea = chartDateConfidencesPerPerson.ChartAreas[0];
            chartArea.AxisX.Title = Resources.Phrases.End;
            chartArea.AxisY.Title = Resources.Phrases.Probability;

            gvIssues.EmptyDataText = Resources.Phrases.NoIssues;

            StyleHelpers.SetChartStyles(chartDateRanges);
            StyleHelpers.SetChartStyles(chartProbabilities);
            StyleHelpers.SetChartStyles(chartDateConfidencesPerPerson);

            ShowDateRanges();
            ShowCompletionDateConfidences();

            gvEvaluationsPerPerson.Columns[1].HeaderText
                = App.ConfidenceLow
                    .ToString("P0", CultureInfo.CurrentCulture);
            gvEvaluationsPerPerson.Columns[2].HeaderText
                = App.ConfidenceMedium
                    .ToString("P0", CultureInfo.CurrentCulture);
            gvEvaluationsPerPerson.Columns[3].HeaderText
                = App.ConfidenceHigh
                    .ToString("P0", CultureInfo.CurrentCulture);

            hlDownloadLatex.NavigateUrl = string.Format
                (CultureInfo.InvariantCulture
                , "EstimateAsLatex.ashx?ProjectId={0}&MaxPriority={1}"
                , this.SelectedProjectId
                , this.SelectedMaxPriority);
        }

        private static string GetShortenedPersonName(string name)
        {
            if (name.Length <= 10)
            {
                return name;
            }
            else
            {
                return name.Substring(0, 10);
            }
        }

        private void IssuesGridView_RowDataBound
            (object sender
            , GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var issueDetails = (IssueEvaluation)e.Row.DataItem;
                double? fractionElapsed = issueDetails.Progress;
                StyleHelpers.SetIssueRowStyle(e.Row, fractionElapsed);
            }
        }

        private void ShowCompletionDateConfidences()
        {
            var projectController = new Controllers.ProjectController();
            var evaluationsPerPerson
                = projectController
                .GetEvaluationsPerPerson
                (this.SelectedProjectId
                , this.SelectedMaxPriority);

            foreach (var evaluationPerPerson in evaluationsPerPerson)
            {
                // If there is no timespan, the person is *not* displayed:
                var personConfidenceDates
                    = evaluationPerPerson.GetPersonConfidenceDates
                    (App.ConfidenceLevels);
                if (personConfidenceDates.CompletionDateHigh
                    == personConfidenceDates.CompletionDateLow)
                {
                    continue;
                }

                string name
                    = personConfidenceDates.PersonName;
                Series seriesConfidence
                    = new Series(GetShortenedPersonName(name));
                seriesConfidence.ChartType = SeriesChartType.Line;
                seriesConfidence.YValueType = ChartValueType.Double;
                foreach (var dateConf in
                    evaluationPerPerson.GetCompletionDateConfidences())
                {
                    seriesConfidence.Points.AddXY(dateConf.Date
                        , dateConf.Probability);
                }

                chartDateConfidencesPerPerson.Series
                    .Add(seriesConfidence);
            }
        }

        private void ShowDateRanges()
        {
            var projectController = new Controllers.ProjectController();
            var completionDatesPerPerson
                = projectController
                .GetConfidenceDatesPerPerson
                (this.SelectedProjectId
                , this.SelectedMaxPriority)
                .OrderBy(epp => epp.CompletionDateHigh);

            int personNumber = 1;
            foreach (var datePerPerson in completionDatesPerPerson)
            {
                if (datePerPerson.CompletionDateHigh
                    == datePerPerson.CompletionDateLow)
                {
                    continue;
                }

                chartDateRanges.Series["Tasks"].Points.AddXY
                    (GetShortenedPersonName(datePerPerson.PersonName)
                    , datePerPerson.CompletionDateLow
                    , datePerPerson.CompletionDateHigh);
                personNumber++;
            }
        }

        private void DownloadWordButton_Click(object sender, EventArgs e)
        {
            officeHelpers.DownloadWord(ph1, "estimates", Response);
        }

        private void DownloadExcelButton_Click(object sender, EventArgs e)
        {
            officeHelpers.DownloadExcel(ph1, "estimates", Response);
        }

        /// <summary>
        /// Confirms that an <see cref="T:System.Web.UI.HtmlControls.HtmlForm"/> control is rendered for the specified ASP.NET server control at run time.
        /// </summary>
        /// <param name="control">The ASP.NET server control that is required in the <see cref="T:System.Web.UI.HtmlControls.HtmlForm"/> control.</param>
        /// <exception cref="T:System.Web.HttpException">
        /// The specified server control is not contained between the opening and closing tags of the <see cref="T:System.Web.UI.HtmlControls.HtmlForm"/> server control at run time.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        /// The control to verify is null.
        /// </exception>
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
    }
}