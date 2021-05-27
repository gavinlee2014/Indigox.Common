using System;
using Indigox.Common.Configuration;
using Indigox.Common.DomainModels.Repository;
using Indigox.Common.Logging;

namespace Indigox.Common.DomainModels.Configuration
{
    public class InstanceMapConfigurator : Configurator<InstanceMapSection>
    {
        private const string AppConfigPath = "indigo/instanceMaps";
        private const string XmlRootName = "config";

        private string path;

        public InstanceMapConfigurator()
        {
        }

        public InstanceMapConfigurator( string path )
        {
            this.path = path;
        }

        public override void Configure()
        {
            if ( string.IsNullOrEmpty( this.path ) )
            {
                this.LoadFromConfig( AppConfigPath );
            }
            else
            {
                string fullpath = GetConfigFileFullPath( this.path );
                this.LoadFromXMLFile( fullpath, XmlRootName );
            }

            foreach ( InstanceMapSection section in this )
            {
                foreach ( InstanceMapElement instanceMapElement in section.InstanceMaps )
                {
                    Log.Debug( instanceMapElement.Instance );
                    try
                    {
                        Type interfaceType = Type.GetType( instanceMapElement.Interface, true );
                        Type instanceType = Type.GetType( instanceMapElement.Instance, true );
                        InstanceTypeMapping.Instance.registerInstance( interfaceType, instanceType );
                    }
                    catch ( Exception e )
                    {
                        Log.Debug( string.Format( "Some Error Occured when Configure InstanceMapElement <instanceMap interface=\"{0}\" instance=\"{1}\" >", instanceMapElement.Interface, instanceMapElement.Instance ) );
                        Log.Debug( e.ToString() );
                    }
                }
            }
        }

        protected override string GetKeyForItem( InstanceMapSection item )
        {
            return Guid.NewGuid().ToString();
        }
    }
}