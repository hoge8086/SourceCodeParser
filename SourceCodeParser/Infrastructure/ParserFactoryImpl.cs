
using SourceCodeParser.Domain.SourceCodeParser;
using SourceCodeParser.Domain;
using System.Collections.Generic;
using System.Linq;
namespace SourceCodeParser.Infrastructure
{
    public class ParserFactoryImpl : IParserFactory
    {
        private List<Domain.SourceCodeParser.SourceCodeParser.Setting> settings = new List<Domain.SourceCodeParser.SourceCodeParser.Setting>();

        public ParserFactoryImpl()
        {
            foreach(var file in System.IO.Directory.GetFiles(@".\", "*.json"))
            {
                settings.Add(new ParseSettingLoader().Load(file));
            }
        }

        public Domain.SourceCodeParser.SourceCodeParser createParser(string targetSourcePath)
        {
            var extension = System.IO.Path.GetExtension(targetSourcePath);
            var setting = settings.FirstOrDefault(s => s.TargetExtensions.Contains(extension));
            if (setting == null)
                return null;

            return new Domain.SourceCodeParser.SourceCodeParser(setting);
        }
    }
}
