using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceCodeParser.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SourceCodeParser.Infrastructure;

namespace SourceCodeParser.Domain.Tests
{
    [TestClass()]
    public class ParseSourceCodeServiceTests
    {
        private ParseSourceCodeService service;

        [TestInitialize]
        public void TestInitialize()
        {
            service = new ParseSourceCodeService(new TextFileReaderImpl(), new ParserFactoryImpl());
        }
        [TestMethod()]
        public void ParseTest()
        {
            var source = service.ParseSourceFile(@"CEditView_Mouse.cpp");
            var expect = new TextFileReaderImpl().Read(@"result_CEditView_Mouse.txt");
            Assert.AreEqual(expect, source.ToString());
            //System.IO.File.WriteAllText(@".\out.txt", source.ToString());
        }
    }
}