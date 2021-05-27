using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Indigox.Common.Configuration
{
    public abstract class Configurator<T> : KeyedCollection<string, T>
    {
        public new T this[ string key ]
        {
            get
            {
                if ( base.Contains( key ) )
                {
                    return base[ key ];
                }
                return default( T );
            }
        }

        protected void SetConfig( T section )
        {
            string key = this.GetKeyForItem( section );
            if ( !this.Contains( key ) )
            {
                this.Add( section );
            }
            else
            {
                throw new ArgumentException( "configuration key already exist" );
            }
        }

        public void LoadFromConfig( string path )
        {
            T section = (T)ConfigurationManager.GetSection( path );
            this.SetConfig( section );
        }

        public void LoadFromXMLFile( string path, string xmlRoot )
        {
            Indigox.Common.Logging.Log.Debug( "Configurator.LoadFromXMLFile( @\"" + path + "\", @\"" + xmlRoot + "\" )" );

            T section = default( T );

            Indigox.Common.Logging.Log.Debug( "new XIncludingReader( @\"" + path + "\" )" );

            try
            {
                #region debug code

                // using ( XmlReader xmlReader = new XIncludingReader( path ) )
                // {
                //     XmlDocument xdoc = new XmlDocument();
                //     xdoc.Load( xmlReader );
                //     Indigox.Common.Logging.Log.Debug( "Full Xml: " + xdoc.InnerXml );
                // }
                //
                // using ( XmlReader xmlReader = new XIncludingReader( path ) )
                // {
                //     if ( ReadToNode( xmlReader, xmlRoot ) )
                //     {
                //         XmlDocument xdoc = new XmlDocument();
                //         xdoc.Load( xmlReader );
                //         Indigox.Common.Logging.Log.Debug( "Root Xml: " + xdoc.InnerXml );
                //     }
                // }

                #endregion debug code

                //using ( XmlReader xmlReader = new XIncludingReader( path ) )
                using ( XmlReader xmlReader = new XmlTextReader( path ) )
                {
                    if ( ReadToNode( xmlReader, xmlRoot ) )
                    {
                        XmlReader subXmlReader = xmlReader.ReadSubtree();
                        XmlSerializer serializer = new XmlSerializer( typeof( T ), new XmlRootAttribute( xmlReader.Name ) );
                        section = (T)serializer.Deserialize( subXmlReader );
                    }
                    else
                    {
                        throw new Exception( "找不到根节点: " + xmlRoot );
                    }
                }
            }
            catch ( Exception ex )
            {
                Exception warppedEx = new ApplicationException( "加载配置文件失败！", ex );
                Indigox.Common.Logging.Log.Error( warppedEx.ToString() );
                throw warppedEx;
            }

            this.SetConfig( section );
        }

        private bool ReadToNode( XmlReader xmlReader, string xmlRoot )
        {
            string[] nodes = xmlRoot.Split( '/' );
            foreach ( string node in nodes )
            {
                if ( !xmlReader.ReadToDescendant( node ) )
                {
                    return false;
                }
            }
            return true;
        }

        public void LoadFromXmlFile( string path )
        {
            this.LoadFromXMLFile( path, "config" );
        }

        public abstract void Configure();

        protected string GetConfigFileFullPath( string path )
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