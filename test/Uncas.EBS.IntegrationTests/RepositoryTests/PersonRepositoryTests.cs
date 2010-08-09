using System;
using System.Linq;
using NUnit.Framework;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;

namespace Uncas.EBS.IntegrationTests.RepositoryTests
{
    [TestFixture]
    public class PersonRepositoryTests
    {
        private IPersonRepository _personRepo =
            TestApp.Repositories.PersonRepository;

        [Test]
        public void UpdatePerson_Default_OK()
        {
            Person person = new Person
                (Guid.NewGuid().ToString()
                , 1
                , 2);
            _personRepo.InsertPerson(person);

            int newDays = 2;
            double newHours = 3d;

            person.PersonName = Guid.NewGuid().ToString();
            person.DaysPerWeek = newDays;
            person.HoursPerDay = newHours;

            _personRepo.UpdatePerson(person);

            Person retrievedPerson
                = _personRepo.GetPersons()
                .Where(p => p.PersonId == person.PersonId)
                .SingleOrDefault();

            Assert.AreEqual
                (person, retrievedPerson);
        }
    }
}