using System;
using Conflux.Core.Extensions;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Conflux.Tests
{
    [TestClass]
    public class DateTimeExtensionsTests
    {
        [TestMethod]
        public void WhenIsYesterdayCalled_WithTodayDate_ReturnsFalse()
        {
            Assert.IsFalse(DateTime.Now.IsYesterday());
        }

        [TestMethod]
        public void WhenIsYesterdayCalled_WithYesterdayDate_ReturnsTrue()
        {
            var yesterday = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));
            
            Assert.IsTrue(yesterday.IsYesterday());
        }

        [TestMethod]
        public void WhenIsYesterdayCalled_WithTomorrowDate_ReturnsTrue()
        {
            var tomorrow = DateTime.Now.AddDays(1);

            Assert.IsFalse(tomorrow.IsYesterday());
        }

        [TestMethod]
        public void WhenIsTodayCalled_WithYesterdayDate_ReturnsFalse()
        {
            var yesterday = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));

            Assert.IsFalse(yesterday.IsToday());
        }

        [TestMethod]
        public void WhenIsTodayCalled_WithTodayDate_ReturnsTrue()
        {
            Assert.IsTrue(DateTime.Now.IsToday());
        }

        [TestMethod]
        public void WhenIsTodayCalled_WithTomorrowDate_ReturnsFalse()
        {
            var tomorrow = DateTime.Now.AddDays(1);
            
            Assert.IsFalse(tomorrow.IsToday());
        }

        [TestMethod]
        public void WhenIsTomorrowCalled_WithYesterdayDate_ReturnsFalse()
        {
            var yesterday = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));

            Assert.IsFalse(yesterday.IsTomorrow());
        }

        [TestMethod]
        public void WhenIsTomorrowCalled_WithTodayDate_ReturnsFalse()
        {
            Assert.IsFalse(DateTime.Now.IsTomorrow());
        }

        [TestMethod]
        public void WhenIsTomorrowCalled_WithTomorrowDate_ReturnsTrue()
        {
            var tomorrow = DateTime.Now.AddDays(1);

            Assert.IsTrue(tomorrow.IsTomorrow());
        }
    }
}
