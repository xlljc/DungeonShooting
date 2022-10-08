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
        
        public void NextKeyword(Token token)
        {
            switch (token.Code)
            {
                case "import": //导入语句
                    var nextToken = _syntaxTree.GetNextToken();
                    if (nextToken != null && nextToken.Type == TokenType.Word)
                    {
                        var nextToken2 = _syntaxTree.TryGetNextToken();
                        // if () //如果是
                        // {
                        //     
                        // }
                    }
                    else
                    {
                        throw new Exception("错误的语法, import 后面应接名称.");
                    }

                    break;
                case "namespace": //命名空间
                    
                    break;
            }
        }
        
    }
}