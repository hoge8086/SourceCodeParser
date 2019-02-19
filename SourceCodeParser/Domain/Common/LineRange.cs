namespace SourceCodeParser.Domain.Common
{
    public class LineRange
    {
        public int Begin { get; private set; }
        public int End { get { return Begin + Num - 1; } }
        public int Num { get; private set; }

        public LineRange(
            int begin,
            int num)
        {
            Begin = begin;
            Num = num;
        }

        public bool Overlap(LineRange range)
        {
            if (IsIn(range.Begin))
                return true;
            if (IsIn(range.End))
                return true;
            if (range.IsIn(Begin))
                return true;
            return false;
        }

        public bool IsIn(int lineNum)
        {
            return (Begin <= lineNum && lineNum <= End);
        }
        public override string ToString()
        {
            return Begin.ToString() + "-" + (Begin + Num - 1).ToString();
        }


    }
}
