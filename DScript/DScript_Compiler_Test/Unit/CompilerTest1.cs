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

    [Fact(DisplayName = "Test1, 测试基础编译行为")]
    public void Test1()
    {
        var fileName = "Script/Test.ds";
        var text = File.ReadAllText(fileName);
        var tree = new Lexer();
        tree.FromSource(fileName, text);
        var arr = tree.GetLexerStrings();
        for (int i = 0; i < arr.Length; i++)
        {
            Console.WriteLine(arr[i]);
        }
    }

}