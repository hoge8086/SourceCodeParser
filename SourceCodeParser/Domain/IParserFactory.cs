namespace SourceCodeParser.Domain
{
    public interface IParserFactory
    {
        SourceCodeParser createParser(string targetSourcePath);
    }
}