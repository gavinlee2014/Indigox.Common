using Indigox.Common.EventBus;
using Indigox.Common.EventBus.Test.EventBus.ClockSample;
using NUnit.Framework;

namespace Indigox.BPM.Test.EventBus
{
    [TestFixture]
    public class EventTriggerTest
    {
        #region SetUp and TearDown methods

        /// <summary>
        /// run before each test method
        /// </summary>
        [SetUp]
        public void SetUp ()
        {
            clockSleeper.AwakeInvokeCounter.Reset();
        }

        /// <summary>
        /// run after each test method
        /// </summary>
        [TearDown]
        public void TearDown ()
        {

        }

        /// <summary>
        /// run before test fixture start
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp ()
        {
            EventRegister.Instance.Register( typeof( ClockAlarmEvent ), typeof( Clock ), clockSleeper, "Awake" );
        }

        /// <summary>
        /// run after test fixture quit
        /// </summary>
        [TestFixtureTearDown]
        public void TestFixtureTearDown ()
        {

        }

        #endregion

        private Sleeper clockSleeper = new Sleeper();

        [Test]
        public void TestTrigger ()
        {
            Clock clock = new Clock();

            EventTrigger.Trigger( clock, new ClockAlarmEvent( false ) );
            Assert.AreEqual( 1, clockSleeper.AwakeInvokeCounter.Count );

            EventTrigger.Trigger( clock, new ClockAlarmEvent( true, 1 ) );
            Assert.AreEqual( 2, clockSleeper.AwakeInvokeCounter.Count );

            EventTrigger.Trigger( clock, new ClockAlarmEvent( true, 2 ) );
            Assert.AreEqual( 3, clockSleeper.AwakeInvokeCounter.Count );

        }
    }
}
