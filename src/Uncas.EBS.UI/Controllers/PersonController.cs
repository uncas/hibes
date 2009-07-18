using System.Collections.Generic;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;

namespace Uncas.EBS.UI.Controllers
{
    public class PersonController
    {
        private IRepositoryFactory _repositories;

        private IPersonRepository _personRepo
        {
            get
            {
                return _repositories.PersonRepository;
            }
        }

        public PersonController()
            : this(App.Repositories)
        {

        }

        public PersonController(IRepositoryFactory repositories)
        {
            this._repositories = repositories;
        }

        public IList<Person> GetPersons()
        {
            return _personRepo.GetPersons();
        }

        public bool InsertPerson(string PersonName
            , int DaysPerWeek
            , double HoursPerDay)
        {
            Person person = new Person(PersonName
                , DaysPerWeek
                , HoursPerDay);
            _personRepo.InsertPerson(person);
            return true;
        }

        public bool UpdatePerson(string PersonName
            , int DaysPerWeek
            , double HoursPerDay
            , int Original_PersonId)
        {
            Person person = new Person
                (Original_PersonId
                , PersonName
                , DaysPerWeek
                , HoursPerDay);
            _personRepo.UpdatePerson(person);
            return true;
        }

        public bool DeletePerson
            (int Original_PersonId)
        {
            _personRepo.DeletePerson(Original_PersonId);
            return true;
        }
    }
}