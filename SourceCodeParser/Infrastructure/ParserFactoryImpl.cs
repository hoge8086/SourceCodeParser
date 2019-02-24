
using SourceCodeParser.Domain.SourceCodeParser;
using SourceCodeParser.Domain;
using System.Collections.Generic;
using System.Linq;
namespace SourceCodeParser.Infrastructure
{
    public class ParserFactoryImpl : IParserFactory
    {
        private List<Domain.SourceCodeParser.SourceCodeParser> parsers = new List<Domain.SourceCodeParser.SourceCodeParser>();

        public ParserFactoryImpl()
        {
            foreach(var file in System.IO.Directory.GetFiles(@".\", "*.json"))
            {
                var setting = new ParseSettingLoader().Load(file);
                parsers.Add(new Domain.SourceCodeParser.SourceCodeParser(setting));

            }
        }

        public Domain.SourceCodeParser.SourceCodeParser createParser(string targetSourcePath)
        {
            var parser = parsers.FirstOrDefault(p => p.Setting.IsParsable(targetSourcePath));
            if (parser == null)
                return null;

            return parser;
        }
    }
}
