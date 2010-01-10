using System;

namespace Uncas.EBS.UI.Helpers
{
    /// <summary>
    /// Represents a column in a latex document.
    /// </summary>
    /// <typeparam name="T">The type of the data for the latex column.</typeparam>
    public class LatexColumn<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LatexColumn&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="transform">The transform.</param>
        public LatexColumn
            (string title
            , Func<T, string> transform)
            : this(title, transform, ColumnAlignment.Left)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LatexColumn&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="transform">The transform.</param>
        /// <param name="alignment">The alignment.</param>
        public LatexColumn
            (string title
            , Func<T, string> transform
            , ColumnAlignment alignment)
        {
            this.Title = title;
            this.Transform = transform;
            this.Alignment = alignment;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the transform.
        /// </summary>
        /// <value>The transform.</value>
        public Func<T, string> Transform { get; set; }

        /// <summary>
        /// Gets or sets the alignment.
        /// </summary>
        /// <value>The alignment.</value>
        public ColumnAlignment Alignment { get; set; }
    }

    /// <summary>
    /// Represents the alignment of a latex column.
    /// </summary>
    public enum ColumnAlignment
    {
        /// <summary>
        /// Alignment not set.
        /// </summary>
        NotSet = 0,
        
        /// <summary>
        /// Left alignment.
        /// </summary>
        Left = 1,
        
        /// <summary>
        /// Center aligment.
        /// </summary>
        Center = 2,
        
        /// <summary>
        /// Right alignment.
        /// </summary>
        Right = 3
    }
}