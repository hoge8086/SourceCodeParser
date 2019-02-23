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
        static void Main()
        {
            try
            {
                var command = ParseCommand(Environment.GetCommandLineArgs());
                command.Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        class ConsoleOutput : IOutputModifiedFunctions
        {
            public void Write(List<FunctionSummary> functions)
            {
                Console.WriteLine(string.Join("\n", functions));
            }
        }

        public class Command
        {
            private string directoryPath;
            private string keyword;
            private IOutputModifiedFunctions output;

            public Command(
                string directoryPath,
                string keyword,
                IOutputModifiedFunctions output)
            {
                this.directoryPath = directoryPath;
                this.keyword = keyword;
                this.output = output;
            }

            public void Execute()
            {
                var service = new ParseSourceCodeService(
                        new TextFileReaderImpl(),
                       output,
                        new ParserFactoryImpl());

                if (!System.IO.Directory.Exists(directoryPath))
                    throw new Exception("not found directory.");

                var paths = System.IO.Directory.GetFiles(directoryPath, "*", System.IO.SearchOption.AllDirectories);

                IModifiedBlockDetector detector = null;
                if (keyword != null)
                    detector = new SingleAndMultiLineModifiedBlockDetector("▼", "▲", keyword);

                service.OutputFunctions(paths, detector);
            }

        }

        static Command ParseCommand(string[] args)
        {
            string directoryPath = "";
            string keyword = null;
            IOutputModifiedFunctions output = new ConsoleOutput();

            int i;
            for(i = 1; i<args.Length; i++)
            {
                if (args[i].Equals("/csv", StringComparison.OrdinalIgnoreCase))
                {
                    i++;
                    if (i < args.Length)
                        output = new OutputModifiedFunctionsCsv(args[1]);
                    else
                        InvalidCommand("出力ファイル名を引数で与えてください.", args[0]);
                }
                else if (args[i++].Equals("/keyword", StringComparison.OrdinalIgnoreCase))
                {
                    i++;
                    if (i < args.Length)
                        keyword = args[2];
                    else
                        InvalidCommand("修正コメントのキーワードを引数で与えてください.", args[0]);
                }
                else
                {
                    break;
                }
            }

            if(i == args.Length - 1)
            {
                directoryPath = args[i];

            }else
            {
                InvalidCommand("ソースファイルが格納されたディレクトリを指定してください.", args[0]);
            }

            return new Command(directoryPath, keyword, output);
        }

        static private void InvalidCommand(string error, string exePath)
        {
            new FormatException(
                    string.Format(
                        "エラー: {0}\n" + 
                        "Usage: {1} [/csv <file_name>] [/keyword <keyword>] <source_file_directory_path>",
                        error, exePath));
        }
    }
}
