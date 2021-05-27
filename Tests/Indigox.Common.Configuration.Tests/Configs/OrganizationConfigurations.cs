using System;

namespace Indigox.Common.Configuration.Test.Configs
{
    public class OrganizationConfigurations : ConfigurationsCollection<OrganizationConfigSection>
    {
        private const string defaultKey = "OrganizationSection";

        private OrganizationConfigurations()
        {
        }

        private static OrganizationConfigurations instance;

        public static OrganizationConfigurations Instance
        {
            get
            {
                if ( instance == null )
                {
                    instance = new OrganizationConfigurations();
                }
                return instance;
            }
        }

        /// <summary>
        /// useful for test
        /// </summary>
        public new static void Clear()
        {
            instance = null;
        }

        protected override string GetKeyForItem( OrganizationConfigSection item )
        {
            return defaultKey;
        }
    }
}