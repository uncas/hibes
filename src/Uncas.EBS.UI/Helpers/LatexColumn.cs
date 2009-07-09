using System;

namespace Uncas.EBS.UI.Helpers
{
    public class LatexColumn<T>
    {
        public LatexColumn
            (string title
            , Func<T, string> transform)
            : this(title, transform, ColumnAlignment.Left)
        {
        }

        public LatexColumn
            (string title
            , Func<T, string> transform
            , ColumnAlignment alignment)
        {
            this.Title = title;
            this.Transform = transform;
            this.Alignment = alignment;
        }

        public string Title { get; set; }

        public Func<T, string> Transform { get; set; }

        public ColumnAlignment Alignment { get; set; }
    }

    public enum ColumnAlignment
    {
        Left = 1,
        Center = 2,
        Right = 3
    }
}