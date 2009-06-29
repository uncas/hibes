using System;
using System.Collections.Generic;
using Uncas.EBS.Domain.Repository;
using Model = Uncas.EBS.Domain.Model;

namespace Uncas.EBS.DAL
{
    class PersonOffRepository : IPersonOffRepository
    {
        #region IPersonOffRepository Members

        public IList<Model.PersonOff> GetPersonOffs()
        {
            // HACK: Hardcoded days the person is off:
            var personOffs = new List<Model.PersonOff>();
            personOffs.Add(new Model.PersonOff
                (new DateTime(2009, 7, 13), new DateTime(2009, 7, 24)));
            personOffs.Add(new Model.PersonOff
                (new DateTime(2009, 8, 3), new DateTime(2009, 8, 7)));
            return personOffs;
        }

        public void InsertPersonOff(Model.PersonOff personOff)
        {
            throw new NotImplementedException();
        }

        public void DeletePersonOff(int personOffId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePersonOff(Model.PersonOff personOff)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}