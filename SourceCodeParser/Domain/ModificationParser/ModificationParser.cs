using System.Collections.Generic;
using System.Linq;

using SourceCodeParser.Domain.Common;
namespace SourceCodeParser.Domain.ModificationParser
{
    public class ModificationParser
    {
        //修正ブロックがネストする場合を考慮する
        class CurrentModifiedBlock
        {
            int nest;
            int beginLine;

            public CurrentModifiedBlock() { Reset(); }
            public void Reset() { nest = 0; beginLine = -1; }
            private bool IsInBlock() { return nest > 0; }

            public void Begin(int beginLine)
            {
                if (!IsInBlock())
                    this.beginLine = beginLine;
                nest++;
            }
            public bool End()
            {
                if(!IsInBlock())
                    return false;

                nest--;
                return !IsInBlock();
            }

            public LineRange GetRange(int endLine)
            {
                if (beginLine < 0)
                    return null;
                return new LineRange(beginLine, endLine - beginLine + 1);
            }
        }

        private IModifiedBlockDetector detector;

        public ModificationParser(IModifiedBlockDetector detector)
        {
            this.detector = detector;
        }

        public Modifications Parse(string code)
        {
            var rangeList = new List<LineRange>();
            var currentBlock = new CurrentModifiedBlock();

            List<string> lines = code.Split('\n').ToList();
            for(int i=0; i<lines.Count; i++)
            {
                if(detector.IsBeginLine(lines[i]))
                {
                    currentBlock.Begin(i+1);
                }
                
                // 注意：修正行が1行のみの場合があるので、else-ifではない
                if(detector.IsEndLine(lines[i]))
                {
                    if(currentBlock.End())
                    {
                        rangeList.Add(currentBlock.GetRange(i+1));
                        currentBlock.Reset();
                    }
                }
            }
            return new Modifications(rangeList);
        }
    }
}
