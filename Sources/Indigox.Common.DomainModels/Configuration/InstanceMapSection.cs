using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.Configuration;
using System.Xml.Serialization;

namespace Indigox.Common.DomainModels.Configuration
{
    public class InstanceMapSection : ConfigSection
    {
        List<InstanceMapElement> instanceMaps;

        [XmlElement ("instanceMap", typeof(InstanceMapElement))]
        public List<InstanceMapElement> InstanceMaps
        {
            get
            {
                if (instanceMaps == null)
                {
                    instanceMaps = new List<InstanceMapElement>();
                }
                return instanceMaps;
            }
            set
            {
                this.instanceMaps = value;
            }
        }

    }
}
