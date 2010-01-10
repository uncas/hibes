namespace Uncas.EBS.Domain.Model
{
    /// <summary>
    /// Represents info about a person.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        /// <param name="id">The id of the person.</param>
        /// <param name="name">The name of the person.</param>
        public Person
            (int id
            , string name)
            : this(id, name, 5, 7.5d)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        /// <param name="name">The name of the person.</param>
        /// <param name="daysPerWeek">The days per week.</param>
        /// <param name="hoursPerDay">The hours per day.</param>
        public Person
            (string name
            , int daysPerWeek
            , double hoursPerDay)
        {
            this.PersonName = name;
            this.DaysPerWeek = daysPerWeek;
            this.HoursPerDay = hoursPerDay;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        /// <param name="id">The id of the person.</param>
        /// <param name="name">The name of the person.</param>
        /// <param name="daysPerWeek">The days per week.</param>
        /// <param name="hoursPerDay">The hours per day.</param>
        public Person
            (int id
            , string name
            , int daysPerWeek
            , double hoursPerDay)
            : this(name, daysPerWeek, hoursPerDay)
        {
            this.PersonId = id;
        }

        /// <summary>
        /// Gets or sets the person id.
        /// </summary>
        /// <value>The person id.</value>
        public int PersonId { get; set; }

        /// <summary>
        /// Gets or sets the name of the person.
        /// </summary>
        /// <value>The name of the person.</value>
        public string PersonName { get; set; }

        /// <summary>
        /// Gets or sets the days per week.
        /// </summary>
        /// <value>The days per week.</value>
        public int DaysPerWeek { get; set; }

        /// <summary>
        /// Gets or sets the hours per day.
        /// </summary>
        /// <value>The hours per day.</value>
        public double HoursPerDay { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <returns>
        /// True if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        /// </exception>
        public override bool Equals(object obj)
        {
            Person other = (Person)obj;
            if (other != null)
            {
                return this.PersonId.Equals(other.PersonId)
                    && this.PersonName.Equals(other.PersonName)
                    && this.DaysPerWeek.Equals(other.DaysPerWeek)
                    && this.HoursPerDay.Equals(other.HoursPerDay);
            }
            else
            {
                return base.Equals(obj);
            }
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return this.PersonId.GetHashCode()
                + this.PersonName.GetHashCode()
                + this.DaysPerWeek.GetHashCode()
                + this.HoursPerDay.GetHashCode();
        }
    }
}