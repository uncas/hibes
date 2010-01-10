using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        [SuppressMessage("Microsoft.Design"
            , "CA1024:UsePropertiesWhereAppropriate"
            , Justification = "Read from database")]
        public IList<Person> GetPersons()
        {
            return _personRepo.GetPersons();
        }

        public bool InsertPerson(string personName
            , int daysPerWeek
            , double hoursPerDay)
        {
            Person person = new Person(personName
                , daysPerWeek
                , hoursPerDay);
            _personRepo.InsertPerson(person);
            return true;
        }

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
            _personRepo.UpdatePerson(person);
            return true;
        }

        public bool DeletePerson
            (int personId)
        {
            _personRepo.DeletePerson(personId);
            return true;
        }
    }
}