using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Indigox.Common.DomainModels.Factory;
using Indigox.Common.DomainModels.Test.Repository.NHibernateImpl;
using Indigox.Common.DomainModels.Repository.Interface;
using Indigox.TestUtility;

namespace Indigox.Common.DomainModels.Test.Repository
{
    [TestFixture]
    public class NHibernateRepositoryTest
    {
        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            DbUtil.ClearData("drop table dbo.StreetAssignmentMap");
            DbUtil.ClearData("drop table dbo.Street");
            DbUtil.ClearData("drop table dbo.DustMan");
        }

        [Test]
        public void TestManyToMany()
        {
            RepositoryFactory.Instance.Register<Street>(typeof(NHibernateRepository<Street>));
            RepositoryFactory.Instance.Register<DustMan>(typeof(NHibernateRepository<DustMan>));

            IRepository<Street> repos = RepositoryFactory.Instance.CreateRepository<Street>();

            DustMan jack = new DustMan();
            jack.Name = "Jack Winkle";

            DustMan rose = new DustMan();
            rose.Name = "Rose Van Serk";

            Street street = new Street();
            street.Name = "Wisteria Lane";

            street.AddDustMan(jack);
            street.AddDustMan(rose);
            repos.Add(street);
            DbAssert.AreExists(String.Format("select count(*) from dbo.Street where StreetID='{0}'", street.GUID.ToString()));
            DbAssert.AreExists(String.Format("select count(*) from dbo.DustMan where DustManID='{0}'", jack.GUID.ToString()));
            DbAssert.AreExists(String.Format("select count(*) from dbo.DustMan where DustManID='{0}'", rose.GUID.ToString()));
            DbAssert.AreExists(String.Format("select count(*) from dbo.StreetAssignmentMap where StreetID='{0}' and DustManID='{1}'", street.GUID.ToString(), jack.GUID.ToString()));
            DbAssert.AreExists(String.Format("select count(*) from dbo.StreetAssignmentMap where StreetID='{0}' and DustManID='{1}'", street.GUID.ToString(), rose.GUID.ToString()));
        }
    }
}
