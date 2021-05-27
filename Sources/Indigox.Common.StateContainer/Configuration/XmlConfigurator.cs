using System;
using Indigox.Common.Configuration;
using Indigox.Common.StateContainer.StateContexts;
using Indigox.Common.Utilities;

namespace Indigox.Common.StateContainer.Configuration
{
    public class XmlConfigurator : Configurator<StateContainerSection>
    {
        private const string AppConfigPath = "indigo/stateContainer";
        private const string XmlRootName = "config/stateContainer";

        private string path;

        public XmlConfigurator()
        {
        }

        public XmlConfigurator( string path )
        {
            this.path = path;
        }

        public override void Configure()
        {
            ListenerManager.Instance.Clear();

            if ( string.IsNullOrEmpty( this.path ) )
            {
                this.LoadFromConfig( AppConfigPath );
            }
            else
            {
                string fullpath = GetConfigFileFullPath( this.path );
                this.LoadFromXMLFile( fullpath, XmlRootName );
            }

            foreach ( StateContainerSection section in this )
            {
                foreach ( ListenerElement element in section.Listeners )
                {
                    IStateContextListener listener = (IStateContextListener)ReflectUtil.CreateInstance( element.TypeName );
                    ListenerManager.Instance.Register( listener );
                }
            }
        }

        protected override string GetKeyForItem( StateContainerSection item )
        {
            return Guid.NewGuid().ToString();
        }
    }
}
