using System.Collections.Generic;
using Uncas.EBS.Domain.ViewModel;
using Uncas.EBS.Domain.Model;

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
        /// <returns></returns>
        IList<PersonView> GetPersonViews();

        /// <summary>
        /// Gets the persons.
        /// </summary>
        /// <returns></returns>
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