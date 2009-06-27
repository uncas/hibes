﻿using System;
using System.Collections.Generic;
using System.Linq;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.Domain.Simulation
{
    /// <summary>
    /// Represents a collection of evaluations.
    /// </summary>
    public class Evaluations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Evaluations"/> class.
        /// </summary>
        /// <param name="historicalTasks">The historical tasks.</param>
        public Evaluations(IList<Task> historicalTasks)
        {
            this.HistoricalTasks = historicalTasks
                .OrderByDescending(t => t.EndDate)
                .ToList();
        }

        private IList<Task> HistoricalTasks { get; set; }

        private Random _rnd = new Random();

        /// <summary>
        /// Gets the project evaluation.
        /// </summary>
        /// <param name="issueViews">The issue views.</param>
        /// <param name="numberOfSimulations">The number of simulations.</param>
        /// <returns></returns>
        public ProjectEvaluation GetProjectEvaluation
            (IList<IssueView> issueViews
            , int numberOfSimulations)
        {
            ProjectEvaluation result = new ProjectEvaluation();
            // For a list of issues:
            // Do a simulation N times:
            for (int simulationNumber = 1
                ; simulationNumber <= numberOfSimulations
                ; simulationNumber++)
            {
                RunSimulation(issueViews, result);
            }
            return result;
        }

        /// <summary>
        /// Runs the simulation.
        /// </summary>
        /// <param name="issueViews">The issue views.</param>
        /// <param name="evaluation">The evaluation.</param>
        public void RunSimulation(IList<IssueView> issueViews
            , ProjectEvaluation evaluation)
        {
            double statisticalRemainingForProject = 0d;
            // For each issue:
            foreach (IssueView issueView in issueViews)
            {
                double issueDuration
                    = GetIssueSimulation(issueView);
                evaluation.AddIssueEvaluation(issueView.Issue
                    , issueView.Tasks.Count
                    , issueDuration
                    );
                statisticalRemainingForProject += issueDuration;
            }
            evaluation.AddEvaluation(statisticalRemainingForProject);
        }

        /// <summary>
        /// Gets the issue simulation.
        /// </summary>
        /// <param name="issueView">The issue view.</param>
        /// <returns></returns>
        public double GetIssueSimulation(IssueView issueView)
        {
            double statisticalRemainingForIssue = 0d;
            // For each of the current tasks:
            foreach (Task task in issueView.Tasks)
            {
                // Get the statistical remaining time:
                // Add this statistical remaining time to the sum:
                statisticalRemainingForIssue += GetTaskSimulation(task);
            }
            return statisticalRemainingForIssue;
        }

        /// <summary>
        /// Gets the task simulation.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns></returns>
        public double GetTaskSimulation(Task task)
        {
            // TODO: Look among similar historical tasks:
            //       Improve this by getting a random historical task 
            //       among the tasks that are most similar to the current task.
            //       For example by comparing estimated remaining time.

            // Gets a random historical task:
            double speed = 1d;
            if (this.HistoricalTasks.Count > 0)
            {
                int randomIndex = _rnd.Next(this.HistoricalTasks.Count());
                var randomHistoricalTask = this.HistoricalTasks[randomIndex];

                // Gets the speed of the random historical task:
                // HACK: What to do when historical closed task has null speed?
                speed = randomHistoricalTask.Speed ?? 1d;
            }

            // Calculates a statistical remaining time by:

            // Gets the current tasks's estimated remaining time:
            var remaining = task.Remaining;

            //      Divides by the randomly picked historical speed
            double statisticalRemainingForTask = remaining / speed;

            return statisticalRemainingForTask;
        }
    }
}