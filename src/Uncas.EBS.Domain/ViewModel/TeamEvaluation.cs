using System.Collections.Generic;

namespace Uncas.EBS.Domain.ViewModel
{
    /// <summary>
    /// Represents an evaluation for an entire team.
    /// </summary>
    public class TeamEvaluation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeamEvaluation"/> class.
        /// </summary>
        public TeamEvaluation()
        {
            this.EvaluationsPerPerson
                = new List<ProjectEvaluation>();
        }

        /// <summary>
        /// Gets or sets the total evaluation.
        /// </summary>
        /// <value>The total evaluation.</value>
        public ProjectEvaluation TotalEvaluation
        { get; set; }

        /// <summary>
        /// Gets the evaluations per person.
        /// </summary>
        /// <value>The evaluations per person.</value>
        public IList<ProjectEvaluation> EvaluationsPerPerson
        { get; private set; }
    }
}