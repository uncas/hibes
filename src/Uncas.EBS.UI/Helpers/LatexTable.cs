using System;
using System.Collections.Generic;
using System.Text;

namespace Uncas.EBS.UI.Helpers
{
    public class LatexTable
    {
        /// <summary>
        /// Appends the table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <param name="columns">The columns.</param>
        public void AppendTable<T>
            (IEnumerable<T> data
            , StringBuilder content
            , params LatexColumn<T>[] columns)
        {
            AppendTable<T>
                (data
                , null
                , content
                , columns);
        }

        /// <summary>
        /// Appends the table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <param name="showRow">The show row.</param>
        /// <param name="columns">The columns.</param>
        public void AppendTable<T>
            (IEnumerable<T> data
            , Func<T, bool> showRow
            , StringBuilder content
            , params LatexColumn<T>[] columns)
        {
            StringBuilder sb = new StringBuilder();

            BeginTabular<T>(columns, sb);

            MakeHeader<T>(columns, sb);

            AddRowPerItem<T>(data
                , columns
                , sb
                , showRow);

            EndTabular(sb);

            content.Append(sb.ToString());
        }

        private static void BeginTabular<T>
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
                sb.Append
                    (GetColumnAlignment<T>
                        (column.Alignment)
                    );
                columnIndex++;
            }
            sb.AppendLine("}");
        }

        private static string GetColumnAlignment<T>
            (ColumnAlignment alignment)
        {
            switch (alignment)
            {
                case ColumnAlignment.Left:
                    return "l";
                case ColumnAlignment.Center:
                    return "c";
                case ColumnAlignment.Right:
                    return "r";
                default:
                    throw new NotImplementedException();
            }
        }

        private static void MakeHeader<T>
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
                    + LatexDocument.EncodeText(column.Title));
                headerFieldIndex++;
            }
            sb.AppendLine(@"    \\
    \hline");
        }

        private static void AddRowPerItem<T>
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
                        + LatexDocument.EncodeText(column.Transform(item)));
                    fieldIndex++;
                }
                sb.AppendLine(@"    \\");
            }
        }


        private static void EndTabular
            (StringBuilder sb)
        {
            sb.AppendLine(
@"    \hline
\end{tabular}");
        }
    }
}