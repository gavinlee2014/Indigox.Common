using Indigox.Common.EventBus.Interface.Event;

namespace Indigox.Common.EventBus.Test.EventBus.ClockSample
{
    internal class Sleeper
    {
        public Counter awakeInvokeCounter = new Counter();

        public Counter AwakeInvokeCounter
        {
            get { return awakeInvokeCounter; }
        }

        public void Awake ( object sender, IEvent e )
        {
            ClockAlarmEvent clockAlarmEvent = e as ClockAlarmEvent;
            if ( clockAlarmEvent.Snooze )
            {
                System.Diagnostics.Debug.WriteLine( "This is an snooze alarm, this alarm is the {0} times," +
                    " it will realarm after 5 mins.",
                    GetOrdinalString( clockAlarmEvent.Times ) );
            }
            else
            {
                System.Diagnostics.Debug.WriteLine( "This is not an snooze alarm." );
            }
            AwakeInvokeCounter.Increase();
        }

        private string GetOrdinalString ( int number )
        {
            switch ( number )
            {
                case 1:
                    return "1st";
                case 2:
                    return "2nd";
                case 3:
                    return "3td";
                default:
                    return number.ToString() + "th";
            }
        }
    }
}
