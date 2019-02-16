using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SourceCodeParser.Domain;

namespace SourceCodeParser.Infrastructure
{
    public class TextFileReaderImpl : ITextFileReader
    {
        public string Read(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.GetEncoding("Shift_JIS"));
            string text = sr.ReadToEnd();
            sr.Close();
            return text;
        }
    }
}
