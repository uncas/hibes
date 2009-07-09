﻿using System.Collections.Generic;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.ViewModel;

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
        IList<Project> GetProjects();

        /// <summary>
        /// Gets the project evaluation.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="maxPriority">The max priority.</param>
        /// <param name="numberOfSimulations">The number of simulations.</param>
        /// <param name="maxNumberOfHistoricalData">The max number of historical data.</param>
        /// <returns></returns>
        ProjectEvaluation GetProjectEvaluation
            (int? projectId
            , int? maxPriority
            , int numberOfSimulations
            , int maxNumberOfHistoricalData);

        /// <summary>
        /// Inserts the project.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        void InsertProject
            (string projectName);

        /// <summary>
        /// Deletes the project.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        void DeleteProject
            (int projectId);

        /// <summary>
        /// Updates the project.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="projectId">The project id.</param>
        void UpdateProject
            (string projectName, int projectId);

    }
}