using SourceCodeParser.Domain.Common;
namespace SourceCodeParser.Domain.SourceCodeParser
{
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
}
