using System;
using System.Collections.Generic;
using System.Linq;

namespace Uncas.EBS.Domain.Model
{
    /// <summary>
    /// Extension methods for the person off class.
    /// </summary>
    public static class PersonOffExtensions
    {
        /// <summary>
        /// Determines whether [is person off] [the specified person offs].
        /// </summary>
        /// <param name="personOffs">The person offs.</param>
        /// <param name="date">The date to check.</param>
        /// <returns>
        ///     <c>true</c> if [is person off] [the specified person offs]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsPersonOff
            (this IList<PersonOff> personOffs
            , DateTime date)
        {
            return personOffs != null
                && personOffs.Any(po => po.IsPersonOff(date));
        }
    }
}