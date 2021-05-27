using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Indigox.Common.DomainModels.Configuration;
using System.IO;

namespace Indigox.Common.DomainModels.Test.Configuration
{
    [TestFixture]
    public class InstanceMapTest
    {
        [Test]
        public void LoadXml(){
            string path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Config/instanceMaps.xml");
            Console.WriteLine(path );
            InstanceMapConfigurator configurator = new InstanceMapConfigurator(path);
            configurator.Configure();
        }
    }
}
