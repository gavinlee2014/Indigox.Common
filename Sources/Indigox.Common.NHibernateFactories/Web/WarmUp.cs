using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Indigox.Common.Configuration.Web;

namespace Indigox.Common.NHibernateFactories.Web
{
    class WarmUp : IWarmUp
    {
        public void OnApplicationStart()
        {
            var configurator = new Indigox.Common.NHibernateFactories.Configuration.XmlConfigurator("config\\factories.xml");
            configurator.Configure();
        }
    }
}
