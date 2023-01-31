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
        var fileName = "Script/Test.ds";
        var text = File.ReadAllText(fileName);
        var tree = new Lexer();
        tree.FromSource(text);
        var arr = tree.GetLexerStrings();
        for (int i = 0; i < arr.Length; i++)
        {
            Console.WriteLine(arr[i].ToString());
        }
    }
    
    [Fact(DisplayName = "Test2, 测试解析语法树")]
    public void Test2()
    {
        var fileName = "Script/Test2.ds";
        var text = File.ReadAllText(fileName);
        var tree = new Lexer();
        tree.FromSource(text);
        var syntaxTree = new SyntaxTree();
        syntaxTree.ParseToken(fileName, tree.GetLexerStrings());
    }

    [Fact(DisplayName = "Test3, 测试解析表达式")]
    public void Test3()
    {
        var fileName = "Script/Test3.ds";
        var text = File.ReadAllText(fileName);
        var tree = new Lexer();
        tree.FromSource(text);
        var syntaxTree = new SyntaxTree();
        syntaxTree.ParseToken(fileName, tree.GetLexerStrings());
    }

}