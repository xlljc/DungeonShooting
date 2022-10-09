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
                 import a;
                 import a.b;
                 import n = a;
                 import n = a.b;
             */
            _syntaxTree.GetNextTokenIgnoreLineFeed(out var nextToken);
            if (nextToken != null && nextToken.Type == TokenType.Word) //下一个必须是单词
            {
                var hasLineFeed = _syntaxTree.TryGetNextTokenIgnoreLineFeed(out var nextToken2); //继续往后匹配, 看其是否结束或者有其他的代码
                if (nextToken2 != null)
                {
                    if (hasLineFeed || nextToken2.Type == TokenType.Semicolon) //如果有换行或者是分号
                    {

                    }
                }
                else
                {
                    //错误的语法, import 后面应接名称.
                    throw new Exception("xxx");
                }
            }
            else
            {
                //错误的语法, import 后面应接名称.
                throw new Exception("xxx");
            }
        }
    }
}