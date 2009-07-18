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

        public IList<PersonOff> GetPersonOffs()
        {
            return _personOffRepo.GetPersonOffs();
        }

        public void InsertPersonOff
            (DateTime FromDate
            , DateTime ToDate)
        {
            _personOffRepo.InsertPersonOff
                (new PersonOff(FromDate, ToDate));
        }

        public void UpdatePersonOff
            (DateTime FromDate
            , DateTime ToDate
            , int Original_PersonOffId)
        {
            // TODO: PERSON: Input person id from person off form:

            _personOffRepo.UpdatePersonOff
                (PersonOff.ReconstructPersonOff(Original_PersonOffId
                , FromDate, ToDate, 1));
        }

        public void DeletePersonOff(int Original_PersonOffId)
        {
            _personOffRepo.DeletePersonOff(Original_PersonOffId);
        }
    }
}