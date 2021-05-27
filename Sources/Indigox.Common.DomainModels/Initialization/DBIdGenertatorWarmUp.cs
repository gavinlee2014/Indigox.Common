using System;
using System.IO;
using Indigox.Common.Configuration.Web;
using Indigox.Common.DomainModels.Configuration.Generator;

namespace Indigox.Common.DomainModels.Initialization
{
    public class DBIdGenertatorWarmUp : IWarmUp
    {
        public void OnApplicationStart()
        {
            try
            {
                string fullpath = Path.Combine(GetBaseDir(), "config\\DBIdGenertators.xml");
                var configurator = new DBIdGenertatorConfigurator(fullpath);
                configurator.Configure();
            }
            catch (Exception)
            {
            }
        }

        private static string GetBaseDir()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
