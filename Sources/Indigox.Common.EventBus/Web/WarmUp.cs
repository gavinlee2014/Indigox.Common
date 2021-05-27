using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using Indigox.Common.Configuration.Web;

namespace Indigox.Common.EventBus
{
    class WarmUp : IWarmUp
    {
        public void OnApplicationStart()
        {
            var configurator = new Indigox.Common.EventBus.Configuration.XmlEventsConfigurator("config\\events.xml");
            configurator.Configure();
        }
    }
}
