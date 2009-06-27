using System.ComponentModel.DataAnnotations;

namespace Uncas.EBS.Domain.Model
{
    /// <summary>
    /// Represents a project.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Gets or sets the project id.
        /// </summary>
        /// <value>The project id.</value>
        public int ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        /// <value>The name of the project.</value>
        public string ProjectName { get; set; }
    }
}