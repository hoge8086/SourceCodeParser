using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SourceCodeParser.Domain.Common;

namespace SourceCodeParser.Domain.SourceCodeParser
{
    public class SourceCodeParser
    {
        public Setting Setting { get; }
        private List<IFunctionChecker> functionCheckers;

        public SourceCodeParser(Setting setting)
        {
            this.Setting = setting;
            functionCheckers = setting.CreateFunctionCheckers();
        }

        /// <summary>
        /// 改行は保持したまま、コメントと無視対象の文字列を削除する
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string RemoveCommentAndIngore(string source)
        {
            string sourceWithoutComentAndIgnore = string.Empty;
            int currentIndex = 0;

            var comments = new List<Comment>();
            Regex r = new Regex(string.Join("|", Setting.CommentAndIgnoreRegexp), System.Text.RegularExpressions.RegexOptions.IgnoreCase);

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
            Regex r = new Regex(string.Join("|", Setting.CommentAndIgnoreRegexp), System.Text.RegularExpressions.RegexOptions.IgnoreCase);

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
            Regex r = new Regex(string.Join("|", Setting.FunctionRegexp), System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            for(Match m = r.Match(code); m.Success; m = m.NextMatch())
            {
                if(IsFucntion(m.Value))
                {
                    var range = new LineRange(
                            CountLine(code.Substring(0, m.Index)),
                            CountLine(ExtractFunctionCode(code.Substring(m.Index))));
                    functions.Add(new Function(range, m.Value));
                }
            }
            return functions;
        }

        private bool IsFucntion(string functionDefinition)
        {
            return functionCheckers.All(c => c.check(functionDefinition));
        }

        /// <summary>
        /// 関数ブロックを抽出する
        /// ※IndexOf()による性能改善を実施(2019/2/24)
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private string ExtractFunctionCode(string code)
        {
            int nest = 0;
            for(int i=0; i<code.Length;)
            {
                int begin = code.IndexOf(Setting.FunctionBeginMarker, i);
                int end = code.IndexOf(Setting.FunctionEndMarker, i);

                if (end < 0)    //終了文字が見つからないことはありえない
                    break;

                if(begin > 0 && begin < end)    //開始文字が終了文字より先に見つかった場合
                {
                    nest++;
                    i = begin + Setting.FunctionBeginMarker.Length;
                }
                else    //終了文字が開始文字より先に見つかった、あるいは終了文字しか見つからない場合
                {
                    nest--;
                    i = end + Setting.FunctionEndMarker.Length;
                    if (nest <= 0)
                        return code.Substring(0, i);
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
