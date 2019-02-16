using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeParser.Domain.ModificationParser
{
    public class SingleAndMultiLineModifiedBlockDetector : IModifiedBlockDetector
    {
        SingleLineModifiedBlockDetector single;
        MultiLineModifiedBlockDetector multi;

        public SingleAndMultiLineModifiedBlockDetector(
            string modificationKeyword,
            string beginKeyword,
            string endKeyword)
        {
            this.single = new SingleLineModifiedBlockDetector(modificationKeyword);
            this.multi = new MultiLineModifiedBlockDetector(
                    new string[] {beginKeyword, modificationKeyword}.ToList(),
                    new string[] {endKeyword, modificationKeyword}.ToList()
                );
        }

        public bool IsBeginLine(string line)
        {
            //注意:先に複数修正かをチェックする必要がある
            if (multi.IsBeginLine(line))
                return true;
            return single.IsMatch(line);
        }

        public bool IsEndLine(string line)
        {
            //注意:先に複数修正かをチェックする必要がある
            if (multi.IsEndLine(line))
                return true;
            return single.IsMatch(line);
        }
    }

    public class SingleLineModifiedBlockDetector : IModifiedBlockDetector
    {
        string keyword;
        public SingleLineModifiedBlockDetector(string keyword) { this.keyword = keyword;}
        public bool IsMatch(string line) { return line.Contains(keyword);}
        public bool IsBeginLine(string line) { return IsMatch(line); }
        public bool IsEndLine(string line) { return IsMatch(line); }
    }

    public class MultiLineModifiedBlockDetector : IModifiedBlockDetector
    {
        List<string> beginKeywords;
        List<string> endKeywords;
        public MultiLineModifiedBlockDetector(List<string> beginKeywords, List<string> endKeywords)
        {
            this.beginKeywords= beginKeywords;
            this.endKeywords = endKeywords;
        }
        private bool IsMatch(string line, List<string> keyWords) { return keyWords.All(kw => line.Contains(kw));}
        public bool IsBeginLine(string line) { return IsMatch(line, beginKeywords); }
        public bool IsEndLine(string line) { return IsMatch(line, endKeywords); }
    }
}
