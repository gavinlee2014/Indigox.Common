using System;
using Indigox.Common.EventBus;
using Indigox.Common.Membership.Events;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;

namespace Indigox.Common.Membership
{
    [Serializable]
    public class Group : Container, IGroup, IMutableGroup
    {
        public static IGroup GetGroupByID( string id )
        {
            return ProviderFactories.GetFactory().GetGroupProvider().GetGroupByID( id );
        }

        protected override void TriggerPropertyChangedEvent( string propertyName, object oldValue, object newValue )
        {
            //EventTrigger.Trigger( this, new GroupPropertyChangedEvent( this.ID, propertyName, oldValue, newValue ) );
        }

        protected override void TriggerRemovedFromEvent(IContainer container)
        {
        }

        protected override void TriggerAddedToEvent(IContainer container)
        {
        }
    }
}