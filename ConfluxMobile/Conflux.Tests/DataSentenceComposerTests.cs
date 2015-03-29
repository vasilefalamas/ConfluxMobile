using System;
using Conflux.Core.Models;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Conflux.Tests
{
    [TestClass]
    public class DataSentenceComposerTests
    {
        [TestMethod]
        public void WhenComposeBeginSentenceCalled_WithNoDate_ReturnsNull()
        {
            var sentence = DateSentenceComposer.ComposeBeginSentence(null);

            Assert.IsNull(sentence);
        }

        [TestMethod]
        public void WhenComposeBeginSentenceCalled_WithPastDate_ReturnsStartOnAndPastReferenceString()
        {
            var pastStartDate = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0));

            var sentence = DateSentenceComposer.ComposeBeginSentence(pastStartDate);

            var expectedString = string.Format("Starts on {0}.", pastStartDate);
            Assert.AreEqual(expectedString, sentence);
        }

        [TestMethod]
        public void WhenComposeBeginSentenceCalled_WithYesterdayDate_ReturnsHasStartedYesterdayString()
        {
            var yesterdayStartDate = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));

            var sentence = DateSentenceComposer.ComposeBeginSentence(yesterdayStartDate);

            var expectedString = string.Format("Has started yesterday.");
            Assert.AreEqual(expectedString, sentence);
        }

        [TestMethod]
        public void WhenComposeBeginSentenceCalled_WithTomorrowDate_ReturnsWillStartTomorrowString()
        {
            var tomorrowStartDate = DateTime.Now.AddDays(1);

            var sentence = DateSentenceComposer.ComposeBeginSentence(tomorrowStartDate);

            var expectedString = string.Format("Will start tomorrow.");
            Assert.AreEqual(expectedString, sentence);
        }

        [TestMethod]
        public void WhenComposeEndSentenceCalled_WithNoDate_ReturnsNull()
        {
            var sentence = DateSentenceComposer.ComposeEndSentence(null);

            Assert.IsNull(sentence);
        }
        
        [TestMethod]
        public void WhenComposeEndSentenceCalled_WithPastDate_ReturnsEndsOnAndPastReferenceString()
        {
            var pastEndDate = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0));

            var sentence = DateSentenceComposer.ComposeEndSentence(pastEndDate);

            var expectedString = string.Format("Ends on {0}.", pastEndDate);
            Assert.AreEqual(expectedString, sentence);
        }

        [TestMethod]
        public void WhenComposeEndSentenceCalled_WithYesterdayDate_ReturnsHasEndedYesterdayString()
        {
            var yesterdayEndDate = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));

            var sentence = DateSentenceComposer.ComposeEndSentence(yesterdayEndDate);

            var expectedString = string.Format("Has ended yesterday.");
            Assert.AreEqual(expectedString, sentence);
        }

        [TestMethod]
        public void WhenComposeEndSentenceCalled_WithTomorrowDate_ReturnsWillEndTomorrowString()
        {
            var tomorrowEndDate = DateTime.Now.AddDays(1);

            var sentence = DateSentenceComposer.ComposeEndSentence(tomorrowEndDate);

            var expectedString = string.Format("Will end tomorrow.");
            Assert.AreEqual(expectedString, sentence);
        }
    }
}
