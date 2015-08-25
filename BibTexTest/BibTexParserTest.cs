using BibTexLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BibTexTest
{
    [TestClass]
    public class BibTexParserTest
    {
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
        public void KnownIssueNoEntries()
        {
            string sample = "@inproceedings{                Schoeman: 2003:ACE: 954014.954020, author = {               Schoeman, Marthie and Cloete, Elsab{\'e}},         title = { Architectural Components for the Efficient Design of Mobile Agent Systems}, booktitle = { Proceedings of the 2003 Annual Research Conference of the South African Institute of Computer Scientists and Information Technologists on Enablement Through Technology}, series = {       SAICSIT '03}, year = { 2003},isbn = { 1 - 58113 - 774 - 5},location = { Johannesburg, South Africa},pages = { 48--58}, numpages = { 11}, url = {                            http://0-dl.acm.org.ujlink.uj.ac.za/citation.cfm?id=954014.954020},                            acmid = { 954020}, publisher = { South African Institute for Computer Scientists and Information Technologists}, address = { Republic of South Africa}, keywords = { design, mobile agent systems, software architecture model, standardisation},}";

            BibTexParser parser = new BibTexParser();

            var output = parser.Parse(sample);

            Assert.IsTrue(output.Entries.Count == 1, "Entry number is incorrect");
            Assert.AreEqual<string>("Schoeman: 2003:ACE: 954014.954020", output.Entries[0].InternalKey);
        }

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
    }
}