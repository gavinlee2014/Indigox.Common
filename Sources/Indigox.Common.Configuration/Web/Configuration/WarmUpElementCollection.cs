using System;
using System.Configuration;

namespace Indigox.Common.Configuration.Web.Configuration
{
    public class WarmUpElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new WarmUpElement();
        }

        protected override object GetElementKey( ConfigurationElement element )
        {
            return ( (WarmUpElement)element ).Name;
        }
    }
}