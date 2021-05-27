using System;
using System.IO;
using System.Xml;
using Indigox.Common.EventBus;
using Indigox.Common.Configuration;
using Indigox.Common.Utilities;

namespace Indigox.Common.EventBus.Configuration
{
    public class XmlEventsConfigurator : Configurator<EventsSection>
    {
        private string path;

        public XmlEventsConfigurator()
        {
        }

        public XmlEventsConfigurator( string path )
        {
            this.path = path;
        }

        public override void Configure()
        {
            //StateMachineSection machines = (string.IsNullOrEmpty(path)) ? 
            //    StateMachineSection.LoadFromAppConfig() :
            //    StateMachineSection.LoadFromXmlFile(path);

            if ( string.IsNullOrEmpty( this.path ) )
            {
                this.LoadFromConfig( "indigo/events" );
            }
            else
            {
                string fullpath = GetConfigFileFullPath( this.path );
                this.LoadFromXMLFile( fullpath, "config" );
            }

            foreach ( EventsSection section in this )
            {
                foreach ( SourceElement source in section.Sources )
                {
                    foreach ( EventElement event_ in source.Events )
                    {
                        foreach ( ListenerElement listener in event_.Listeners )
                        {
                            EventRegister.Instance.Register(
                                    Type.GetType( event_.TypeName, true ),
                                    Type.GetType( source.TypeName, true ),
                                    SingletoneFactory.GetInstance( listener.TypeName ),
                                    listener.MethodName
                                );
                        }
                    }
                }
            }
        }

        protected override string GetKeyForItem( EventsSection item )
        {
            return Guid.NewGuid().ToString();
        }

        private static string GetConfigFileFullPath( string path )
        {
            string fullPath = path;
            if ( !File.Exists( fullPath ) )
            {
                fullPath = Path.Combine( GetBaseDir(), fullPath );
            }
            if ( !File.Exists( fullPath ) )
            {
                throw new FileNotFoundException( "找不到配置文件：" + fullPath );
            }
            return fullPath;
        }

        private static string GetBaseDir()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
