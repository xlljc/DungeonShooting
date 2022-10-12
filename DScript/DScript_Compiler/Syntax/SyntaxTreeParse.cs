using System;

namespace DScript.Compiler
{
    internal class SyntaxTreeParse
    {
        private static readonly MarchData[] ImportMarchData = new[] { new MarchData(MarchType.Word), new MarchData("="), new MarchData(MarchType.FullWord) };
        private static readonly MarchData[] NamespaceMarchData = new[] { new MarchData(MarchType.FullWord) };

        private SyntaxTree _syntaxTree;

        public SyntaxTreeParse(SyntaxTree syntaxTree)
        {
            _syntaxTree = syntaxTree;
        }

        /// <summary>
        /// 解析关键字
        /// </summary>
        /// <param name="token">关键字对象</param>
        /// <param name="fileToken">关联的文件</param>
        public void NextKeyword(Token token, FileToken fileToken)
        {
            switch (token.Code)
            {
                case "import": //导入语句
                    ImportKeyword(token, fileToken);
                    break;
                case "namespace": //命名空间
                    NamespaceKeyword(token, fileToken);
                    break;
            }
        }

        //解析导入
        private void ImportKeyword(Token token, FileToken fileToken)
        {
            /*
             匹配:
                 import n = a;
                 import n = a.b.c;
             */
            MarchUtils.March(_syntaxTree, ImportMarchData,
                (result) =>
                {
                    if (result.Success)
                    {
                        var newArr = _syntaxTree.CopyTokens(result.Start, result.End);
                        //添加导入名称
                        var importName = newArr[0];
                        fileToken.AddImport(importName.Code, new ImportNode(importName.Code, importName, newArr));
                    }
                    else
                    {
                        //import语法错误
                        throw new Exception("xxx");
                    }
                });
        }

        //解析声明命名空间
        private void NamespaceKeyword(Token token, FileToken fileToken)
        {
            /*
             匹配:
                 namespace test;
                 namespace test.a;
             */
            MarchUtils.March(_syntaxTree, NamespaceMarchData,
                (result) =>
                {
                    if (result.Success)
                    {
                        var newArr = _syntaxTree.CopyTokens(result.Start, result.End);
                        string fullName = "";

                        for (int i = 0; i < newArr.Length; i++)
                        {
                            fullName += newArr[i].Code;
                        }
                        //设置声明的命名空间
                        fileToken.SetNamespace(NamespaceNode.FromNamespace(_syntaxTree, fullName));
                    }
                    else
                    {
                        //namespace语法错误
                        throw new Exception("xxx");
                    }
                });
        }
    }
}