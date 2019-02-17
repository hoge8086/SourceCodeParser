using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SourceCodeParser.Domain.SourceCodeParser;
using SourceCodeParser.Domain.ModificationParser;

namespace SourceCodeParser.Domain
{
    public class SourceCodeFactory
    {
        private ITextFileReader textFileReader;
        private IParserFactory sourceParserFactory;
        private ModificationParser.ModificationParser modificationParser;

        public SourceCodeFactory(
            ITextFileReader textFileReader,
            IParserFactory parserFactory,
            ModificationParser.ModificationParser modificationParser)
        {
            this.textFileReader = textFileReader;
            this.sourceParserFactory = parserFactory;
            this.modificationParser = modificationParser;
        }

        public SourceCode Create(string path)
        {
            var code = textFileReader.Read(path);
            var sourceParser = sourceParserFactory.createParser(path);
            return new SourceCode(
                    path,
                    code,
                    sourceParser.ParseComments(code),
                    sourceParser.ParseFunctions(code),
                    modificationParser.Parse(code));
        }
    }
}
