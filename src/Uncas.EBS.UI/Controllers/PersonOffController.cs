using System;
using System.Collections.Generic;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;

namespace Uncas.EBS.UI.Controllers
{
    /// <summary>
    /// Controller for handling info about person off.
    /// </summary>
    public class PersonOffController
    {
        private IPersonOffRepository personOffRepo =
            App.Repositories.PersonOffRepository;

        /// <summary>
        /// Gets the person offs.
        /// </summary>
        /// <param name="refPersonId">The ref person id.</param>
        /// <returns>A list of person offs.</returns>
        public IList<PersonOff> GetPersonOffs(int refPersonId)
        {
            return personOffRepo.GetPersonOffs(refPersonId);
        }

        /// <summary>
        /// Inserts the person off.
        /// </summary>
        /// <param name="refPersonId">The ref person id.</param>
        /// <param name="fromDate">The from date.</param>
        /// <param name="toDate">The to date.</param>
        public void InsertPersonOff
            (int refPersonId
            , DateTime fromDate
            , DateTime toDate)
        {
            personOffRepo.InsertPersonOff
                (new PersonOff(refPersonId, fromDate, toDate));
        }

        /// <summary>
        /// Updates the person off.
        /// </summary>
        /// <param name="fromDate">The from date.</param>
        /// <param name="toDate">The to date.</param>
        /// <param name="personOffId">The person off id.</param>
        public void UpdatePersonOff
            (DateTime fromDate
            , DateTime toDate
            , int personOffId)
        {
            personOffRepo.UpdatePersonOff
                (PersonOff.ReconstructPersonOff
                    (personOffId
                    , fromDate
                    , toDate
                    , 1));
        }

        /// <summary>
        /// Deletes the person off.
        /// </summary>
        /// <param name="personOffId">The person off id.</param>
        public void DeletePersonOff(int personOffId)
        {
            personOffRepo.DeletePersonOff(personOffId);
        }
    }
}