using System.Collections.Generic;
using System.Linq;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.ViewModel;
using Model = Uncas.EBS.Domain.Model;

namespace Uncas.EBS.DAL
{
    class PersonRepository : BaseRepository, IPersonRepository
    {
        #region IPersonRepository Members

        public IList<PersonView> GetPersonViews()
        {
            var result = DB.Persons
                .Select(p =>
                    new PersonView
                        (p.PersonId
                        , p.PersonName
                        , p.PersonOffs.Select
                            (po => new Model.PersonOff
                                (po.RefPersonId
                                , po.FromDate
                                , po.ToDate)
                            ).ToList()
                        )
                        );

            return result.ToList();
        }

        public IList<Model.Person> GetPersons()
        {
            var result = DB.Persons
                .Select(p =>
                    new Model.Person
                        (p.PersonId
                        , p.PersonName
                        , p.DaysPerWeek
                        , (double)p.HoursPerDay
                        )
                        );

            return result.ToList();
        }

        public void InsertPerson(Model.Person person)
        {
            var dbPerson = new Person
            {
                DaysPerWeek = person.DaysPerWeek,
                HoursPerDay = (decimal)person.HoursPerDay,
                PersonName = person.PersonName,
            };

            DB.Persons.InsertOnSubmit(dbPerson);

            base.SubmitChanges();

            person.PersonId = dbPerson.PersonId;
        }

        public void UpdatePerson(Model.Person person)
        {
            Person dbPerson = GetDbPerson(person.PersonId);
            if (dbPerson == null)
            {
                throw new RepositoryException("No such person.");
            }

            dbPerson.DaysPerWeek = person.DaysPerWeek;
            dbPerson.HoursPerDay = (decimal)person.HoursPerDay;
            dbPerson.PersonName = person.PersonName;

            DB.SubmitChanges();
        }

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