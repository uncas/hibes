using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Uncas.EBS.Domain.ViewModel;
using Uncas.EBS.UI.Controllers;

namespace Uncas.EBS.UI.Helpers
{
    public class LatexHelpers
    {
        #region Constructors

        #endregion

        private ProjectController _projectController
            = new ProjectController();

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

        public string GetLatexFromEstimate
            (int? projectId
            , int? maxPriority)
        {
            StringBuilder sb = new StringBuilder();

            AppendLatexDocumentBegin(sb);


            AppendSection(Resources.Phrases.Date, sb);

            //AppendCompletionDateTable(projectId, maxPriority, sb);
            AppendPersonConfidenceDateTable
                (projectId, maxPriority, sb);


            AppendSection(Resources.Phrases.Issues, sb);

            AppendIssueEstimateTables(projectId, maxPriority, sb);


            AppendLatexDocumentEnd(sb);

            return sb.ToString();
        }

        private void AppendLatexDocumentBegin
            (StringBuilder sb)
        {
            sb.AppendLine(
@"\documentclass[a4paper,10pt,onecolumn]{article}

\usepackage[danish]{babel}
\usepackage{verbatim}
\usepackage{textcomp}

\addtolength\textheight{3cm}
\addtolength\topmargin{-1.5cm}

\addtolength\textwidth{4cm}
\addtolength\oddsidemargin{-2cm}
\addtolength\evensidemargin{-2cm}

\begin{document}

\title{...}
\author{...}
\maketitle
");
        }

        private void AppendLatexDocumentEnd
            (StringBuilder sb)
        {
            sb.Append(@"\end{document}");
        }

        private void AppendCompletionDateTable
            (int? projectId
            , int? maxPriority
            , StringBuilder sb)
        {
            var completionDateConfidences
                = _projectController.GetSelectedCompletionDateConfidences
                (projectId, maxPriority);

            var dateColumn
                = new LatexColumn<CompletionDateConfidence>
                (Resources.Phrases.Date
                , (CompletionDateConfidence to)
                    => to.Date.ToShortDateString());

            var probabilityColumn
                = new LatexColumn<CompletionDateConfidence>
                (Resources.Phrases.Probability
                , (CompletionDateConfidence to)
                    => LatexPercentageStringFromDouble(to.Probability)
                , ColumnAlignment.Right);

            sb.AppendLine(GetLatexTable<CompletionDateConfidence>
                (completionDateConfidences
                , dateColumn
                , probabilityColumn));
        }

        private void AppendPersonConfidenceDateTable
            (int? projectId
            , int? maxPriority
            , StringBuilder sb)
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

            sb.AppendLine(GetLatexTable<PersonConfidenceDates>
                (personConfidenceDates
                , nameColumn
                , lowColumn
                , mediumColumn
                , highColumn));
        }

        private void AppendIssueEstimateTables
            (int? projectId
            , int? maxPriority
            , StringBuilder sb)
        {
            var issueEstimates
                = _projectController.GetIssueEstimates
                (projectId, maxPriority);

            AppendIssueEstimateTable(sb, issueEstimates, true);

            foreach (var personEvaluation
                in _projectController.GetEvaluationsPerPerson
                (projectId, maxPriority))
            {
                // TODO: FEATURE: Person name on latex output.
                AppendIssueEstimateTable
                    (sb
                    , personEvaluation.GetIssueEvaluations()
                    , false
                    );
            }
        }

        private void AppendIssueEstimateTable
            (StringBuilder sb
            , IEnumerable<IssueEvaluation> issueEstimates
            , bool showEmptyRows)
        {
            var priorityColumn
                = new LatexColumn<IssueEvaluation>
                (Resources.Phrases.Priority
                , (IssueEvaluation ie)
                    => ie.Priority.ToString()
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

            var averageDaysColumn
                = new LatexColumn<IssueEvaluation>
                (Resources.Phrases.Days
                , (IssueEvaluation ie)
                    => GetDaysRemainingText(ie.Average)
                , ColumnAlignment.Right);

            Func<IssueEvaluation, bool> showRow = null;
            if (!showEmptyRows)
            {
                showRow = (IssueEvaluation ie)
                    => ie.Average.HasValue;
            }

            sb.AppendLine(GetLatexTable<IssueEvaluation>
                (issueEstimates
                , showRow
                , priorityColumn
                , projectColumn
                , issueTitleColumn
                , averageDaysColumn
                ));
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
        public string GetDaysRemainingText(double? daysRemaining)
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
                    .ToString();
            }
        }

        public string ShortenText(string text, int maxLength)
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

        public string LatexPercentageStringFromDouble
            (double number)
        {
            return number.ToString("P0").Replace("%", @"\%");
        }

        public string LatexEncodeText(string text)
        {
            var transforms = new Dictionary<string, string>();
            transforms.Add("æ", @"\ae");
            transforms.Add("ø", @"\o");
            transforms.Add("å", @"\aa");
            transforms.Add("Æ", @"\AE");
            transforms.Add("Ø", @"\O");
            transforms.Add("Å", @"\AA");

            string result = text;

            foreach (KeyValuePair<string, string> transform
                in transforms)
            {
                LatexTransformString(ref result, transform);
            }

            return result;
        }

        private void LatexTransformString
            (ref string result
            , KeyValuePair<string, string> transform)
        {
            string oldString = transform.Key + " ";
            string newString = transform.Value + @"\ ";
            result = result.Replace
                (oldString
                , newString);

            oldString = transform.Key;
            newString = transform.Value + " ";
            result = result.Replace
                (oldString
                , newString);
        }

        private void AppendSection
            (string sectionTitle
            , StringBuilder sb)
        {
            sb.AppendLine(@"\section*{" + LatexEncodeText(sectionTitle) + "}");
        }

        public string GetLatexTable<T>
               (IEnumerable<T> data
               , params LatexColumn<T>[] columns
               )
        {
            return GetLatexTable<T>
                (data
                , null
                , columns);
        }

        public string GetLatexTable<T>
            (IEnumerable<T> data
            , Func<T, bool> showRow
            , params LatexColumn<T>[] columns
            )
        {
            StringBuilder sb = new StringBuilder();

            BeginTabular<T>(columns, sb);

            MakeHeader<T>(columns, sb);

            AddRowPerItem<T>(data
                , columns
                , sb
                , showRow);

            EndTabular(sb);

            return sb.ToString();
        }

        private void BeginTabular<T>
            (LatexColumn<T>[] columns
            , StringBuilder sb)
        {
            sb.Append(@"\begin{tabular}{");
            int columnIndex = 0;
            foreach (LatexColumn<T> column in columns)
            {
                if (columnIndex > 0)
                {
                    sb.Append(" | ");
                }
                switch (column.Alignment)
                {
                    case ColumnAlignment.Left:
                        sb.Append("l");
                        break;
                    case ColumnAlignment.Center:
                        sb.Append("c");
                        break;
                    case ColumnAlignment.Right:
                        sb.Append("r");
                        break;
                    default:
                        throw new NotImplementedException();
                }
                columnIndex++;
            }
            sb.AppendLine("}");
        }

        private void MakeHeader<T>
            (LatexColumn<T>[] columns
            , StringBuilder sb)
        {
            // Makes header:
            int headerFieldIndex = 0;
            foreach (LatexColumn<T> column in columns)
            {
                if (headerFieldIndex > 0)
                {
                    sb.AppendLine("        &");
                }
                sb.AppendLine("        "
                    + LatexEncodeText(column.Title));
                headerFieldIndex++;
            }
            sb.AppendLine(@"    \\
    \hline");
        }

        [Obsolete]
        private void AddRowPerItem<T>
           (IEnumerable<T> data
           , LatexColumn<T>[] columns
           , StringBuilder sb)
        {
            AddRowPerItem<T>
                (data
                , columns
                , sb
                , null);
        }

        private void AddRowPerItem<T>
            (IEnumerable<T> data
            , LatexColumn<T>[] columns
            , StringBuilder sb
            , Func<T, bool> showRow
            )
        {
            // Adds a row per item:
            foreach (var item in data)
            {
                if (showRow != null
                    && !showRow(item))
                {
                    continue;
                }
                int fieldIndex = 0;
                foreach (LatexColumn<T> column in columns)
                {
                    if (fieldIndex > 0)
                    {
                        sb.AppendLine("        &");
                    }
                    sb.AppendLine("        "
                        + LatexEncodeText(column.Transform(item)));
                    fieldIndex++;
                }
                sb.AppendLine(@"    \\");
            }
        }

        private void EndTabular
            (StringBuilder sb)
        {
            sb.AppendLine(
@"    \hline
\end{tabular}");
        }
    }
}