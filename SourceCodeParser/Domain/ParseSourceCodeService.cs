using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeParser.Domain
{
    public class ParseSourceCodeService
    {
        private ITextFileReader textFileReader;
        private IParserFactory parserFactory;

        public ParseSourceCodeService(
            ITextFileReader textFileReader,
            IParserFactory parserFactory)
        {
            this.textFileReader = textFileReader;
            this.parserFactory = parserFactory;
        }

        public SourceCode ParseSourceFile(string path)
        {
            if(!System.IO.File.Exists(path))
                throw new ArgumentException("not found file.");

            var source = textFileReader.Read(path);
            var parser = parserFactory.createParser(path);
            if (parser == null)
                return null;

            return parser.Parse(path, source);
        }

        public List<SourceCode> ParseSourceFiles(string[] paths)
        {
            List<SourceCode> sources = new List<SourceCode>();
            foreach(var path in paths)
            {
                var source = ParseSourceFile(path);
                if(source != null)
                    sources.Add(source);
            }
            return sources;
        }

        public List<SourceCode> ParseAllFiles(string directoryPath)
        {
            if (!System.IO.Directory.Exists(directoryPath))
                throw new ArgumentException("not found directory.");

            var paths = System.IO.Directory.GetFiles(directoryPath, "*", System.IO.SearchOption.AllDirectories);
            return ParseSourceFiles(paths);
        }
    }
}
