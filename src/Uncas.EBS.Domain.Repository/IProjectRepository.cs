using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.Domain.Repository
{
    /// <summary>
    /// Handles storage of projects.
    /// </summary>
    public interface IProjectRepository
    {
        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design"
            , "CA1024:UsePropertiesWhereAppropriate")]
        IList<Project> GetProjects();

        /// <summary>
        /// Inserts the project.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        void InsertProject
            (string projectName);

        /// <summary>
        /// Updates the project.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="projectId">The project id.</param>
        void UpdateProject
            (string projectName, int projectId);

        /// <summary>
        /// Deletes the project.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        void DeleteProject
            (int projectId);
    }
}