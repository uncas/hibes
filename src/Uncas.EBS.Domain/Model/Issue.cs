using System;
using System.ComponentModel.DataAnnotations;

namespace Uncas.EBS.Domain.Model
{
    /// <summary>
    /// Represents an issue.
    /// </summary>
    public class Issue
    {
        public Issue()
        {
            this.CreatedDate = DateTime.Now;
            this.Status = Status.Open;
            this.Priority = 1;
        }

        public int? IssueId { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public string Title { get; set; }

        public int Priority { get; set; }

        private Status _status = Status.Open;
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