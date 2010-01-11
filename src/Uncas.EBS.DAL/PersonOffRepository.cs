using System;
using System.Collections.Generic;
using System.Linq;
using Uncas.EBS.Domain.Repository;
using Model = Uncas.EBS.Domain.Model;

namespace Uncas.EBS.DAL
{
    /// <summary>
    /// Repository for person off info.
    /// </summary>
    internal class PersonOffRepository : BaseRepository
        , IPersonOffRepository
    {
        #region IPersonOffRepository Members

        /// <summary>
        /// Gets the person offs.
        /// </summary>
        /// <param name="personId">The person id.</param>
        /// <returns>A list of person off information.</returns>
        public IList<Model.PersonOff> GetPersonOffs(int personId)
        {
            return DB.PersonOffs
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

        /// <summary>
        /// Inserts the person off.
        /// </summary>
        /// <param name="personOff">The person off.</param>
        public void InsertPersonOff(Model.PersonOff personOff)
        {
            DB.PersonOffs.InsertOnSubmit
                (new PersonOff
                {
                    FromDate = personOff.FromDate,
                    ToDate = personOff.ToDate,
                    RefPersonId = personOff.RefPersonId
                });
            this.SubmitChanges();
        }

        /// <summary>
        /// Deletes the person off.
        /// </summary>
        /// <param name="personOffId">The person off id.</param>
        public void DeletePersonOff(int personOffId)
        {
            DB.PersonOffs.DeleteOnSubmit
                (GetPersonOff(personOffId));
            DB.SubmitChanges();
        }

        /// <summary>
        /// Updates the person off.
        /// </summary>
        /// <param name="personOff">The person off.</param>
        public void UpdatePersonOff(Model.PersonOff personOff)
        {
            if (!personOff.PersonOffId.HasValue)
            {
                return;
            }

            var dbpo = GetPersonOff(personOff.PersonOffId.Value);
            dbpo.FromDate = personOff.FromDate;
            dbpo.ToDate = personOff.ToDate;
            DB.SubmitChanges();
        }

        #endregion

        private PersonOff GetPersonOff(int personOffId)
        {
            return DB.PersonOffs
                .Where(po => po.PersonOffId == personOffId)
                .SingleOrDefault();
        }
    }
}