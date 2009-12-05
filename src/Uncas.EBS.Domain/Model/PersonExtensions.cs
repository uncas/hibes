using System;

namespace Uncas.EBS.Domain.Model
{
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
        ///     <c>true</c> if [is at work] [the specified person]; otherwise, <c>false</c>.
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