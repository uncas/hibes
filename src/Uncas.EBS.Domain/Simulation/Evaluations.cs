using System;
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
            // TODO: FEATURE: Look among similar historical tasks:
            //       Improve this by getting a random historical task 
            //       among the tasks that are most similar to the current task.
            //       For example by comparing estimated remaining time.

            // Gets a random historical task:
            double speed = 1d;

            // We always generate at least minRandomCount random numbers:
            int minRandomCount = 2;
            // If we have less than minMaxIndex historical tasks
            // we generate some additional random numbers in between:
            int minMaxIndex = 10;
            // A random index to apply:
            int maxIndex
                = Math.Max(this.HistoricalTasks.Count, minMaxIndex)
                + minRandomCount;
            int randomIndex = _rnd.Next(maxIndex + 1);
            if (randomIndex < this.HistoricalTasks.Count)
            {
                var randomHistoricalTask = this.HistoricalTasks[randomIndex];

                // Gets the speed of the random historical task:
                speed = randomHistoricalTask.Speed
                    ?? GetRandomSpeed();
            }
            else
            {
                speed = GetRandomSpeed();
            }

            // Calculates a statistical remaining time by:

            // Gets the current tasks's estimated remaining time:
            var remaining = task.Remaining;

            // Divides by the randomly picked historical speed:
            double statisticalRemainingForTask = remaining / speed;

            return statisticalRemainingForTask;
        }

        private double GetRandomSpeed()
        {
            // Sets the speed to a random number:
            double minRandomSpeed = 0.5d;
            double maxRandomSpeed = 2d;
            double speed = minRandomSpeed
                + (maxRandomSpeed - minRandomSpeed) * _rnd.NextDouble();
            return speed;
        }
    }
}