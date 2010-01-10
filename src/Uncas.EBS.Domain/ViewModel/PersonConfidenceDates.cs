using System;
using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.Domain.ViewModel
{
    /// <summary>
    /// Represents info about confidence dates for a person.
    /// </summary>
    public class PersonConfidenceDates : Person
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonConfidenceDates"/> class.
        /// </summary>
        /// <param name="id">The id of the person.</param>
        /// <param name="name">The name of the person.</param>
        /// <param name="completionDateLow">The completion date with low probability.</param>
        /// <param name="completionDateMedium">The completion date with medium probability.</param>
        /// <param name="completionDateHigh">The completion date with high probability.</param>
        public PersonConfidenceDates
            (int id
            , string name
            , DateTime completionDateLow
            , DateTime completionDateMedium
            , DateTime completionDateHigh)
            : base(id, name)
        {
            this.CompletionDateLow = completionDateLow;
            this.CompletionDateMedium = completionDateMedium;
            this.CompletionDateHigh = completionDateHigh;
        }

        /// <summary>
        /// Gets or sets the completion date with low confidence.
        /// </summary>
        /// <value>The completion date with low confidence.</value>
        public DateTime CompletionDateLow { get; set; }

        /// <summary>
        /// Gets or sets the completion date with medium confidence.
        /// </summary>
        /// <value>The completion date with medium confidence.</value>
        public DateTime CompletionDateMedium { get; set; }

        /// <summary>
        /// Gets or sets the completion date with high confidence.
        /// </summary>
        /// <value>The completion date for with high confidence.</value>
        public DateTime CompletionDateHigh { get; set; }
    }
}