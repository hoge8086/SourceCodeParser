using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceCodeParser.Domain.ModificationParser;
using SourceCodeParser.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeParser.Domain.ModificationParser.Tests
{
    [TestClass()]
    public class ModificationParserTests
    {
        [TestMethod()]
        public void ParseTest()
        {
            var detector = new SingleAndMultiLineModifiedBlockDetector("修正行", "▼", "▲");
            var reader = new TextFileReaderImpl();
            var parser = new ModificationParser(detector);
            var m = parser.Parse(reader.Read("modification.cpp"));
            Assert.IsTrue(m.RangeList.Count == 4);
            Assert.IsTrue(m.RangeList.Any(x => x.Begin == 6 & x.End == 8));
            Assert.IsTrue(m.RangeList.Any(x => x.Begin == 11 & x.End == 41));
            Assert.IsTrue(m.RangeList.Any(x => x.Begin == 51 & x.End == 51));
            Assert.IsTrue(m.RangeList.Any(x => x.Begin == 54 & x.End == 54));
        }
    }
}