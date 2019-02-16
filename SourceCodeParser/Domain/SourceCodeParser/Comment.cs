using SourceCodeParser.Domain.Common;
namespace SourceCodeParser.Domain.SourceCodeParser
{
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
}
