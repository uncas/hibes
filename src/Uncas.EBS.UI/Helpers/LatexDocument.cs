using System;
using System.Collections.Generic;
using System.Text;

namespace Uncas.EBS.UI.Helpers
{
    /// <summary>
    /// Represents a latex document.
    /// </summary>
    public class LatexDocument
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LatexDocument"/> class.
        /// </summary>
        public LatexDocument()
        {
            content = new StringBuilder();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Encodes the text.
        /// </summary>
        /// <param name="text">The text to be encoded.</param>
        /// <returns>The encoded text.</returns>
        public static string EncodeText(string text)
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
                TransformString(ref result, transform);
            }

            return result;
        }

        /// <summary>
        /// Appends the section.
        /// </summary>
        /// <param name="sectionTitle">The section title.</param>
        public void AppendSection
            (string sectionTitle)
        {
            content.AppendLine(@"\section*{" + EncodeText(sectionTitle) + "}");
        }

        /// <summary>
        /// Appends the table.
        /// </summary>
        /// <typeparam name="T">The type of the data for the latex table.</typeparam>
        /// <param name="data">The data for the table.</param>
        /// <param name="columns">The columns.</param>
        public void AppendTable<T>
               (IEnumerable<T> data
               , params LatexColumn<T>[] columns)
        {
            AppendTable<T>
                (data
                , null
                , columns);
        }

        /// <summary>
        /// Appends the table.
        /// </summary>
        /// <typeparam name="T">The type of the data for the latex table.</typeparam>
        /// <param name="data">The data for the table.</param>
        /// <param name="showRow">The show row.</param>
        /// <param name="columns">The columns.</param>
        public void AppendTable<T>
            (IEnumerable<T> data
            , Func<T, bool> showRow
            , params LatexColumn<T>[] columns)
        {
            LatexTable.AppendTable<T>
                (data
                , showRow
                , content
                , columns);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            AppendBegin(sb);
            sb.Append(content.ToString());
            AppendEnd(sb);
            return sb.ToString();
        }

        #endregion

        #region Private fields and properties


        private StringBuilder content;


        #endregion

        #region Private methods

        private static void AppendBegin
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

");
        }

        private static void AppendEnd
            (StringBuilder sb)
        {
            sb.Append(@"\end{document}");
        }

        private static void TransformString
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

        #endregion

        internal void AppendText(string text)
        {
            content.AppendLine();
            content.AppendLine(text);
            content.AppendLine();
        }
    }
}