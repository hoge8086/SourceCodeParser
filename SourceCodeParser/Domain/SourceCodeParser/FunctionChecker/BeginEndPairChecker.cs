namespace SourceCodeParser.Domain.SourceCodeParser.FunctionChecker
{
    public class BeginEndPairChecker : IFunctionChecker
    {
        string beginStr;
        string endStr;

        public BeginEndPairChecker(string beginStr, string endStr)
        {
            this.beginStr = beginStr;
            this.endStr = endStr;
        }

        public bool check(string functionDefinition)
        {
            int nest = 0;
            for(int i=0; i<functionDefinition.Length;)
            {
                int begin = functionDefinition.IndexOf(beginStr, i);
                int end = functionDefinition.IndexOf(endStr, i);
                if (end < 0 && begin < 0)
                    break;

                if((begin > 0) &&  (end < 0 || begin < end))
                {
                    nest++;
                    i = begin + beginStr.Length;
                }
                else
                {
                    nest--;
                    i = end + endStr.Length;
                }
            }
            return nest == 0;
        }
    }
}
