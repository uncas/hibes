using System;
using System.Collections.Generic;
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
        /// <param name="paging">The paging.</param>
        /// <returns>
        /// A list of projects.
        /// </returns>
        IList<Project> GetProjects(Paging paging);

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