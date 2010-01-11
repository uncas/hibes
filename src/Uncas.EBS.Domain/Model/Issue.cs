using System;
using System.Globalization;

namespace Uncas.EBS.Domain.Model
{
    /// <summary>
    /// Represents an issue.
    /// </summary>
    public class Issue
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Issue"/> class.
        /// </summary>
        protected internal Issue()
        {
            this.CreatedDate = DateTime.Now;
            this.Status = Status.Open;
            this.Priority = 1;
        }

        /// <summary>
        /// Constructs the issue.
        /// </summary>
        /// <param name="refProjectId">The project id.</param>
        /// <param name="title">The title.</param>
        /// <param name="status">The status.</param>
        /// <param name="priority">The priority.</param>
        /// <returns>The issue.</returns>
        public static Issue ConstructIssue
            (int refProjectId
            , string title
            , Status status
            , int priority)
        {
            Issue issue = new Issue
            {
                RefProjectId = refProjectId,
                Title = title,
                Status = status,
                Priority = priority
            };
            return issue;
        }

        /// <summary>
        /// Reconstructs the issue.
        /// </summary>
        /// <param name="issueId">The issue id.</param>
        /// <param name="title">The title.</param>
        /// <param name="status">The status.</param>
        /// <param name="priority">The priority.</param>
        /// <returns>The issue.</returns>
        public static Issue ReconstructIssue
            (int issueId
            , string title
            , Status status
            , int priority)
        {
            Issue issue = new Issue
            {
                IssueId = issueId,
                Title = title,
                Status = status,
                Priority = priority
            };
            return issue;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the issue id.
        /// </summary>
        /// <value>The issue id.</value>
        public int? IssueId { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The created date.</value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the project id.
        /// </summary>
        /// <value>The project id.</value>
        public int RefProjectId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public int Priority { get; set; }

        private Status status = Status.Open;

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public Status Status
        {
            get
            {
                return status;
            }
            
            set
            {
                if (value != Status.Any)
                {
                    status = value;
                }
            }
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format
                (CultureInfo.CurrentCulture
                , "{0} - {1} - {2} - {3} - {4}"
                , this.IssueId
                , this.CreatedDate
                , this.Priority
                , this.Status
                , this.Title);
        }
    }
}