using System.Text;
using Uncas.EBS.UI.Controllers;

namespace Uncas.EBS.UI.Helpers
{
    public class LatexHelpers
    {
        public void DownloadLatexFromEvaluation
            (int? projectId
            , int? maxPriority)
        {

        }

        public string LatexFromCompletionDates
            (int? projectId
            , int? maxPriority)
        {
            ProjectController projectController = new ProjectController();
            var completionDateConfidences
                = projectController.GetSelectedCompletionDateConfidences
                (projectId, maxPriority);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(
@"\documentclass[a4paper,11pt,onecolumn]{article}

\usepackage[danish]{babel}
\usepackage{verbatim}

\addtolength\textheight{3cm}
\addtolength\topmargin{-1.5cm}

\addtolength\textwidth{2cm}
\addtolength\oddsidemargin{-1cm}
\addtolength\evensidemargin{-1cm}

\begin{document}

\title{...}
\author{...}
\maketitle

\begin{tabular}{l | l}
    Dato
    &
    Sandsynlighed
\\
\hline");
            foreach (var completionDate in completionDateConfidences)
            {
                sb.AppendFormat(
@"    {0:d}
    &
    {1:P0}
\\"

// UNDONE: Escaping percentage sign

                    , completionDate.Date
                    , completionDate.Probability);
            }
            sb.AppendLine(
@"\hline
\end{tabular}

\end{document}");
            return sb.ToString();
        }
    }
}