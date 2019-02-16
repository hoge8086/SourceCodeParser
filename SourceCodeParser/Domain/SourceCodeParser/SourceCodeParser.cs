using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;

using SourceCodeParser.Domain.Common;
namespace SourceCodeParser.Domain.SourceCodeParser
{
    public class SourceCodeParser
    {
        [DataContract]
        public class Setting
        {
            [DataMember]
            public List<string> TargetExtensions{ get; set; }
            [DataMember]
            public List<string> CommentAndIgnoreRegexp { get; set; }
            [DataMember]
            public List<string> FunctionRegexp { get; set; }
            [DataMember]
            public string FunctionBeginMarker { get; set; }
            [DataMember]
            public string FunctionEndMarker { get; set; }

            public Setting(
                List<string> commentAndIgnoreRegexp,
                List<string> functionRegexps,
                string functionBeginMarker,
                string functionEndMarker)
            {
                CommentAndIgnoreRegexp = commentAndIgnoreRegexp;
                FunctionRegexp = functionRegexps;
                FunctionBeginMarker = functionBeginMarker;
                FunctionEndMarker = functionEndMarker;
            }

        }
        private Setting setting;

        public SourceCodeParser(Setting setting)
        {
            this.setting = setting;
        }

        /// <summary>
        /// 改行は保持したまｍ、コメントと無視対象の文字列を削除する
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string RemoveCommentAndIngore(string source)
        {
            string sourceWithoutComentAndIgnore = string.Empty;
            int currentIndex = 0;

            var comments = new List<Comment>();
            Regex r = new Regex(string.Join("|", setting.CommentAndIgnoreRegexp), System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            for(Match m = r.Match(source); m.Success; m = m.NextMatch())
            {
                sourceWithoutComentAndIgnore += source.Substring(currentIndex, m.Index - currentIndex);
                sourceWithoutComentAndIgnore += Regex.Replace(m.Value, @"[^\r\n]", "");
                currentIndex = m.Index + m.Value.Length;
            }
            return sourceWithoutComentAndIgnore + source.Substring(currentIndex);
        }

        public List<Comment> ParseComments(string code)
        {
            var comments = new List<Comment>();
            Regex r = new Regex(string.Join("|", setting.CommentAndIgnoreRegexp), System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            for(Match m = r.Match(code); m.Success; m = m.NextMatch())
            {
                if(m.Groups["comment"] != null)
                {
                    var codeRange = new LineRange(
                        CountLine(code.Substring(0, m.Index)),
                        CountLine(m.Value));
                    comments.Add(new Comment(codeRange, m.Groups["comment"].Value));
                }
            }
            return comments;
        }

        public List<Function> ParseFunctions(string code)
        {
            code = RemoveCommentAndIngore(code);

            //System.Diagnostics.Debug.WriteLine("-----------------------------");
            //System.Diagnostics.Debug.WriteLine(source);

            var functions = new List<Function>();
            Regex r = new Regex(string.Join("|", setting.FunctionRegexp), System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            for(Match m = r.Match(code); m.Success; m = m.NextMatch())
            {
                var range = new LineRange(
                        CountLine(code.Substring(0, m.Index)),
                        CountLine(ExtractFunctionCode(code.Substring(m.Index))));
                functions.Add(new Function(range, m.Value));
            }
            return functions;
        }

        private string ExtractFunctionCode(string code)
        {
            int nest = 0;
            for(int i=0; i<code.Length; i++)
            {
                if (code.Substring(i).StartsWith(setting.FunctionBeginMarker))
                    nest++;

                if (code.Substring(i).StartsWith(setting.FunctionEndMarker))
                {
                    nest--;
                    if (nest <= 0)
                        return code.Substring(0, i + 1);
                }
            }
            return code;
        }

        private int CountLine(string text)
        {
            return text.Count( c => c == '\n' ) + 1;
        }
    }
}
