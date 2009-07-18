namespace Uncas.EBS.Domain.Model
{
    /// <summary>
    /// Represents a person.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        public Person()
        {
            this.DaysPerWeek = 5;
            this.HoursPerDay = 7.5d;
        }

        //public int PersonId { get; set; }

        //public string PersonName { get; set; }

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
    }
}