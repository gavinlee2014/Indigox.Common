using Indigox.Common.EventBus.Interface.Event;

namespace Indigox.Common.EventBus.Test.EventBus.ClockSample
{
    internal class ClockAlarmEvent : IEvent
    {
        public ClockAlarmEvent ( bool snooze )
        {
            this.snooze = snooze;
        }

        public ClockAlarmEvent ( bool snooze, int times )
        {
            this.snooze = snooze;
            this.times = times;
        }

        bool snooze = false;
        int times = 1;

        public bool Snooze
        {
            get { return snooze; }
        }

        public int Times
        {
            get { return times; }
        }
    }
}
