namespace SourceCodeParser.Domain.ModificationParser
{
    public interface IModifiedBlockDetector
    {
        bool IsBeginLine(string line);
        bool IsEndLine(string line);
    }
}
