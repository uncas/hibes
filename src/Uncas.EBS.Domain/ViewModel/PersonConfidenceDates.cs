using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// <param name="id">The id.</param>
        /// <param name="name">The name.</param>
        /// <param name="completionDate5">The completion date5.</param>
        /// <param name="completionDate50">The completion date50.</param>
        /// <param name="completionDate95">The completion date95.</param>
        public PersonConfidenceDates
            (int id
            , string name
            , DateTime completionDate5
            , DateTime completionDate50
            , DateTime completionDate95
            )
            : base(id, name)
        {
            this.CompletionDate5 = completionDate5;
            this.CompletionDate50 = completionDate50;
            this.CompletionDate95 = completionDate95;
        }

        /// <summary>
        /// Gets or sets the completion date for 5 percent confidence.
        /// </summary>
        /// <value>The completion date for 5 percent confidence.</value>
        public DateTime CompletionDate5 { get; set; }

        /// <summary>
        /// Gets or sets the completion date for 50 percent confidence.
        /// </summary>
        /// <value>The completion date for 50 percent confidence.</value>
        public DateTime CompletionDate50 { get; set; }

        /// <summary>
        /// Gets or sets the completion date for 95 percent confidence.
        /// </summary>
        /// <value>The completion date for 95 percent confidence.</value>
        public DateTime CompletionDate95 { get; set; }
    }
}