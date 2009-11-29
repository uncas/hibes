using System;
using System.Collections.Generic;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.Tests.Fakes
{
    internal class FakePersonRepository : IPersonRepository
    {
        #region IPersonRepository Members

        public IList<PersonView> GetPersonViews()
        {
            var result = new List<PersonView>();
            result.Add(new PersonView(1, "N.N.", null));
            return result;
        }

        public IList<Person> GetPersons()
        {
            throw new NotImplementedException();
        }

        public void InsertPerson(Person person)
        {
            throw new NotImplementedException();
        }

        public void UpdatePerson(Person person)
        {
            throw new NotImplementedException();
        }

        public void DeletePerson(int personId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
