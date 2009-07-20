using System;
using System.Collections.Generic;
using System.Linq;
using Uncas.EBS.Domain.Repository;
using Model = Uncas.EBS.Domain.Model;

namespace Uncas.EBS.DAL
{
    class PersonOffRepository : BaseRepository
        , IPersonOffRepository
    {
        #region IPersonOffRepository Members

        public IList<Model.PersonOff> GetPersonOffs(int personId)
        {
            return db.PersonOffs
                .Where(po
                    => po.RefPersonId == personId
                    && po.ToDate.Date >= DateTime.Now.Date)
                .OrderBy(po => po.ToDate)
                .Select(po => Model.PersonOff.ReconstructPersonOff
                    (po.PersonOffId
                    , po.FromDate
                    , po.ToDate
                    , po.RefPersonId))
                .ToList();
        }

        public void InsertPersonOff(Model.PersonOff personOff)
        {
            db.PersonOffs.InsertOnSubmit
                (new PersonOff
                {
                    FromDate = personOff.FromDate,
                    ToDate = personOff.ToDate,
                    RefPersonId = personOff.RefPersonId
                });
            base.SubmitChanges();
        }

        public void DeletePersonOff(int personOffId)
        {
            db.PersonOffs.DeleteOnSubmit
                (GetPersonOff(personOffId));
            db.SubmitChanges();
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

        private PersonOff GetPersonOff(int personOffId)
        {
            return db.PersonOffs
                .Where(po => po.PersonOffId == personOffId)
                .SingleOrDefault();
        }
    }
}