using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceCodeParser.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using SourceCodeParser.Domain.ModificationParser;
using SourceCodeParser.Domain.SourceCodeParser;
using SourceCodeParser.Infrastructure;

namespace SourceCodeParser.Domain.Tests
{
    [TestClass()]
    public class SourceCodeTests
    {
        [TestMethod()]
        public void ModifiedFunctionSummaryTest()
        {
            var detector = new SingleAndMultiLineModifiedBlockDetector("修正行コメント", "▼", "▲");
            var factory = new SourceCodeFactory(
                            new TextFileReaderImpl(),
                            new ParserFactoryImpl());
                            //new ModificationParser.ModificationParser(detector));
            
            var source = factory.Create(@"CEditView_Mouse.cpp");
            var funcs = source.FunctionSummary();
            System.IO.File.WriteAllText("out.txt", string.Join("\n", funcs));

        }

        [TestMethod()]
        public void ModifiedFunctionSummaryTest2()
        {
            var detector = new SingleAndMultiLineModifiedBlockDetector("修正行", "▼", "▲");
            var reader = new TextFileReaderImpl();
            var parser = new ModificationParser.ModificationParser(detector);
            var m = parser.Parse(reader.Read("modification.cpp"));
            Assert.IsTrue(m.RangeList.Count == 4);
            Assert.IsTrue(m.RangeList.Any(x => x.Begin == 6 & x.End == 8));
            Assert.IsTrue(m.RangeList.Any(x => x.Begin == 11 & x.End == 41));
            Assert.IsTrue(m.RangeList.Any(x => x.Begin == 51 & x.End == 51));
            Assert.IsTrue(m.RangeList.Any(x => x.Begin == 54 & x.End == 54));
        }
    }
}