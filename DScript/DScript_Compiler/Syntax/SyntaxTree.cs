using System;
using System.Collections;

namespace DScript.Compiler
{
    /// <summary>
    /// 语法树
    /// </summary>
    public class SyntaxTree
    {
        /// <summary>
        /// 根命名空间
        /// </summary>
        public NamespaceNode Root { get; } = new NamespaceNode("global", "global", null);

        
        private readonly Token[] _tokens;
        private int _currIndex = 0;
        
        public SyntaxTree(Token[] tokens)
        {
            var aTreeParse = new SyntaxTreeParse(this);
            _tokens = tokens;
            while (_currIndex < _tokens.Length)
            {
                var current = GetNextToken();
                
                if (current.Type == TokenType.Keyword)
                {
                    aTreeParse.NextKeyword(current);
                }
                
            }
        }

        internal Token? GetCurrentToken()
        {
            if (_currIndex < _tokens.Length)
            {
                return _tokens[_currIndex];
            }

            return null;
        }
        
        internal Token? GetNextToken()
        {
            _currIndex++;
            if (_currIndex < _tokens.Length)
            {
                return _tokens[_currIndex];
            }

            return null;
        }

        internal Token? TryGetNextToken(int offset = 1)
        {
            if (_currIndex < _tokens.Length - offset)
            {
                return _tokens[_currIndex + offset];
            }

            return null;
        }
        
    }
}