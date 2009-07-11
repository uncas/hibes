﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Uncas.EBS.Domain.ViewModel;
using Uncas.EBS.UI.Controllers;

namespace Uncas.EBS.UI.Helpers
{
    public class LatexHelpers
    {
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

            AppendCompletionDateTable(projectId, maxPriority, sb);


            AppendSection(Resources.Phrases.Issues, sb);

            AppendIssueEstimateTable(projectId, maxPriority, sb);


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

            var completionDateConfidences
                = _projectController.GetSelectedCompletionDateConfidences
                (projectId, maxPriority);

            sb.AppendLine(GetLatexTable<CompletionDateConfidence>
                (completionDateConfidences
                , dateColumn
                , probabilityColumn));
        }

        private void AppendIssueEstimateTable
            (int? projectId
            , int? maxPriority
            , StringBuilder sb)
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
                (Resources.Phrases.Average
                , (IssueEvaluation ie)
                    => ie.Average.HasValue
                    ? ie.Average.Value.ToString("N1")
                    : string.Empty
                , ColumnAlignment.Right);

            var progressColumn
                = new LatexColumn<IssueEvaluation>
                (Resources.Phrases.Progress
                , (IssueEvaluation ie)
                    => ie.Progress.HasValue
                    ? LatexPercentageStringFromDouble(ie.Progress.Value)
                    : string.Empty
                , ColumnAlignment.Right);

            var issueEstimates
                = _projectController.GetIssueEstimates
                (projectId, maxPriority);

            sb.AppendLine(GetLatexTable<IssueEvaluation>
                (issueEstimates
                , priorityColumn
                , projectColumn
                , issueTitleColumn
                , averageDaysColumn
                , progressColumn
                ));
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

            foreach (KeyValuePair<string, string> transform in transforms)
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

            return result;
        }

        private void AppendSection(string sectionTitle, StringBuilder sb)
        {
            sb.AppendLine(@"\section*{" + LatexEncodeText(sectionTitle) + "}");
        }

        public string GetLatexTable<T>
            (IEnumerable<T> data
            , params LatexColumn<T>[] columns)
        {
            StringBuilder sb = new StringBuilder();

            // Begins tabular:
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

            // Adds a row per item:
            foreach (var item in data)
            {
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
            sb.AppendLine(
@"    \hline
\end{tabular}");
            return sb.ToString();
        }
    }
}