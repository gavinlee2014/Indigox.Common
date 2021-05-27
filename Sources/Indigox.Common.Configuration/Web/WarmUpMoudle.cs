using System;
using System.Web;
using Indigox.Common.Configuration.Web.Configuration;
using Indigox.Common.Logging;

namespace Indigox.Common.Configuration.Web
{
    internal class WarmUpMoudle : IHttpModule
    {
        private static bool WarmUpped = false;
        private static object WarmUpLocker = new object();
        private static int timers = 0;

        private void WarmUp()
        {
            Log.Debug( "Actual WarmUp beign on times: " + ( timers ) );
            foreach ( WarmUpElement element in WarmUpSection.Default.WarmUpElements )
            {
                IWarmUp entry = GetWarmUpEntry( element );
                Log.Debug( "warmup [" + element.Name + "] started." );
                entry.OnApplicationStart();
                Log.Debug( "warmup [" + element.Name + "] finished." );
            }
        }

        private IWarmUp GetWarmUpEntry( WarmUpElement element )
        {
            Type t = Type.GetType( element.TypeName, true );
            if ( typeof( IWarmUp ).IsAssignableFrom( t ) )
            {
                return (IWarmUp)Activator.CreateInstance( t );
            }
            else
            {
                throw new ArgumentException( string.Format( "类型 {0} 不是 IWarmUp 的实现类。", element.TypeName ) );
            }
        }

        public void Dispose()
        {
        }

        public void Init( HttpApplication app )
        {
            Log.Debug( "WarmUp Module Inited: " + ( ++timers ) );
            if ( !WarmUpped )
            {
                lock ( WarmUpLocker )
                {
                    if ( !WarmUpped )
                    {
                        WarmUp();
                    }
                    WarmUpped = true;
                }
            }
        }
    }
}