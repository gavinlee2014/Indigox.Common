using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.Configuration;

namespace Indigox.Common.DomainModels.Configuration.Generator
{
    public class DBIdGenertatorConfigurator : Configurator<DBIdGenertatorSection>
    {
        private string path;

        public DBIdGenertatorConfigurator()
        {
        }

        public DBIdGenertatorConfigurator(string path)
        {
            this.path = path;
        }

        public override void Configure()
        {
            if (string.IsNullOrEmpty(path))
            {
                this.LoadFromConfig("indigo/dbIdGenertator");
            }
            else
            {
                string fullpath = GetConfigFileFullPath(this.path);
                this.LoadFromXMLFile(path, "dbIdGenertators");
            }

            foreach (DBIdGenertatorSection section in this)
            {
                foreach (DBIdGenertatorElement configuration in section.DBIdGenertators)
                {
                    DBIdGenertatorConfigurations.Instance.Register(configuration);
                }
            }
        }

        protected override string GetKeyForItem(DBIdGenertatorSection item)
        {
            return Guid.NewGuid().ToString();
        }
    }
}
