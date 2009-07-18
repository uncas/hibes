using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.Domain.ViewModel
{
    /// <summary>
    /// Stores details about tasks.
    /// </summary>
    public class TaskDetails : Task
    {
        /// <summary>
        /// Gets or sets the name of the person.
        /// </summary>
        /// <value>The name of the person.</value>
        public string PersonName { get; set; }
    }
}