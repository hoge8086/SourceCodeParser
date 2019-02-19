using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SourceCodeParser.Domain;
using SourceCodeParser.Infrastructure;
using SourceCodeParser.Domain.ModificationParser;

namespace ModifiedFunctionPicker
{
    class Program
    {
        class ConsoleOutput : IOutputModifiedFunctions
        {
            public void Write(List<FunctionSummary> functions)
            {
                Console.WriteLine(string.Join("\n", functions));
            }
        }

        static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();
            string keyword;
            string directoryPath;
            IOutputModifiedFunctions output;

            if(args.Length == 3)
            {
                keyword = args[1];
                directoryPath = args[2];
                output = new ConsoleOutput();

            }else if(args.Length == 5 && args[1].Equals("/csv", StringComparison.OrdinalIgnoreCase))
            {
                output = new OutputModifiedFunctionsCsv(args[2]);
                keyword = args[3];
                directoryPath = args[4];
            }
            else
            {
                Console.WriteLine("Usage: {0} [/csv <file_name>] <keyword> <source_file_directory_path>", args[0]);
                return;
            }

            try
            {

                var service = new ParseSourceCodeService(
                        new TextFileReaderImpl(),
                        output,
                        new ParserFactoryImpl(),
                        new SingleAndMultiLineModifiedBlockDetector(keyword, "▼", "▲"));

                if (!System.IO.Directory.Exists(directoryPath))
                {
                    Console.WriteLine("not found directory.");
                    return;
                }

                var paths = System.IO.Directory.GetFiles(directoryPath, "*", System.IO.SearchOption.AllDirectories);
                service.OutputModifiedFunctions(paths);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
