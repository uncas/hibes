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
            var personOffs = por.GetPersonOffs();
            var personView = new PersonView(personOffs);
            personViews.Add(personView);

            return personViews;
        }

        #endregion
    }
}