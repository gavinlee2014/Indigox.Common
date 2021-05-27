using System;
using System.Text.RegularExpressions;
using System.Web;
using Indigox.Common.Logging;
using Indigox.Common.StateContainer.CurrentUserProviders;

namespace Indigox.Common.StateContainer.Web
{
    public class StateContainerModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init( HttpApplication application )
        {
            application.AcquireRequestState += new EventHandler( OnAcquireRequestState );
            application.PostAcquireRequestState += new EventHandler( OnPostAcquireRequestState );

            // 注意：不要使用 BeginRequest 和 EndRequest，这两个事件不是一个线程
            application.PreRequestHandlerExecute += new EventHandler( OnBeginRequest );
            application.PostRequestHandlerExecute += new EventHandler( OnEndRequest );
        }

        static readonly Regex reg = new Regex( @".*\.(js|jpg(e)?|png|ico|gif|css)(\?.*)?" );

        void OnAcquireRequestState( object sender, EventArgs e )
        {
            Log.Debug("StateContainerModule OnAcquireRequestState " + HttpContext.Current.Request.Path);

            if ( reg.IsMatch( HttpContext.Current.Request.Path ) )
            {
                return;
            }

            try
            {
                Log.Debug("StateContainerModule OnAcquireRequestState start");
                if ( StateContext.Current.Application == null )
                {
                    Log.Debug("StateContainerModule OnAcquireRequestState should BeginApplication");
                    StateContext.Current.BeginApplication();
                }

                if ( HttpContext.Current.Session != null )
                {
                    Log.Debug("StateContainerModule OnAcquireRequestState should BeginSession");
                    if ( StateContext.Current.Session == null )
                    {
                        StateContext.Current.BeginSession( new WebCurrentUserProvider() );
                    }
                }
                Log.Debug("StateContainerModule OnAcquireRequestState end");
            }
            catch ( Exception ex )
            {
                Log.Error( ex.ToString() );
            }
        }

        void OnPostAcquireRequestState( object sender, EventArgs e )
        {
        }

        void OnBeginRequest( object sender, EventArgs e )
        {
            if ( reg.IsMatch( HttpContext.Current.Request.RawUrl ) )
            {
                return;
            }

            try
            {
                StateContext.Current.BeginTransaction();
            }
            catch ( Exception ex )
            {
                Log.Error( ex.ToString() );
            }
        }

        void OnEndRequest( object sender, EventArgs e )
        {
            if ( reg.IsMatch( HttpContext.Current.Request.RawUrl ) )
            {
                return;
            }

            try
            {
                StateContext.Current.EndTransaction();
            }
            catch ( Exception ex )
            {
                Log.Error( ex.ToString() );
            }
        }
    }
}
