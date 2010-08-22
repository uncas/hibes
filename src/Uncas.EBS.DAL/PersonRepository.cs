using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.ViewModel;
using Model = Uncas.EBS.Domain.Model;

namespace Uncas.EBS.DAL
{
    /// <summary>
    /// Repository for person info.
    /// </summary>
    internal class PersonRepository : BaseRepository
        , IPersonRepository
    {
        #region IPersonRepository Members

        /// <summary>
        /// Gets the person views.
        /// </summary>
        /// <returns>A list of persons.</returns>
        public IList<PersonView> GetPersonViews()
        {
            var result = DB.Persons
                .Select(p =>
                    new PersonView
                        (p.PersonId
                        , p.PersonName
                        , GetPersonOffs(p)));

            return result.ToList();
        }

        [SuppressMessage(
            "Microsoft.Performance",
            "CA1811:AvoidUncalledPrivateCode",
            Justification = "Is called in Linq")]
        private static List<Model.PersonOff> GetPersonOffs
            (Person person)
        {
            return person.PersonOffs.Select
                (po => new Model.PersonOff
                    (po.RefPersonId
                    , po.FromDate
                    , po.ToDate))
                .ToList();
        }

        /// <summary>
        /// Gets the persons.
        /// </summary>
        /// <returns>A list of persons.</returns>
        public IList<Model.Person> GetPersons()
        {
            var result = DB.Persons
                .Select(p =>
                    new Model.Person
                        (p.PersonId
                        , p.PersonName
                        , p.DaysPerWeek
                        , (double)p.HoursPerDay));

            return result.ToList();
        }

        /// <summary>
        /// Inserts the person.
        /// </summary>
        /// <param name="person">The person.</param>
        public void InsertPerson(Model.Person person)
        {
            var databasePerson = new Person
            {
                DaysPerWeek = person.DaysPerWeek,
                HoursPerDay = (decimal)person.HoursPerDay,
                PersonName = person.PersonName,
            };

            DB.Persons.InsertOnSubmit(databasePerson);

            this.SubmitChanges();

            person.PersonId = databasePerson.PersonId;
        }

        /// <summary>
        /// Updates the person.
        /// </summary>
        /// <param name="person">The person.</param>
        public void UpdatePerson(Model.Person person)
        {
            var databasePerson = GetDbPerson(person.PersonId);
            if (databasePerson == null)
            {
                throw new RepositoryException("No such person.");
            }

            databasePerson.DaysPerWeek = person.DaysPerWeek;
            databasePerson.HoursPerDay = (decimal)person.HoursPerDay;
            databasePerson.PersonName = person.PersonName;

            DB.SubmitChanges();
        }

        /// <summary>
        /// Deletes the person.
        /// </summary>
        /// <param name="personId">The person id.</param>
        public void DeletePerson(int personId)
        {
            Person person = GetDbPerson(personId);
            if (person == null)
            {
                throw new RepositoryException("No such person.");
            }

            DB.Persons.DeleteOnSubmit(person);
            DB.SubmitChanges();
        }

        #endregion

        private Person GetDbPerson(int personId)
        {
            return DB.Persons
                .Where(p => p.PersonId == personId)
                .SingleOrDefault();
        }
    }
}