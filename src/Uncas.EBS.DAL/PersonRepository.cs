using System.Collections.Generic;
using System.Linq;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.ViewModel;
using Model = Uncas.EBS.Domain.Model;

namespace Uncas.EBS.DAL
{
    class PersonRepository : BaseRepository, IPersonRepository
    {
        private static IList<PersonView> _personViews = null;
        public static IList<PersonView> PersonViews
        {
            get
            {
                if (_personViews == null)
                {
                    _personViews = new List<PersonView>();
                    var personOffRepository
                        = new PersonOffRepository();

                    // Simulating two persons here:

                    var personOffs
                        = personOffRepository.GetPersonOffs(1);
                    var personViewA = new PersonView
                        (1, "A.A.", 5, 7.5d, personOffs);
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
            // TODO: PERSON: Retrieve person views from db.

            return PersonViews;
        }

        public IList<Model.Person> GetPersons()
        {
            // TODO: PERSON: Retrieve persons from db.

            return PersonViews
                .Cast<Model.Person>()
                .ToList();
        }

        public void InsertPerson(Model.Person person)
        {
            // TODO: PERSON: Insert person in db.

            PersonViews.Add(new PersonView
                (PersonViews.Max(pv => pv.PersonId) + 1
                , person.PersonName
                , person.DaysPerWeek
                , person.HoursPerDay
                , null));
        }

        public void UpdatePerson(Uncas.EBS.Domain.Model.Person person)
        {
            // TODO: PERSON: Update person in db.
        }

        public void DeletePerson(int personId)
        {
            // TODO: PERSON: Delete person from db.
        }

        #endregion
    }

}