﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.Domain.Repository
{
    /// <summary>
    /// Handles storage of person info.
    /// </summary>
    public interface IPersonRepository
    {
        /// <summary>
        /// Gets the person views.
        /// </summary>
        /// <param name="paging">The paging.</param>
        /// <returns>
        /// A list of persons.
        /// </returns>
        IList<PersonView> GetPersonViews(Paging paging);

        /// <summary>
        /// Gets the persons.
        /// </summary>
        /// <returns>A list of persons.</returns>
        /// TODO: [Obsolete("Use overload with paging instead")]
        [SuppressMessage("Microsoft.Design"
            , "CA1024:UsePropertiesWhereAppropriate"
            , Justification = "Read from database")]
        IList<Person> GetPersons();

        /// <summary>
        /// Inserts the person.
        /// </summary>
        /// <param name="person">The person.</param>
        void InsertPerson(Person person);

        /// <summary>
        /// Updates the person.
        /// </summary>
        /// <param name="person">The person.</param>
        void UpdatePerson(Person person);

        /// <summary>
        /// Deletes the person.
        /// </summary>
        /// <param name="personId">The person id.</param>
        void DeletePerson(int personId);
    }
}