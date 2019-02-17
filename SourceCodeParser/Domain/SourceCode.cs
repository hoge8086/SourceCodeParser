using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SourceCodeParser.Domain.Common;
using SourceCodeParser.Domain.SourceCodeParser;
namespace SourceCodeParser.Domain
{

    public class FunctionSummary
    {
        public string Definition { get; private set; }
        public string Comment { get; private set; }
        public FunctionSummary(string definition, string comment)
        {
            Definition = definition;
            Comment = comment;
        }
        public override string ToString()
        {
            string str = string.Empty;
            str += "---------------------\n";
            str += "■" + Definition + "\n";
            str += "---------------------\n";
            str += Comment + "\n";
            return str;
        }
    }

    public class SourceCode
    {
        public string Path { get; private set; }
        public List<string> Lines { get; private set; }
        public List<Comment> Comments { get; private set; }
        public List<Function> Functions { get; private set; }
        public Modifications Modifications { get; private set; }

        public SourceCode(
            string path,
            string code,
            List<Comment> comments,
            List<Function> functions,
            List<LineRange> modificationRange)
        {
            Path = path;
            Lines = code.Split('\n').Select(l => l + "\n").ToList();
            Comments = comments;
            Functions = functions;
            Modifications = new Modifications(modificationRange);
        }

        public List<FunctionSummary> ModifiedFunctionSummary()
        {
            return new List<FunctionSummary>(
                    Functions.Where(f => Modifications.IsModified(f.Range))
                    .Select(f => new FunctionSummary(f.Definition, FindFunctionComment(f.Range.Begin))));
        }

        private string FindFunctionComment(int startFunctionLineNum)
        {
            if (startFunctionLineNum < 0)
                return string.Empty;

            var comment = Comments.FirstOrDefault(c => c.Range.IsIn(startFunctionLineNum - 1));
            if(comment == null)
                return string.Empty;

            return FindFunctionComment(comment.Range.Begin) + comment.Text;
        }


        public override string ToString()
        {
            string text = string.Empty;
            //text += "対象ファイル:" + Path + "\n";
            //text += "------------[関数一覧]-------------\n";
            text += string.Join("\n", Functions.Select(f => f.ToString())) + "\n";
            //text += "\n------------[コメント一覧]-------------\n";
            //text += string.Join("\n", Comments.Select(c => c.ToString()));
            return text;
        }
    }
}
