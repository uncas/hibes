using System;
using System.Collections.Generic;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;

namespace Uncas.EBS.UI.Controllers
{
    public class PersonOffController
    {
        private IPersonOffRepository _personOffRepo =
            App.Repositories.PersonOffRepository;

        public IList<PersonOff> GetPersonOffs(int refPersonId)
        {
            return _personOffRepo.GetPersonOffs(refPersonId);
        }

        public void InsertPersonOff
            (int refPersonId
            , DateTime fromDate
            , DateTime toDate)
        {
            _personOffRepo.InsertPersonOff
                (new PersonOff(refPersonId, fromDate, toDate));
        }

        public void UpdatePersonOff
            (DateTime fromDate
            , DateTime toDate
            , int personOffId)
        {
            _personOffRepo.UpdatePersonOff
                (PersonOff.ReconstructPersonOff
                    (personOffId
                    , fromDate, toDate, 1));
        }

        public void DeletePersonOff(int personOffId)
        {
            _personOffRepo.DeletePersonOff(personOffId);
        }
    }
}