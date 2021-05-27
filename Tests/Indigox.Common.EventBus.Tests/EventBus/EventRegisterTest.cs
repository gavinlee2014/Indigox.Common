using System.Collections.Generic;
using Indigox.Common.EventBus;
using Indigox.Common.EventBus.Test.EventBus.ClockSample;
using NUnit.Framework;

namespace Indigox.Common.EventBus.Test.EventBus
{
    [TestFixture]
    public class EventRegisterTest
    {
        [Test]
        public void TestRegister()
        {
            Sleeper sleeper = new Sleeper();
            EventRegister.Instance.Register(typeof(ClockAlarmEvent), typeof(Clock), sleeper, "Awake");

            IList<EventRegItem> eventItems = EventRegister.Instance.GetEventItem(typeof(ClockAlarmEvent), typeof(Clock));
            EventRegItem check = null;
            foreach (EventRegItem item in eventItems)
            {
                if (item.Listener.Equals(sleeper) && item.MethodName.Equals("Awake"))
                {
                    check = item;
                    break;
                }
            }
            Assert.NotNull(check);
        }

        [Test]
        public void TestUnregister()
        {
            Sleeper sleeper = new Sleeper();
            EventRegister.Instance.Register(typeof(ClockAlarmEvent), typeof(Clock), sleeper, "Awake");
            EventRegister.Instance.Unregister(typeof(ClockAlarmEvent), typeof(Clock), sleeper, "Awake");

            IList<EventRegItem> eventItems = EventRegister.Instance.GetEventItem(typeof(ClockAlarmEvent), typeof(Clock));
            EventRegItem check = null;
            foreach (EventRegItem item in eventItems)
            {
                if (item.Listener.Equals(sleeper) && item.MethodName.Equals("Awake"))
                {
                    check = item;
                    break;
                }
            }
            Assert.Null(check);
        }
    }
}
