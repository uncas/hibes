using System;
using System.ComponentModel.DataAnnotations;

namespace Uncas.EBS.Domain.Model
{
    /// <summary>
    /// Represents an issue.
    /// </summary>
    public class Issue
    {
        #region Constructors

        // TODO: REFACTOR: Create constructors or factory methods.

        /// <summary>
        /// Initializes a new instance of the <see cref="Issue"/> class.
        /// </summary>
        internal protected Issue()
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
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// Gets or sets the name of the project.
        /// </summary>
        /// <value>The name of the project.</value>
        // TODO: REFACTOR: Move this to ViewModel.IssueDetails.
        public string ProjectName { get; set; }

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

        private Status _status = Status.Open;
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public Status Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (value != Status.Any)
                {
                    _status = value;
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
            return string.Format("{0} - {1} - {2} - {3} - {4} - {5}"
                , this.IssueId
                , this.CreatedDate
                , this.Priority
                , this.ProjectName
                , this.Status
                , this.Title);
        }
    }
}