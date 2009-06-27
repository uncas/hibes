using System;
using System.ComponentModel.DataAnnotations;

namespace Uncas.EBS.Domain.Model
{
    /// <summary>
    /// Represents an issue.
    /// </summary>
    public class Issue
    {
        // TODO: Consider constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Issue"/> class.
        /// </summary>
        public Issue()
        {
            this.CreatedDate = DateTime.Now;
            this.Status = Status.Open;
            this.Priority = 1;
        }

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
        /// Gets or sets the name of the project.
        /// </summary>
        /// <value>The name of the project.</value>
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