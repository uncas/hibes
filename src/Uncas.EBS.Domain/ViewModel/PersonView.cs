using System.Collections.Generic;
using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.Domain.ViewModel
{
    /// <summary>
    /// Represents a view of a person including person off information.
    /// </summary>
    public class PersonView : Person
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonView"/> class.
        /// </summary>
        /// <param name="personOffs">The person offs.</param>
        public PersonView(IList<PersonOff> personOffs)
            : base()
        {
            this.PersonOffs = personOffs;
        }

        /// <summary>
        /// Gets or sets the person offs.
        /// </summary>
        /// <value>The person offs.</value>
        public IList<PersonOff> PersonOffs { get; set; }
    }
}