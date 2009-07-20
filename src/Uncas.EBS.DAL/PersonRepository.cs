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
            var result = db.Persons
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
            var result = db.Persons
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

            db.Persons.InsertOnSubmit(dbPerson);

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

            db.SubmitChanges();
        }

        public void DeletePerson(int personId)
        {
            Person person = GetDbPerson(personId);
            if (person == null)
            {
                throw new RepositoryException("No such person.");
            }
            db.Persons.DeleteOnSubmit(person);
            db.SubmitChanges();
        }

        #endregion

        private Person GetDbPerson(int personId)
        {
            return db.Persons
                .Where(p => p.PersonId == personId)
                .SingleOrDefault();
        }
    }
}