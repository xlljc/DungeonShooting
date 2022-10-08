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
        public void NextKeyword(Token token)
        {
            switch (token.Code)
            {
                case "import": //导入语句
                    ImportKeyword(token);
                    break;
                case "namespace": //命名空间
                    
                    break;
            }
        }

        private void ImportKeyword(Token token)
        {
            var nextToken = _syntaxTree.GetNextToken();
            if (nextToken != null && nextToken.Type == TokenType.Word) //下一个必须是单词
            {
                var nextToken2 = _syntaxTree.TryGetNextToken();
                if (nextToken2 != null)
                {
                    if (nextToken2.Type == TokenType.LineFeed || nextToken2.Type == TokenType.Semicolon) //如果是换行或者分号
                    {
                        
                    }
                }
                else
                {
                    throw new Exception("错误的语法, import 后面应接名称.");
                }
            }
            else
            {
                throw new Exception("错误的语法, import 后面应接名称.");
            }
        }
    }
}