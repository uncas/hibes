using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Uncas.EBS.Domain;
using Uncas.EBS.Domain.Model;

namespace Uncas.EBS.IntegrationTests.RepositoryTests
{
    [TestFixture]
    public class PersonOffRepositoryTests
    {
        [Test]
        public void
            GetPersonOffs_DifferentPersons_DifferentPersonOffs()
        {
            var personOffLists
                = new List<IList<PersonOff>>();
            foreach (var person in
                TestApp.Repositories.PersonRepository
                .GetPersons(new Paging()))
            {
                var personOffs = TestApp.Repositories
                    .PersonOffRepository
                    .GetPersonOffs(person.PersonId);
                personOffLists.Add(personOffs);
            }

            if (personOffLists.Count >= 2)
            {
                IList<PersonOff> po1 = personOffLists[0];
                IList<PersonOff> po2 = personOffLists[1];
                for (int i = 0;
                    i < personOffLists.Max(pol => pol.Count);
                    i++)
                {
                    if (i < po1.Count && i < po2.Count)
                    {
                        Assert.AreNotEqual(po1[i].PersonOffId
                            , po2[i].PersonOffId);
                    }
                }
            }
        }
    }
}