using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
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
            gvIssues.RowDataBound
                += new GridViewRowEventHandler(gvIssues_RowDataBound);
        }


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
                = Uncas.EBS.UI.App.ConfidenceLow.ToString("P0");
            gvEvaluationsPerPerson.Columns[2].HeaderText
                = Uncas.EBS.UI.App.ConfidenceMedium.ToString("P0");
            gvEvaluationsPerPerson.Columns[3].HeaderText
                = Uncas.EBS.UI.App.ConfidenceHigh.ToString("P0");
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


        private void ShowCompletionDateConfidences()
        {
            var projectController = new Controllers.ProjectController();
            var evaluationsPerPerson
                = projectController
                .GetEvaluationsPerPerson
                (
                this.SelectedProjectId
                , this.SelectedMaxPriority
                )
                ;

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
                (
                this.SelectedProjectId
                , this.SelectedMaxPriority
                )
                .OrderBy(epp => epp.CompletionDateHigh)
                ;

            int personNumber = 1;
            foreach (var datePerPerson in completionDatesPerPerson)
            {
                if (datePerPerson.CompletionDateHigh
                    == datePerPerson.CompletionDateLow)
                {
                    continue;
                }

                chartDateRanges.Series["Tasks"].Points.AddXY
                    (
                    GetShortenedPersonName(datePerPerson.PersonName)
                    , datePerPerson.CompletionDateLow
                    , datePerPerson.CompletionDateHigh
                    );
                personNumber++;
            }
        }

        private string GetShortenedPersonName(string name)
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