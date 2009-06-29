﻿using System.Collections.Generic;
using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.Domain.Repository
{
    /// <summary>
    /// Handles storage of person off info.
    /// </summary>
    public interface IPersonOffRepository
    {
        /// <summary>
        /// Gets the person offs.
        /// </summary>
        /// <returns></returns>
        IList<PersonOff> GetPersonOffs();

        /// <summary>
        /// Inserts the person off.
        /// </summary>
        /// <param name="personOff">The person off.</param>
        void InsertPersonOff(PersonOff personOff);

        /// <summary>
        /// Deletes the person off.
        /// </summary>
        /// <param name="personOffId">The person off id.</param>
        void DeletePersonOff(int personOffId);

        /// <summary>
        /// Updates the person off.
        /// </summary>
        /// <param name="personOff">The person off.</param>
        void UpdatePersonOff(PersonOff personOff);
    }
}