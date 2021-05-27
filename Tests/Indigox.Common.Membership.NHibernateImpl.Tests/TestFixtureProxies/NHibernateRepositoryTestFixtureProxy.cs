using System;
using Indigox.Common.DomainModels.Factory;
using Indigox.Common.DomainModels.Repository.NHibernateImpl;
using Indigox.Common.NHibernateFactories.Configuration;
using Indigox.TestUtility.TestFixtures;

namespace Indigox.Common.Membership.NHibernateImpl.NUnitTest.TestFixtureProxies
{
    public class NHibernateRepositoryTestFixtureProxy : BaseTestFixtureProxy
    {
        public override void OnTestFixtureSetUp()
        {
            RegisteRepositories();
            InitializeReposiryFactories();
        }

        private void InitializeReposiryFactories()
        {
            XmlConfigurator configurator = new XmlConfigurator( "config\\Factories.xml" );
            configurator.Configure();
        }

        protected virtual void RegisteRepositories()
        {
            RepositoryFactory.Instance.RegisterDefault( typeof( NHibernateRepository<> ) );
        }
    }
}