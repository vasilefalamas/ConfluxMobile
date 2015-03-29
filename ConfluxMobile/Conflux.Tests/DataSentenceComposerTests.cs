using System;
using Conflux.Core.Models;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Conflux.Tests
{
    [TestClass]
    public class DataSentenceComposerTests
    {
        [TestMethod]
        public void WhenComposeBeginSentenceCalled_WithNoStartDate_ReturnsNull()
        {
            var sentence = DateSentenceComposer.ComposeBeginSentence(null);

            Assert.IsNull(sentence);
        }

        [TestMethod]
        public void WhenComposeBeginSentenceCalled_WithPastStartDate_ReturnsPastReferenceString()
        {
            var date = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0));

            var sentence = DateSentenceComposer.ComposeBeginSentence(date);

            var expectedString = string.Format("Started on {0}.", date);
            Assert.AreEqual(expectedString, sentence);
        }

        [TestMethod]
        public void WhenComposeBeginSentenceCalled_WithYesterdayStartDate_ReturnsStartedYesterday()
        {
            var date = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));

            var sentence = DateSentenceComposer.ComposeBeginSentence(date);

            var expectedString = string.Format("Has started yesterday.");
            Assert.AreEqual(expectedString, sentence);
        }

        [TestMethod]
        public void WhenComposeEndSentenceCalled_WithNoStartDate_ReturnsNull()
        {
            var sentence = DateSentenceComposer.ComposeEndSentence(null);

            Assert.IsNull(sentence);
        }
        
        [TestMethod]
        public void WhenComposeEndSentenceCalled_WithEndPastDate_ReturnsPastReferenceString()
        {
            var date = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0));

            var sentence = DateSentenceComposer.ComposeEndSentence(date);

            var expectedString = string.Format("Ended on {0}.", date);
            Assert.AreEqual(expectedString, sentence);
        }
    }
}
