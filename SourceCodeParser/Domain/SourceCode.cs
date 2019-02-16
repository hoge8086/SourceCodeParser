using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SourceCodeParser.Domain.Common;
using SourceCodeParser.Domain.SourceCodeParser;
namespace SourceCodeParser.Domain
{

    public class SourceCode
    {
        public string Path { get; private set; }
        public List<string> Lines { get; private set; }
        public List<Comment> Comments { get; private set; }
        public List<Function> Functions { get; private set; }
        public Modefications Modefications { get; private set; }

        public SourceCode(
            string path,
            string code,
            List<Comment> comments,
            List<Function> functions,
            Modefications modefications)
        {
            Path = path;
            Lines = code.Split('\n').Select(l => l + "\n").ToList();
            Comments = comments;
            Functions = functions;
            Modefications = modefications;
            
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
