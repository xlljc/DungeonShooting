using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace DScript.Compiler.Test;

public class CompilerTest1 : UnitTest
{
    public CompilerTest1(ITestOutputHelper helper) : base(helper)
    {
    }

    [Fact(DisplayName = "Test1, 测试解析词法")]
    public void Test1()
    {
        var fileName = "Script/example.ds";
        var text = File.ReadAllText(fileName);
            var tree = new Lexer();
        ExecuteTime.Run("执行用时", () =>
        {
            tree.FromSource(text);
            var arr = tree.GetLexerStrings();
             for (int i = 0; i < arr.Length; i++)
             {
                 LogUtils.Log(arr[i].ToString());
             }
        });

    }

    [Fact(DisplayName = "Test2, 测试解析表达式")]
    public void Test2()
    {
        var code = "1 + 1";
        var tree = new Lexer();
        tree.FromSource(code);
        var syntaxTree = new SyntaxTree();
        syntaxTree.ParseToken("noname", tree.GetLexerStrings());
    }

}