using System;
using Indigox.Common.EventBus;
using Indigox.Common.Configuration;

namespace Indigox.Common.EventBus.Configuration
{
    public class BasicEventsConfigurator : Configurator<SourceElement>
    {
        public override void Configure()
        {
            foreach (SourceElement source in this)
            {
                foreach (EventElement event_ in source.Events)
                {
                    foreach (ListenerElement listener in event_.Listeners)
                    {
                        EventRegister.Instance.Register(
                                Type.GetType(event_.TypeName, true),
                                Type.GetType(source.TypeName, true),
                                SingletoneFactory.GetInstance(listener.TypeName),
                                listener.MethodName
                            );
                    }
                }
            }
        }

        protected override string GetKeyForItem(SourceElement item)
        {
            return Guid.NewGuid().ToString();
        }
    }
}
