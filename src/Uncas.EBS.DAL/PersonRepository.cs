using System.Collections.Generic;
using Uncas.EBS.Domain.Repository;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.DAL
{
    class PersonRepository : BaseRepository, IPersonRepository
    {
        #region IPersonRepository Members

        public IList<PersonView> GetPersonViews()
        {
            // HACK: PERSON: Implement real fetch from db.

            PersonOffRepository por = new PersonOffRepository();

            var personViews = new List<PersonView>();

            // HACK: PERSON: Simulating two persons here:

            var personOffs = por.GetPersonOffs();
            var personViewA = new PersonView
                (1, "A.A.", 5, 7.5d, personOffs);
            personViews.Add(personViewA);

            var personViewB = new PersonView
                (2, "B.B.", 3, 5d, null);
            personViews.Add(personViewB);

            return personViews;
        }

        #endregion
    }
}