using System;
using System.Collections.Generic;
using System.Linq;

namespace Uncas.EBS.Domain.Model
{
    /// <summary>
    /// Represents info about whether a person is off.
    /// </summary>
    public class PersonOff
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonOff"/> class.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public PersonOff(DateTime from, DateTime to)
        {
            if (from.Date > to.Date)
            {
                throw new ArgumentException();
            }
            this.FromDate = from;
            this.ToDate = to;
        }

        /// <summary>
        /// Reconstructs the person off.
        /// </summary>
        /// <param name="personOffId">The person off id.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="refPersonId">The ref person id.</param>
        /// <returns></returns>
        public static PersonOff ReconstructPersonOff
            (int personOffId
            , DateTime from
            , DateTime to
            , int refPersonId
            )
        {
            var personOff = new PersonOff(from, to);
            personOff.PersonOffId = personOffId;
            personOff.RefPersonId = refPersonId;
            return personOff;
        }

        /// <summary>
        /// Gets or sets the person off id.
        /// </summary>
        /// <value>The person off id.</value>
        public int? PersonOffId { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>From date.</value>
        public DateTime FromDate { get; private set; }

        /// <summary>
        /// Gets or sets to date.
        /// </summary>
        /// <value>To date.</value>
        public DateTime ToDate { get; private set; }

        /// <summary>
        /// Determines whether the person is off on the specified date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// 	<c>true</c> if person is off on the specified date; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPersonOff(DateTime date)
        {
            return this.FromDate.Date <= date.Date
                && date.Date <= this.ToDate.Date;
        }

        /// <summary>
        /// Gets or sets the person id.
        /// </summary>
        /// <value>The person id.</value>
        public int RefPersonId { get; set; }
    }

    /// <summary>
    /// Extension methods for the person off class.
    /// </summary>
    public static class PersonOffExtensions
    {
        /// <summary>
        /// Determines whether [is person off] [the specified person offs].
        /// </summary>
        /// <param name="personOffs">The person offs.</param>
        /// <param name="date">The date.</param>
        /// <returns>
        /// 	<c>true</c> if [is person off] [the specified person offs]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsPersonOff(this IList<PersonOff> personOffs
            , DateTime date)
        {
            return personOffs != null
                && personOffs.Any(po => po.IsPersonOff(date));
        }
    }
}