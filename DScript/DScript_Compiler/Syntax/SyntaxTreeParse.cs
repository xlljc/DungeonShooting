using System;

namespace DScript.Compiler
{
    /// <summary>
    /// 语法树解析类
    /// </summary>
    internal class SyntaxTreeParse
    {
        //匹配导入类
        private static readonly MarchData[] ImportMarchData = new[]
        {
            MarchData.FromType(MarchType.Word),
            MarchData.FromCode("="),
            MarchData.FromType(MarchType.FullWord)
        };

        //匹配声明命名空间
        private static readonly MarchData[] NamespaceMarchData = new[]
        {
            MarchData.FromType(MarchType.FullWord)
        };

        //匹配声明类
        private static readonly MarchData[] ClassMarchData = new[]
        {
            MarchData.FromType(MarchType.Word),
            MarchData.FromTryMarch(MarchData.FromCode("extends"), MarchData.FromType(MarchType.FullWord))
        };

        //匹配函数声明
        private static readonly MarchData[] FunctionMarchData = new[]
        {
            MarchData.FromType(MarchType.Word),
            MarchData.FromType(MarchType.ParenthesesGroup),
            MarchData.FromTryMarch(MarchData.FromCode(":"), MarchData.FromType(MarchType.FullKeyword)),
            MarchData.FromType(MarchType.BraceGroup)
        };
        
        //匹配变量声明
        private static readonly MarchData[] VarMarchData = new[]
        {
            MarchData.FromType(MarchType.Word),
            MarchData.FromCode("="),
            MarchData.FromType(MarchType.Expression)
        };
        
        //语法树对象
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
                case "class": //类
                    ClassKeyword(token, fileToken);
                    break;
                case "func": //函数
                    FunctionKeyword(token, fileToken);
                    break;
                case "var": //字段
                    VarKeyword(token, fileToken);
                    break;
            }
        }

        /// <summary>
        /// 解析单词
        /// </summary>
        /// <param name="token">单词对象</param>
        /// <param name="fileToken">关联的文件</param>
        public void NextWorld(Token token, FileToken fileToken)
        {
            LogUtils.Error("未知单词: " + token.Code);
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
                        string fullName = "";
                        for (var i = 2; i < newArr.Length; i++)
                        {
                            fullName += newArr[i].Code;
                        }

                        //添加导入名称
                        var importName = newArr[0];
                        fileToken.AddImport(new ImportNode(importName.Code, fullName));
                    }
                    else
                    {
                        //import语法错误
                        throw new System.Exception("xxx");
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
                        throw new System.Exception("xxx");
                    }
                });
        }

        //匹配类声明
        private void ClassKeyword(Token token, FileToken fileToken)
        {
            /*
             匹配:
                 class a;
                 class a extends b.c;
             */
            MarchUtils.March(_syntaxTree, ClassMarchData,
                (result) =>
                {
                    if (result.Success)
                    {
                        var newArr = _syntaxTree.CopyTokens(result.Start, result.End);
                        ClassNode classNode;
                        if (newArr.Length > 1)
                        {
                            string name = "";
                            for (var i = 2; i < newArr.Length; i++)
                            {
                                name += newArr[i].Code;
                            }

                            //有继承的父类
                            classNode = new ClassNode(newArr[0].Code, name);
                        }
                        else
                        {
                            //没有显示继承的父类
                            classNode = new ClassNode(newArr[0].Code, "Object");
                        }

                        fileToken.SetClass(classNode);
                    }
                    else
                    {
                        //class语法错误
                        throw new System.Exception("xxx");
                    }
                });
        }

        //解析声明函数
        private void FunctionKeyword(Token token, FileToken fileToken)
        {
            /*
             匹配:
                 func a() {}
                 func a(): void {}
                 func a(): System.Float {}
             */
            MarchUtils.March(_syntaxTree, FunctionMarchData,
                (result) =>
                {
                    if (result.Success)
                    {
                        var newArr = _syntaxTree.CopyTokens(result.Start, result.End);
                        var functionNode = new FunctionNode(newArr[0].Code, newArr);
                        fileToken.AddFunction(functionNode);
                    }
                    else
                    {
                        //namespace语法错误
                        throw new System.Exception("xxx");
                    }
                });
        }

        //解析声明字段
        private void VarKeyword(Token token, FileToken fileToken)
        {
            /*
             匹配:
                 var a = 表达式
             */
            MarchUtils.March(_syntaxTree, VarMarchData,
                (result) =>
                {
                    if (result.Success)
                    {
                        var newArr = _syntaxTree.CopyTokens(result.Start, result.End);

                        var str = "";
                        foreach (var item in newArr)
                        {
                            str += item.Code;
                        }
                        LogUtils.Log("var匹配成功: " + str);
                    }
                    else
                    {
                        //var语法错误
                        throw new System.Exception("xxx");
                    }
                });
        }
    }
}