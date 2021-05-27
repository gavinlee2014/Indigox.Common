using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using Indigox.Common.NHibernateFactories;
using Indigox.Common.Configuration;
using Indigox.Common.Logging;

namespace Indigox.Common.NHibernateFactories.Configuration
{
    public class XmlConfigurator : Configurator<FactoriesSection>
    {
        private string path;

        public XmlConfigurator()
        {
            Log.Debug("XmlConfigurator init blank");
        }

        public XmlConfigurator(string path)
        {
            Log.Debug("XmlConfigurator init path" + path);
            this.path = path;
        }

        public override void Configure()
        {
            Log.Debug("NHibernateFactories Configure start");
            if (string.IsNullOrEmpty(this.path))
            {
                Log.Debug("NHibernateFactories Configure load from indigo/factories");
                this.LoadFromConfig("indigo/factories");
            }
            else
            {
                string fullpath = GetConfigFileFullPath(this.path);
                Log.Debug("NHibernateFactories Configure load from fullpath " + fullpath);
                this.LoadFromXMLFile(fullpath, "config");
            }

            foreach (FactoriesSection section in this)
            {
                foreach (FactoryElement Factory in section.Factories)
                {
                    Log.Debug("NHibernateFactories Configure section.Factories " + Factory.AssemblyName);
                    SessionFactories.Instance.Register(Factory.AssemblyName, 
                                                        Factory.Path, 
                                                        Factory.ConnectionString, 
                                                        Factory.AutoBind);

                }
            }
        }

        protected override string GetKeyForItem(FactoriesSection item)
        {
            return Guid.NewGuid().ToString();
        }

    }
}
