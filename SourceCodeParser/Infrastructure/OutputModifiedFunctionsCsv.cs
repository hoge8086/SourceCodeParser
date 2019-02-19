using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SourceCodeParser.Domain;
namespace SourceCodeParser.Infrastructure
{
    public class OutputModifiedFunctionsCsv : IOutputModifiedFunctions
    {
        private string path;

        public OutputModifiedFunctionsCsv(string path)
        {
            this.path = path;
        }

        public void Write(List<FunctionSummary> functions)
        {
            using (System.IO.StreamWriter sr = new System.IO.StreamWriter(path, false, Encoding.GetEncoding("Shift_JIS")))
            {
                sr.WriteLine("フォルダ名,ファイル名,関数宣言,関数コメント");
                for (int i = 0; i < functions.Count; i++)
                {
                    var f = functions[i];
                    var line = string.Format("{0},{1},{2},{3}",
                                    CreateCsvField(Path.GetDirectoryName(f.Path)),
                                    CreateCsvField(Path.GetFileNameWithoutExtension(f.Path)),
                                    CreateCsvField(f.Definition),
                                    CreateCsvField(f.Comment));
                    sr.WriteLine(line);
                }
            }
        }
        public string CreateCsvField(string value)
        {
            return "\"" +value.Replace("\"", "\"\"").Replace("\r", "") + "\"";
        }

    }
}
