using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.Configuration.Web;
using System.IO;
using Indigox.Common.DomainModels.Configuration;

namespace Indigox.Common.DomainModels.Initialization
{
    public class Initializer : IWarmUp
    {
        public void OnApplicationStart()
        {
            string basedir = GetBaseDir();
            string fullpath = Path.Combine(basedir, "config\\instanceMaps.xml");
            InstanceMapConfigurator configurator = new InstanceMapConfigurator(fullpath);
            configurator.Configure();
        }

        private static string GetBaseDir()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
