namespace SourceCodeParser.Domain.SourceCodeParser
{
    public interface IFunctionChecker
    {
        //コンストラクタの引数は(string[] args)である必要がある.
        bool check(string functionDefinition);
    }
}
