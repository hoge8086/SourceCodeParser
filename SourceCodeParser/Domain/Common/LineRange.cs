namespace SourceCodeParser.Domain.Common
{
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
}
