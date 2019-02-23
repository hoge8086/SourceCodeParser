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
        public string Path { get; private set; }
        public string Definition { get; private set; }
        public string Comment { get; private set; }
        public FunctionSummary(string path, string definition, string comment)
        {
            Path = path;
            Definition = definition;
            Comment = comment;
        }
        public override string ToString()
        {
            string str = string.Empty;
            str += "---------------------\n";
            str += "パス: " + Path + "\n";
            str += "定義: " + Definition + "\n";
            str += "概要: " + Comment;
            return str;
        }
    }

    public class SourceCode
    {
        public string Path { get; private set; }
        public List<string> Lines { get; private set; }
        public List<Comment> Comments { get; private set; }
        public List<Function> Functions { get; private set; }
        //public Modifications Modifications { get; private set; }

        public SourceCode(
            string path,
            string code,
            List<Comment> comments,
            List<Function> functions)
            //List<LineRange> modificationRange)
        {
            Path = path;
            Lines = code.Split('\n').Select(l => l + "\n").ToList();
            Comments = comments;
            Functions = functions;
        }

        public List<FunctionSummary> FunctionSummary()
        {
            return CreateFunctionSummary(Functions);
        }

        public List<FunctionSummary> FunctionSummary(Modifications modifications)
        {
            return CreateFunctionSummary(Functions.Where(f => modifications.IsModified(f.Range)).ToList());
        }

        private List<FunctionSummary> CreateFunctionSummary(List<Function> functions)
        {
            return functions.Select(f => new FunctionSummary(Path, f.Definition, FindFunctionComment(f.Range.Begin) ?? "")).ToList();
        }

        private string FindFunctionComment(int startFunctionLineNum)
        {
            if (startFunctionLineNum < 0)
                return null;

            var comment = Comments.FirstOrDefault(c => c.Range.IsIn(startFunctionLineNum - 1));
            if(comment == null)
                return null;

            var upperComment = FindFunctionComment(comment.Range.Begin);
            if(upperComment == null)
                return comment.Text;

            return upperComment + "\n" + comment.Text;
        }

        public override string ToString()
        {
            string text = string.Empty;
            text += string.Join("\n", Functions.Select(f => f.ToString())) + "\n";
            return text;
        }
    }
}
