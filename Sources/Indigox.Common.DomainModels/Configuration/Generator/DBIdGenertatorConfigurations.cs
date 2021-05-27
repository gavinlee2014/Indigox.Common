using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.Configuration;

namespace Indigox.Common.DomainModels.Configuration.Generator
{
    public class DBIdGenertatorConfigurations : ConfigurationsCollection<DBIdGenertatorElement>
    {
        private static DBIdGenertatorConfigurations instance;

        public static DBIdGenertatorConfigurations Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DBIdGenertatorConfigurations();
                }
                return instance;
            }
        }

        protected override string GetKeyForItem(DBIdGenertatorElement item)
        {
            return item.SerialName;
        }

        public DBIdGenertatorElement GetConfiguration(string serialName)
        {
            if (this.Contains(serialName))
            {
                return this[serialName];
            }

            return null;
        }

    }
}
