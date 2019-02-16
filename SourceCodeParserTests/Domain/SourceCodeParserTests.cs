﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class SourceCodeParserTests
    {
        private IParserFactory factory;
        private ITextFileReader reader;

        [TestInitialize]
        public void TestInitialize()
        {
            factory = new ParserFactoryImpl();
            reader = new TextFileReaderImpl();
        }

        [TestMethod()]
        public void ParseFunctionsTest()
        {
            var file = @"CEditView_Mouse.cpp";
            var parser = factory.createParser(file);
            var functions = parser.ParseFunctions(reader.Read(file));
            var expect = new TextFileReaderImpl().Read(@"result_CEditView_Mouse.txt");
            var result = string.Join("\n", functions);
            Assert.AreEqual(expect.Length, result.Length);
            Assert.AreEqual(expect, result);
        }
    }
}