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
        /// The status.
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// The id of the person.
        /// </summary>
        public int? PersonId { get; set; }

        /// <summary>
        /// The maximum number to retrieve.
        /// </summary>
        public int? MaxCount { get; set; }
    }
}