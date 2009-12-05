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
        /// <param name="id">The id of the person.</param>
        /// <param name="name">The name of the person.</param>
        /// <param name="personOffs">The person offs.</param>
        public PersonView
            (int id
            , string name
            , IList<PersonOff> personOffs)
            : base(id, name)
        {
            this.PersonOffs = personOffs;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonView"/> class.
        /// </summary>
        /// <param name="id">The id of the person.</param>
        /// <param name="name">The name of the person.</param>
        /// <param name="daysPerWeek">The days per week.</param>
        /// <param name="hoursPerDay">The hours per day.</param>
        /// <param name="personOffs">The person offs.</param>
        public PersonView
            (int id
            , string name
            , int daysPerWeek
            , double hoursPerDay
            , IList<PersonOff> personOffs)
            : base(id, name, daysPerWeek, hoursPerDay)
        {
            this.PersonOffs = personOffs;
        }

        /// <summary>
        /// Gets the person offs.
        /// </summary>
        /// <value>The person offs.</value>
        public IList<PersonOff> PersonOffs { get; private set; }
    }
}