using System;

namespace DScript.Compiler
{
    internal class SyntaxTreeParse
    {

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

                    break;
            }
        }

        private void ImportKeyword(Token token, FileToken fileToken)
        {
            /*
             匹配:
                 import n = a;
                 import n = a.b.c;
             */
            _syntaxTree.GetNextTokenIgnoreLineFeed(out var nameToken); //匹配名称
            _syntaxTree.GetNextTokenIgnoreLineFeed(out var nextToken2); //匹配 =
            //记录起始位置
            int startIndex = _syntaxTree.GetTokenIndex() + 1;
            if (nameToken != null && nextToken2 != null && nameToken.Type == TokenType.Word && nextToken2.Code == "=")
            {
                //是否能结束匹配
                bool canEnd = false;
                Token tempToken;

                while (_syntaxTree.HasNextToken())
                {
                    //获取下一个token
                    var lineFeed = _syntaxTree.GetNextTokenIgnoreLineFeed(out tempToken);
                    if (canEnd)
                    {
                        if (tempToken.Type == TokenType.Dot) //碰到逗号, 继续匹配
                        {
                            canEnd = false;
                        }
                        else if (tempToken.Type == TokenType.Semicolon) //碰到分号, 直接结束
                        {
                            var newArr = _syntaxTree.CopyTokens(startIndex, _syntaxTree.GetTokenIndex());
                            AddImportNode(fileToken, nameToken, newArr);
                            break;
                        }
                        else if (lineFeed) //换了行, 结束
                        {
                            //回滚索引
                            _syntaxTree.RollbackTokenIndex();
                            var newArr = _syntaxTree.CopyTokens(startIndex, _syntaxTree.GetTokenIndex());
                            AddImportNode(fileToken, nameToken, newArr);
                            break;
                        }
                        else //语法错误
                        {
                            //import语法错误
                            throw new Exception("xxx");
                        }
                    }
                    else
                    {
                        if (tempToken.Type == TokenType.Word)
                        {
                            canEnd = true;
                        }
                        else //语法错误
                        {
                            //import语法错误
                            throw new Exception("xxx");
                        }
                    }
                }
            }
            else
            {
                //错误的语法, import 后面应接名称.
                throw new Exception("xxx");
            }
        }

        private void AddImportNode(FileToken fileToken, Token importName, Token[] fullName)
        {
            //到文件结尾
            if (!fileToken.Import.ContainsKey(importName.Code))
            {
                fileToken.Import.Add(importName.Code, new ImportNode(importName.Code, importName, fullName));
            }
            else
            {
                //导入了相同的名称 {nextToken.Code}
                throw new Exception("xxx");
            }
        }
    }
}