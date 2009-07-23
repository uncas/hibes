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
                    = evaluationPerPerson.GetPersonConfidenceDates();
                if (personConfidenceDates.CompletionDate95
                    == personConfidenceDates.CompletionDate5)
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
                .OrderBy(epp => epp.CompletionDate95)
                ;

            int personNumber = 1;
            foreach (var datePerPerson in completionDatesPerPerson)
            {
                if (datePerPerson.CompletionDate95
                    == datePerPerson.CompletionDate5)
                {
                    continue;
                }

                chartDateRanges.Series["Tasks"].Points.AddXY
                    (
                    GetShortenedPersonName(datePerPerson.PersonName)
                    , datePerPerson.CompletionDate5
                    , datePerPerson.CompletionDate95
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