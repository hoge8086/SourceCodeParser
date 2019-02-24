using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SourceCodeParser.Domain.Common;
using SourceCodeParser.Domain.SourceCodeParser;
using SourceCodeParser.Domain.ModificationParser;
namespace SourceCodeParser.Domain
{
    public class ParseSourceCodeService
    {
        private ITextFileReader textFileReader;
        private SourceCodeFactory sourceFactory;
        private IOutputModifiedFunctions output;

        public ParseSourceCodeService(
            ITextFileReader textFileReader,
            IOutputModifiedFunctions output,
            IParserFactory parserFactory)
            //IModifiedBlockDetector modificationDetector)
        {
            this.textFileReader = textFileReader;
            this.output = output;
            this.sourceFactory = new SourceCodeFactory(textFileReader, parserFactory);
        }

        public List<FunctionSummary> ParseSourceFile(string path, IModifiedBlockDetector modificationDetector)
        {
            if(!System.IO.File.Exists(path))
                throw new ArgumentException("not found file.");

            var source = this.sourceFactory.Create(path);
            if (source == null)
                return null;

            if(modificationDetector != null)
            {
                var modificationParser = new ModificationParser.ModificationParser(modificationDetector);
                var code = textFileReader.Read(path);
                var modification = modificationParser.Parse(code);
                return source.FunctionSummary(modification);
            }

            return source.FunctionSummary();
        }


        public List<FunctionSummary> ParseSourceFiles(string[] paths, IModifiedBlockDetector modificationDetector)
        {
            List<FunctionSummary> functions = new List<FunctionSummary>();
            foreach(var path in paths)
            {
                var funcs = ParseSourceFile(path, modificationDetector);
                if (funcs != null)
                    functions.AddRange(funcs);
            }
            return functions;
        }

        //public List<FunctionSummary> ParseAllFilesInDirectory(string directoryPath)
        //{
        //    if (!System.IO.Directory.Exists(directoryPath))
        //        throw new ArgumentException("not found directory.");

        //    var paths = System.IO.Directory.GetFiles(directoryPath, "*", System.IO.SearchOption.AllDirectories);
        //    return ParseSourceFiles(paths);
        //}

        public void OutputFunctions(string[] paths, IModifiedBlockDetector modificationDetector = null)
        {
            var functions = ParseSourceFiles(paths, modificationDetector);
            output.Write(functions);
        }
    }
}
