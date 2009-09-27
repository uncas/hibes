using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using Uncas.EBS.Domain.ViewModel;
using Uncas.EBS.UI.Controllers;

namespace Uncas.EBS.UI.Helpers
{
    public class LatexHelpers
    {

        #region Public methods


        public void DownloadLatexFromEstimate
            (int? projectId
            , int? maxPriority
            , HttpResponse response)
        {
            response.Clear();
            response.AddHeader
                ("Content-Disposition"
                , "attachment;filename=estimate.tex");
            response.ContentType = "application/x-latex";
            response.Write(GetLatexFromEstimate
                (projectId, maxPriority));
            response.Flush();
            response.End();
        }


        #endregion



        #region Private fields and properties

        private ProjectController _projectController
            = new ProjectController();

        #endregion



        #region Private methods


        private string GetLatexFromEstimate
            (int? projectId
            , int? maxPriority)
        {
            LatexDocument document = new LatexDocument();

            document.AppendText("@TOP@");

            AppendIssueEstimateTables
                (projectId
                , maxPriority
                , document);

            document.AppendSection(Resources.Phrases.Date);

            AppendPersonConfidenceDateTable
                (projectId
                , maxPriority
                , document);

            document.AppendText("@BOTTOM@");

            return document.ToString();
        }


        private void AppendPersonConfidenceDateTable
            (int? projectId
            , int? maxPriority
            , LatexDocument document)
        {
            var personConfidenceDates
                = _projectController.GetConfidenceDatesPerPerson
                (projectId, maxPriority);

            var nameColumn
                = new LatexColumn<PersonConfidenceDates>
                (Resources.Phrases.Person
                , (PersonConfidenceDates to)
                    => to.PersonName);

            var lowColumn
                = new LatexColumn<PersonConfidenceDates>
                (LatexPercentageStringFromDouble
                    (App.ConfidenceLow)
                , (PersonConfidenceDates to)
                    => to.CompletionDateLow.ToShortDateString()
                , ColumnAlignment.Right);

            var mediumColumn
                = new LatexColumn<PersonConfidenceDates>
                (LatexPercentageStringFromDouble
                    (App.ConfidenceMedium)
                , (PersonConfidenceDates to)
                    => to.CompletionDateMedium.ToShortDateString()
                , ColumnAlignment.Right);

            var highColumn
                = new LatexColumn<PersonConfidenceDates>
                (LatexPercentageStringFromDouble
                    (App.ConfidenceHigh)
                , (PersonConfidenceDates to)
                    => to.CompletionDateHigh.ToShortDateString()
                , ColumnAlignment.Right);

            document.AppendTable<PersonConfidenceDates>
                (personConfidenceDates
                , nameColumn
                , lowColumn
                , mediumColumn
                , highColumn);
        }


        private void AppendIssueEstimateTables
            (int? projectId
            , int? maxPriority
            , LatexDocument document)
        {
            var issueEstimates
                = _projectController.GetIssueEstimates
                (projectId, maxPriority);

            document.AppendSection(Resources.Phrases.Issues);
            AppendIssueEstimateTable
                (issueEstimates
                , true
                , document);

            foreach (var personEvaluation
                in _projectController.GetEvaluationsPerPerson
                (projectId, maxPriority))
            {
                document.AppendSection
                    (personEvaluation.PersonName);
                AppendIssueEstimateTable
                    (personEvaluation.GetIssueEvaluations()
                    , false
                    , document
                    );
            }
        }


        private static void AppendIssueEstimateTable
            (IEnumerable<IssueEvaluation> issueEstimates
            , bool showEmptyRows
            , LatexDocument document)
        {
            var priorityColumn
                = new LatexColumn<IssueEvaluation>
                (Resources.Phrases.Priority
                , (IssueEvaluation ie)
                    => ie.Priority.ToString
                    (CultureInfo.CurrentCulture)
                , ColumnAlignment.Center);

            var projectColumn
                = new LatexColumn<IssueEvaluation>
                (Resources.Phrases.Project
                , (IssueEvaluation ie)
                    => ie.ProjectName);

            int maxCharactersInIssueTitle = 45;

            var issueTitleColumn
                = new LatexColumn<IssueEvaluation>
                (Resources.Phrases.Issue
                , (IssueEvaluation ie)
                    => ShortenText(ie.IssueTitle
                        , maxCharactersInIssueTitle)
                );

            var elapsedDaysColumn
                = new LatexColumn<IssueEvaluation>
                (Resources.Phrases.Elapsed
                , (IssueEvaluation ie)
                    => string.Format("{0:N1}"
                    , ie.ElapsedDays)
                , ColumnAlignment.Right);

            var remainingDaysColumn
                = new LatexColumn<IssueEvaluation>
                (Resources.Phrases.Remaining
                , (IssueEvaluation ie)
                    => GetDaysRemainingText(ie.Average)
                , ColumnAlignment.Right);

            var totalDaysColumn
                = new LatexColumn<IssueEvaluation>
                (Resources.Phrases.Total
                , (IssueEvaluation ie)
                    => GetDaysRemainingText(ie.TotalDays)
                , ColumnAlignment.Right);

            Func<IssueEvaluation, bool> showRow = null;
            if (!showEmptyRows)
            {
                showRow = (IssueEvaluation ie)
                    => ie.Average.HasValue;
            }

            document.AppendTable<IssueEvaluation>
                   (issueEstimates
                   , showRow
                   , priorityColumn
                   , projectColumn
                   , issueTitleColumn
                   , elapsedDaysColumn
                   , remainingDaysColumn
                   , totalDaysColumn
                   );
        }


        /// <summary>
        /// Gets the days remaining text.
        /// </summary>
        /// <param name="daysRemaining">The days remaining.</param>
        /// <example>
        ///     daysRemaining  returnValue
        ///        0.4             ½
        ///        0.6             1
        ///        1.1             2
        ///        3.9             4
        /// </example>
        /// <returns></returns>
        private static string GetDaysRemainingText(double? daysRemaining)
        {
            if (!daysRemaining.HasValue)
            {
                return "?";
            }
            else if (daysRemaining.Value < 0.5d)
            {
                return @"\textonehalf";
            }
            else
            {
                return ((int)Math.Ceiling(daysRemaining.Value))
                    .ToString(CultureInfo.CurrentCulture);
            }
        }


        private static string LatexPercentageStringFromDouble
            (double number)
        {
            return number
                .ToString("P0", CultureInfo.CurrentCulture)
                .Replace("%", @"\%");
        }


        private static string ShortenText(string text, int maxLength)
        {
            if (text.Length <= maxLength)
            {
                return text;
            }
            else
            {
                return text.Substring(0, maxLength) + "...";
            }
        }


        #endregion

    }
}