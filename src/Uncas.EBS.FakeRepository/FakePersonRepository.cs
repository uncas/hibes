using System.Collections.Generic;
using System.Linq;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.FakeRepository
{
    class FakePersonRepository : IPersonRepository
    {
        private static IList<PersonView> _personViews = null;
        public static IList<PersonView> PersonViews
        {
            get
            {
                if (_personViews == null)
                {
                    _personViews = new List<PersonView>();

                    // Simulating two persons here:

                    var personViewA = new PersonView
                        (1, "A.A.", 5, 7.5d, null);
                    _personViews.Add(personViewA);

                    var personViewB = new PersonView
                        (2, "B.B.", 3, 5d, null);
                    _personViews.Add(personViewB);
                }
                return _personViews;
            }
        }

        #region IPersonRepository Members

        public IList<PersonView> GetPersonViews()
        {
            return PersonViews;
        }

        public IList<Person> GetPersons()
        {
            return PersonViews
                .Cast<Person>()
                .ToList();
        }

        public void InsertPerson(Person person)
        {
            PersonViews.Add(new PersonView
                (PersonViews.Max(pv => pv.PersonId) + 1
                , person.PersonName
                , person.DaysPerWeek
                , person.HoursPerDay
                , null));
        }

        public void UpdatePerson(Person person)
        {
        }

        public void DeletePerson(int personId)
        {
        }

        #endregion
    }

}