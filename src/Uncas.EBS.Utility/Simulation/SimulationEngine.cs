using System;
using System.Collections.Generic;
using System.Linq;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.Utility.Simulation
{
    /// <summary>
    /// Simulation engine that runs the simulations.
    /// </summary>
    public class SimulationEngine
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SimulationEngine"/> class.
        /// </summary>
        /// <param name="historicalTasks">The historical tasks.</param>
        public SimulationEngine(IList<Task> historicalTasks)
        {
            this.HistoricalTasks = historicalTasks
                .OrderByDescending(t => t.EndDate)
                .ToList();
        }

        #endregion



        #region Public method


        /// <summary>
        /// Gets the project evaluation.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <param name="issueViews">The issue views.</param>
        /// <param name="numberOfSimulations">
        /// The number of simulations.</param>
        /// <param name="standardNumberOfHoursPerDay">
        /// The standard number of hours per day.</param>
        /// <returns></returns>
        public ProjectEvaluation GetProjectEvaluation
            (PersonView person
            , IList<IssueView> issueViews
            , int numberOfSimulations
            , double standardNumberOfHoursPerDay)
        {
            ProjectEvaluation result
                = new ProjectEvaluation
                    (person
                    , standardNumberOfHoursPerDay);

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


        #endregion



        #region Private methods


        /// <summary>
        /// Runs the simulation.
        /// </summary>
        /// <param name="issueViews">The issue views.</param>
        /// <param name="evaluation">The evaluation.</param>
        private void RunSimulation(IList<IssueView> issueViews
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
                    , issueView.Issue.Elapsed
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
        private double GetIssueSimulation(IssueView issueView)
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
        private double GetTaskSimulation(Task task)
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
            // Historical tasks for the person in question:
            var historicalTasksForThisPerson = this.HistoricalTasks
                .Where(t => t.RefPersonId == task.RefPersonId)
                .ToList();
            // A random index to apply:
            int maxIndex
                = Math.Max(historicalTasksForThisPerson.Count, minMaxIndex)
                + minRandomCount;
            int randomIndex = _rnd.Next(maxIndex + 1);
            if (randomIndex < historicalTasksForThisPerson.Count)
            {
                var randomHistoricalTask
                    = historicalTasksForThisPerson[randomIndex];

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
            const double averageSpeed = 1d;
            const double deltaSpeed = 0.8d;

            return GetRandomSpeedConcentratedAroundAverage
                (averageSpeed
                , deltaSpeed);

            // Speed of execution 
            // (5000 runs with 1000 simulations each; 
            // i.e. 5M simulations in total):

            // EvenlyDistributed:
            // 7.19, 6.91, 7.00, 7.16, 7.42

            // Concentrated:
            // 7.35, 7.66, 7.84, 7.56
        }


        private double GetRandomSpeedConcentratedAroundAverage
            (double averageSpeed
            , double deltaSpeed)
        {
            // A random number between -1 and +1:
            double randomBase = 2d * _rnd.NextDouble() - 1d;

            // A power that should be an odd number (1,3,5,etc.)
            const double power = 3d;

            // Random speed between 
            //      averageSpeed - deltaSpeed 
            // and 
            //      averageSpeed + deltaSpeed:
            double speed = averageSpeed
                + deltaSpeed * Math.Pow(randomBase, power);
            return speed;
        }


        #endregion



        #region Private fields and properties


        private IList<Task> HistoricalTasks { get; set; }


        private Random _rnd = new Random();


        #endregion

    }
}