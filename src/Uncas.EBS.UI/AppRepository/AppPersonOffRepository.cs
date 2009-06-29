using System;
using System.Collections.Generic;
using Uncas.EBS.Domain.Model;
using Uncas.EBS.Domain.Repository;

namespace Uncas.EBS.UI.AppRepository
{
    public class AppPersonOffRepository
    {
        private IPersonOffRepository _personOffRepo =
            App.Repositories.PersonOffRepository;

        public IList<PersonOff> GetPersonOffs()
        {
            return _personOffRepo.GetPersonOffs();
        }

        public void InsertPersonOff(DateTime FromDate, DateTime ToDate)
        {
            _personOffRepo.InsertPersonOff
                (new PersonOff(FromDate, ToDate));
        }

        public void UpdatePersonOff(DateTime FromDate
            , DateTime ToDate
            , int Original_PersonOffId)
        {
            _personOffRepo.UpdatePersonOff
                (PersonOff.ReconstructPersonOff(Original_PersonOffId
                , FromDate, ToDate));
        }

        public void DeletePersonOff(int Original_PersonOffId)
        {
            _personOffRepo.DeletePersonOff(Original_PersonOffId);
        }
    }
}