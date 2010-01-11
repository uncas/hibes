using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.Domain.ViewModel
{
    /// <summary>
    /// Represents the filter for retrieving tasks.
    /// </summary>
    public class TaskFilter
    {
        private static TaskFilter none = new TaskFilter();

        /// <summary>
        /// Gets a filter that does not filter...
        /// </summary>
        /// <value>A filter that does not filter.</value>
        public static TaskFilter None
        {
            get
            {
                return none;
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
        /// <value>The person id.</value>
        public int? PersonId { get; set; }

        /// <summary>
        /// Gets or sets the maximum number to retrieve.
        /// </summary>
        /// <value>The max count.</value>
        public int? MaxCount { get; set; }
    }
}