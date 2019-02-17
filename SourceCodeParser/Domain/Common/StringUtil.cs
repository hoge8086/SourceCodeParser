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
        public static string TrimWhiteSpaceWithoutCRLF(string text)
        {
            text = Regex.Replace(text, @"(^|(?<=\n))[ \t]+", "");
            text = Regex.Replace(text, @"[ \t]+($|(?=\n))", "");
            return Regex.Replace(text, @"[ \t]+", " ");
        }
    }
}
