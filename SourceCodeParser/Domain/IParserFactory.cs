using SourceCodeParser.Domain.SourceCodeParser;
namespace SourceCodeParser.Domain
{
    public interface IParserFactory
    {
        Domain.SourceCodeParser.SourceCodeParser createParser(string targetSourcePath);
    }
}