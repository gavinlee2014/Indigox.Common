using System;
using System.Collections.Generic;
using Indigox.Common.Configuration.Web;
using Indigox.Common.DomainModels.Factory;

namespace Indigox.Common.DomainModels.Repository.NHibernateImpl.Web
{
    public class WarmUp : IWarmUp
    {
        public void OnApplicationStart()
        {
            RepositoryFactory.Instance.RegisterDefault( typeof( NHibernateRepository<> ) );
        }
    }
}
