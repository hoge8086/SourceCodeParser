using System.Text.RegularExpressions;

namespace SourceCodeParser.Domain.Common
{
    public class StringUtil
    {
        public static string TrimWhiteSpace(string text)
        {
            text = Regex.Replace(text, @"^\s+", "");
            text = Regex.Replace(text, @"\s+$", "");
            return Regex.Replace(text, @"\s+", " ");
        }
    }
}
