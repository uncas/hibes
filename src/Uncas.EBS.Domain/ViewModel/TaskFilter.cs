using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.Domain.ViewModel
{
    /// <summary>
    /// Represents the filter for retrieving tasks.
    /// </summary>
    public class TaskFilter
    {
        private static TaskFilter _none = new TaskFilter();

        /// <summary>
        /// Gets a filter that does not filter...
        /// </summary>
        public static TaskFilter None
        {
            get
            {
                return _none;
            }
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public Status Status { get; set; }

        /// <summary>
        /// Gets or sets the id of the person.
        /// </summary>
        public int? PersonId { get; set; }

        /// <summary>
        /// Gets or sets the maximum number to retrieve.
        /// </summary>
        public int? MaxCount { get; set; }
    }
}