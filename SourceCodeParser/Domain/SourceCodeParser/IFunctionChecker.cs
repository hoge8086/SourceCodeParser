namespace SourceCodeParser.Domain.SourceCodeParser
{
    public interface IFunctionChecker
    {
        //コンストラクタの引数は(string str1, [string str2] ...)である必要がある.
        bool check(string functionDefinition);
    }
}
