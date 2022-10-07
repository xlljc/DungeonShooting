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
        tree.FromSource(text);
        var arr = tree.GetLexerStrings();
        for (int i = 0; i < arr.Length; i++)
        {
            Console.WriteLine(arr[i].ToString());
        }
    }

    [Fact(DisplayName = "TestCreateNamespace, 测试创建Namespace节点对象")]
    public void TestCreateNamespace()
    {
        var namespaceNode1 = NamespaceNode.FromNamespace("a.b.c");
        var namespaceNode2 = NamespaceNode.FromNamespace("a.b");
        var namespaceNode3 = NamespaceNode.FromNamespace("a");
        var namespaceNode4 = NamespaceNode.FromNamespace("a.b.d");
    }

    [Fact(DisplayName = "TestCreateNode, 测试创建节点对象")]
    public void TestCreateNode()
    {
        var namespaceNode1 = NamespaceNode.FromNamespace("a.b.c");
        var classNode = new ClassNode(namespaceNode1, "MyClass");
    }
    
}