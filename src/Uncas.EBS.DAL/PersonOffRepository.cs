using System;
using System.Collections.Generic;
using System.Linq;
using Uncas.EBS.Domain.Repository;
using Model = Uncas.EBS.Domain.Model;

namespace Uncas.EBS.DAL
{
    class PersonOffRepository : BaseRepository, IPersonOffRepository
    {
        #region IPersonOffRepository Members

        public IList<Model.PersonOff> GetPersonOffs(int personId)
        {
            // TODO: PERSON: Retrieve person id from dbPersonOff:
            return db.PersonOffs
                .Where(po => po.ToDate.Date >= DateTime.Now.Date)
                .OrderBy(po => po.ToDate)
                .Select(po => Model.PersonOff.ReconstructPersonOff
                    (po.PersonOffId
                    , po.FromDate
                    , po.ToDate
                    , personId))
                .ToList();
        }

        public void InsertPersonOff(Model.PersonOff personOff)
        {
            // TODO: PERSON: Save PersonOff.RefPersonId to db:
            db.PersonOffs.InsertOnSubmit
                (new PersonOff
                {
                    FromDate = personOff.FromDate,
                    ToDate = personOff.ToDate,
                    //RefPersonId = personOff.RefPersonId
                });
            base.SubmitChanges();
        }

        public void DeletePersonOff(int personOffId)
        {
            db.PersonOffs.DeleteOnSubmit(GetPersonOff(personOffId));
            db.SubmitChanges();
        }

        private PersonOff GetPersonOff(int personOffId)
        {
            return db.PersonOffs
                .Where(po => po.PersonOffId == personOffId)
                .SingleOrDefault();
        }

        public void UpdatePersonOff(Model.PersonOff personOff)
        {
            if (!personOff.PersonOffId.HasValue)
            {
                return;
            }
            var dbpo = GetPersonOff(personOff.PersonOffId.Value);
            dbpo.FromDate = personOff.FromDate;
            dbpo.ToDate = personOff.ToDate;
            db.SubmitChanges();
        }

        #endregion
    }
}
