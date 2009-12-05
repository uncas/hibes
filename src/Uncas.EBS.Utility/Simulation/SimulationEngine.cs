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
            if (historicalTasks != null)
            {
                // Sorts the tasks with the newest first:
                this._historicalTasks
                    = historicalTasks
                    .OrderByDescending(t => t.EndDate)
                    .ToList();
            }
        }

        #endregion



        #region Public method


        /// <summary>
        /// Gets a project evaluation
        /// with statistical remaining days
        /// based on a number of simulations.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <param name="issues">The issues.</param>
        /// <param name="numberOfSimulations">The number of simulations.</param>
        /// <param name="standardNumberOfHoursPerDay">The standard number of hours per day.</param>
        /// <returns>
        /// The total project evaluation from the simulations.
        /// </returns>
        public ProjectEvaluation GetProjectEvaluation
            (PersonView person
            , IList<IssueView> issues
            , int numberOfSimulations
            , double standardNumberOfHoursPerDay)
        {
            // Prepares the container for the simulation results:
            ProjectEvaluation result
                = new ProjectEvaluation
                    (person
                    , standardNumberOfHoursPerDay);

            // Runs through a number of simulations for the given issues:
            for (int simulationNumber = 1
                ; simulationNumber <= numberOfSimulations
                ; simulationNumber++)
            {
                // In each simulation the container for simulation results
                // is passed along in order to collect the result:
                RunSimulation(issues, result);
            }

            // Returns the container with the simulation results:
            return result;
        }


        #endregion



        #region Private methods


        /// <summary>
        /// Runs one simulation.
        /// </summary>
        /// <param name="issues">The issues.</param>
        /// <param name="evaluation">The evaluation container with simulation results.</param>
        private void RunSimulation
            (IList<IssueView> issues
            , ProjectEvaluation evaluation)
        {
            // In this simulation the remaining hours for the project is calculated:
            double statisticalRemainingForProject = 0d;

            // In one simulation all issues are treated individually:
            foreach (IssueView issue in issues)
            {
                // Gets the simulated remaining hours for the issue:
                double issueDuration
                    = GetSimulatedRemainingHoursForIssue(issue);

                // The simulated remaining hours for the issue 
                // is added to the container of the simulation results:
                // TODO: Obey Law of Demeter...
                evaluation.AddIssueEvaluation
                    (issue.Issue
                    , issue.Tasks.Count
                    , issue.Issue.Elapsed
                    , issueDuration);

                // The simulated remaining hours for the issue
                // is added to the remaining hours for the entire project:
                statisticalRemainingForProject
                    += issueDuration;
            }

            // The total simulated remaining hours for the entire project
            // is added to the container of simulation results:
            evaluation.AddEvaluation
                (statisticalRemainingForProject);
        }


        /// <summary>
        /// Gets the simulated remaining hours for a given issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <returns>
        /// The simulated remaining hours for the issue.
        /// </returns>
        private double GetSimulatedRemainingHoursForIssue
            (IssueView issue)
        {
            // For this issue the simulated remaining hours is calculated:
            double statisticalRemainingForIssue = 0d;

            // For this issue all associated tasks are treated individually:
            foreach (Task task in issue.Tasks)
            {
                // Gets the simulated remaining hours for the task:
                // and adds it to the sum for the issue:
                statisticalRemainingForIssue
                    += GetSimulatedRemainingHoursForTask(task);
            }

            // Returns the total simulated remaining hours for the issue:
            return statisticalRemainingForIssue;
        }


        /// <summary>
        /// Gets the simulated remaining hours for a given task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns></returns>
        private double GetSimulatedRemainingHoursForTask
            (Task task)
        {
            // Gets the speed to use in the simulation:
            double speed = GetSpeedForSimulation(task);

            // Gets the current tasks's estimated remaining time:
            double remaining = task.Remaining;

            // Calculates a statistical remaining time:
            // by dividing by the randomly picked historical speed:
            return remaining / speed;
        }

        /// <summary>
        /// Gets the speed for the simulation of a given task.
        /// </summary>
        /// <param name="task">The given task.</param>
        /// <returns>The speed.</returns>
        private double GetSpeedForSimulation(Task task)
        {
            // Historical tasks for the person that is responsible for the given task:
            var tasksForPerson
                = this._historicalTasks
                .Where(t => t.RefPersonId == task.RefPersonId)
                .ToList();

            // We need a random historical task in order to get a random historical speed.
            // Therefore we first calculate a random index that depends on the number of historical tasks.
            // However, we would also like to take in some completely random speeds
            // that do not depend on historical tasks.
            // So the random index might be larger than the number of historical tasks.
            // Tricksy master...

            // First we gets the indexeses to use:
            int randomIndex
                = GetRandomTaskIndex
                (tasksForPerson.Count);

            // If the index is low enought we take a real historical speed:
            if (randomIndex < tasksForPerson.Count)
            {
                // Gets the speed of the random historical task:
                return tasksForPerson[randomIndex].Speed
                    ?? GetRandomSpeed();
            }
            else
            {
                // Otherwise we take a random speed:
                return GetRandomSpeed();
            }
        }

        /// <summary>
        /// Gets a random index that might be larger than the length of the list...
        /// </summary>
        /// <param name="numberOfTasks">The number of tasks.</param>
        /// <returns>The random index.</returns>
        private int GetRandomTaskIndex(int numberOfTasks)
        {
            // We always generate at least minRandomCount random numbers:
            int minRandomCount = 2;

            // If we have less than minMaxIndex historical tasks
            // we generate some additional random numbers in between:
            int minMaxIndex = 10;

            // So the largest index is minRandomCount
            // plus the larger of the number of tasks or minMaxIndex:
            int maxIndex
                = Math.Max(numberOfTasks, minMaxIndex)
                + minRandomCount;

            // A random index to apply:
            return _random.Next(maxIndex + 1);
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
            double randomBase = 2d * _random.NextDouble() - 1d;

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


        private readonly IList<Task> _historicalTasks;


        private Random _random = new Random();


        #endregion

    }
}