using BibTexLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BibTexTest
{
    [TestClass]
    public class BibTexParserTest
    {
        [TestMethod]
        public void SampleBibParseTest()
        {
            string sample = "%  a sample bibliography file% @article{small,author = { Freely, I.P.},title = { A small paper},journal = { The journal of small papers},year = 1997,volume = { -1},note = { to appear},}   @article{                big,author = { Jass, Hugh},title = { A big paper},journal = { The journal of big papers},year = 7991,volume = { MCMXCVII},}% The authors mentioned here are almost, but not quite,% entirely unrelated to Matt Groening.";

            BibTexParser parser = new BibTexParser();

            var output = parser.Parse(sample);
        }

        [TestMethod]
        public void SampleCompleteMultipleEntryBibTexTest()
        {
        }

        [TestMethod]
        public void SampleCompleteSingleEntryBibTexTest()
        {
        }

        [TestMethod]
        public void SampleMultipleBibTexTest()
        {
        }

        [TestMethod]
        public void SampleSingleBibTexTest()
        {
        }

        [TestMethod]
        public void BraceCheckingTestSingleBrace()
        {
            string teststring = "{This is the content}";
            string expectedString = "This is the content";

            string actualstring = "";

            BibTexParser parser = new BibTexParser();

            actualstring = parser.GetContentBetweenOpenCloseBraces(teststring, '{', '}');

            Assert.AreEqual(expectedString, actualstring);
        }

        [TestMethod]
        public void BraceCheckingTestMultipleBrace()
        {
            string teststring = "{This is {the} content}";
            string expectedString = "This is {the} content";

            string actualstring = "";

            BibTexParser parser = new BibTexParser();

            actualstring = parser.GetContentBetweenOpenCloseBraces(teststring, '{', '}');

            Assert.AreEqual(actualstring, expectedString);
        }
    }
}