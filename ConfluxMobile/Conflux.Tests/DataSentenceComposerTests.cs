﻿using System;
using Conflux.Core.Models;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Conflux.Tests
{
    [TestClass]
    public class DataSentenceComposerTests
    {
        private const string DateFormat = "dd MMMM yyyy, dddd";

        [TestMethod]
        public void WhenComposeBeginSentenceCalled_WithPastDate_ReturnsStartOnAndPastReferenceString()
        {
            var pastStartDate = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0));

            var sentence = DateSentenceComposer.ComposeBeginSentence(pastStartDate);

            var expectedString = string.Format("Started on {0}.", pastStartDate.ToString(DateFormat));
            Assert.AreEqual(expectedString, sentence);
        }

        [TestMethod]
        public void WhenComposeBeginSentenceCalled_WithYesterdayDate_ReturnsHasStartedYesterdayString()
        {
            var yesterdayStartDate = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));

            var sentence = DateSentenceComposer.ComposeBeginSentence(yesterdayStartDate);

            var expectedString = string.Format("Started yesterday.");
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
        public void WhenComposeEndSentenceCalled_WithPastDate_ReturnsEndsOnAndPastReferenceString()
        {
            var pastEndDate = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0));

            var sentence = DateSentenceComposer.ComposeEndSentence(pastEndDate);

            var expectedString = string.Format("Ended on {0}.", pastEndDate.ToString(DateFormat));
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
