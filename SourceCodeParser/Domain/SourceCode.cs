using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SourceCodeParser.Domain
{
    public class StringUtil
    {
        public static string TrimWhiteSpace(string text)
        {
            text = Regex.Replace(text, @"^\s+", "");
            text = Regex.Replace(text, @"\s+$", "");
            return Regex.Replace(text, @"\s+", " ");
        }
    }

    public class Function
    {
        public string Definition { get; private set; }
        public LineRange Range { get; private set; }

        public Function(LineRange range, string definition)
        {
            Definition = StringUtil.TrimWhiteSpace(definition);
            Range = range;
        }

        public override string ToString()
        {
            return Definition + "(" + Range.ToString() + ")";
        }
    }

    public class Comment
    {
        public string Text { get; private set; }
        public LineRange Range { get; private set; }

        public Comment(LineRange range, string text)
        {
            Text = StringUtil.TrimWhiteSpace(text);
            Range = range;
        }

        public override string ToString()
        {
            return Text + "(" + Range.ToString() + ")";
        }
    }

    public class LineRange
    {
        public int StartLine { get; private set; }
        public int LineNum { get; private set; }

        public LineRange(
            int startLine,
            int lineNum)
        {
            StartLine = startLine;
            LineNum = lineNum;
        }

        public override string ToString()
        {
            return StartLine.ToString() + "-" + (StartLine + LineNum - 1).ToString();
        }

    }

    public class Modefication
    {
        public List<LineRange> ModefiedRange { get; private set; }

        public Modefication(List<LineRange> modefiedRange)
        {
            this.ModefiedRange = modefiedRange;
        }
    }

    public class SourceCode
    {
        public string Path { get; private set; }
        public List<string> Lines { get; private set; }
        public List<Comment> Comments { get; private set; }
        public List<Function> Functions { get; private set; }

        public SourceCode(
            string path,
            string code,
            List<Comment> comments,
            List<Function> functions)
        {
            Path = path;
            Lines = code.Split('\n').Select(l => l + "\n").ToList();
            Comments = comments;
            Functions = functions;
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
