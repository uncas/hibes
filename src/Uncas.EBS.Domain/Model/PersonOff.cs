using System;

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
        /// <param name="refPersonId">The person id.</param>
        /// <param name="from">The from date.</param>
        /// <param name="to">The to date.</param>
        public PersonOff
            (int refPersonId
            , DateTime from
            , DateTime to)
        {
            if (from.Date > to.Date)
            {
                throw new ArgumentException
                    ("From date must be smaller than to date."
                    , "from");
            }

            this.FromDate = from;
            this.ToDate = to;
            this.RefPersonId = refPersonId;
        }

        /// <summary>
        /// Reconstructs the person off.
        /// </summary>
        /// <param name="personOffId">The person off id.</param>
        /// <param name="from">The from date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="refPersonId">The person id.</param>
        /// <returns>The person off.</returns>
        public static PersonOff ReconstructPersonOff
            (int personOffId
            , DateTime from
            , DateTime to
            , int refPersonId)
        {
            var personOff = new PersonOff(refPersonId, from, to);
            personOff.PersonOffId = personOffId;
            return personOff;
        }

        /// <summary>
        /// Gets or sets the person off id.
        /// </summary>
        /// <value>The person off id.</value>
        public int? PersonOffId { get; set; }

        /// <summary>
        /// Gets the from date.
        /// </summary>
        /// <value>From date.</value>
        public DateTime FromDate { get; private set; }

        /// <summary>
        /// Gets the to date.
        /// </summary>
        /// <value>The to date.</value>
        public DateTime ToDate { get; private set; }

        /// <summary>
        /// Determines whether the person is off on the specified date.
        /// </summary>
        /// <param name="date">The date to check.</param>
        /// <returns>
        ///     <c>true</c> if person is off on the specified date; otherwise, <c>false</c>.
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
}