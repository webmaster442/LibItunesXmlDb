using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webmaster442.LibItunesXmlDb.Internals;

namespace Tests
{
    [TestFixture]
    public class ParserTests
    {
        [TestCase("2:00", 120000L)]
        [TestCase("2:15", 135000L)]
        [TestCase("0:33", 33000L)]
        [TestCase("0:33", 33001L)]
        public void EnsureThat_Parser_MillisecondsToFormattedMinutesAndSeconds_Result_IsCorrect(string expected, long input)
        {
            var result =  Parser.MillisecondsToFormattedMinutesAndSeconds(input);
            Assert.AreEqual(expected, result);
        }

        [TestCase(null, "")]
        [TestCase(null, null)]
        [TestCase(@"C:\test.mp3", "file://localhost/C:/test.mp3")]
        [TestCase(@"C:\Bill Evans & Jim Hall\test.mp3", "file://localhost/C:/Bill%20Evans%20&#38;%20Jim%20Hall/test.mp3")]
        public void EnsureThat_Parser_UrlDecode_Result_IsCorrect(string expected, string input)
        {
            var result = Parser.UrlDecode(input);
            Assert.AreEqual(expected, result);
        }
    }
}
