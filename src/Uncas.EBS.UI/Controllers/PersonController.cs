using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Uncas.EBS.Domain;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;

namespace Uncas.EBS.UI.Controllers
{
    /// <summary>
    /// Controller for person info.
    /// </summary>
    public class PersonController
    {
        private IRepositoryFactory repositories;

        private IPersonRepository PersonRepository
        {
            get
            {
                return repositories.PersonRepository;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonController"/> class.
        /// </summary>
        public PersonController()
            : this(App.Repositories)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonController"/> class.
        /// </summary>
        /// <param name="repositories">The repositories.</param>
        public PersonController(IRepositoryFactory repositories)
        {
            this.repositories = repositories;
        }

        /// <summary>
        /// Gets the persons.
        /// </summary>
        /// <returns>A list of persons.</returns>
        /// TODO: [Obsolete("Use overload with paging instead")]
        [SuppressMessage("Microsoft.Design"
            , "CA1024:UsePropertiesWhereAppropriate"
            , Justification = "Read from database")]
        public IList<Person> GetPersons()
        {
            return PersonRepository.GetPersons(new Paging());
        }

        /// <summary>
        /// Inserts the person.
        /// </summary>
        /// <param name="personName">Name of the person.</param>
        /// <param name="daysPerWeek">The days per week.</param>
        /// <param name="hoursPerDay">The hours per day.</param>
        /// <returns>True if succesful.</returns>
        public bool InsertPerson(string personName
            , int daysPerWeek
            , double hoursPerDay)
        {
            Person person = new Person(personName
                , daysPerWeek
                , hoursPerDay);
            PersonRepository.InsertPerson(person);
            return true;
        }

        /// <summary>
        /// Updates the person.
        /// </summary>
        /// <param name="personName">Name of the person.</param>
        /// <param name="daysPerWeek">The days per week.</param>
        /// <param name="hoursPerDay">The hours per day.</param>
        /// <param name="personId">The person id.</param>
        /// <returns>True if succesful.</returns>
        public bool UpdatePerson
            (string personName
            , int daysPerWeek
            , double hoursPerDay
            , int personId)
        {
            Person person = new Person
                (personId
                , personName
                , daysPerWeek
                , hoursPerDay);
            PersonRepository.UpdatePerson(person);
            return true;
        }

        /// <summary>
        /// Deletes the person.
        /// </summary>
        /// <param name="personId">The person id.</param>
        /// <returns>True if succesful.</returns>
        public bool DeletePerson
            (int personId)
        {
            PersonRepository.DeletePerson(personId);
            return true;
        }
    }
}