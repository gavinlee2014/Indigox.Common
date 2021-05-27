using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Indigox.Common.StateContainer.Configuration;

namespace Indigox.Common.StateContainer.StateContexts
{
    class ListenerManager
    {
        private static ListenerManager instance;

        public static ListenerManager Instance
        {
            get { return ListenerManager.instance; }
        }

        static ListenerManager()
        {
            instance = new ListenerManager();

            XmlConfigurator configurator = new XmlConfigurator( "config\\StateContainer.xml" );
            configurator.Configure();
        }

        private IList<IStateContextListener> listeners = new List<IStateContextListener>();

        public void Register( IStateContextListener listener )
        {
            listeners.Add( listener );
        }

        public void Clear()
        {
            listeners.Clear();
        }

        public IList<IStateContextListener> Listeners
        {
            get { return new ReadOnlyCollection<IStateContextListener>( listeners ); }
        }
    }
}
