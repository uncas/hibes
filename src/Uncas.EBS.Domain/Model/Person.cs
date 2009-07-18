using System;
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
        public Person(int id
            , string name)
            : this(id, name, 5, 7.5d)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        public Person(int id
            , string name
            , int daysPerWeek
            , double hoursPerDay)
        {
            this.PersonId = id;
            this.PersonName = name;
            this.DaysPerWeek = daysPerWeek;
            this.HoursPerDay = hoursPerDay;
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
    }

    /// <summary>
    /// Extensions for the person class.
    /// </summary>
    public static class PersonExtensions
    {
        /// <summary>
        /// Determines whether [is at work] [the specified person].
        /// </summary>
        /// <param name="person">The person.</param>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns>
        /// 	<c>true</c> if [is at work] [the specified person]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAtWork
            (this Person person
            , DayOfWeek dayOfWeek)
        {
            int workDayNumber = (int)dayOfWeek;

            // Sunday:
            if (workDayNumber == 0)
            {
                workDayNumber = 7;
            }

            return workDayNumber <= person.DaysPerWeek;
        }
    }
}