
namespace Indigox.Common.EventBus.Test.EventBus.ClockSample
{
    class Counter
    {

        private int count = 0;

        public int Count
        {
            get { return count; }
        }

        public void Increase ()
        {
            count++;
        }

        public void Reset ()
        {
            count = 0;
        }
    }
}
