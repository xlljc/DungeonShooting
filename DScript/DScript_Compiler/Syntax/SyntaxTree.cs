
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

        private SyntaxTreeParse _treeParse;
        private readonly Token[] _tokens;
        private int _currIndex = -1;
        
        public SyntaxTree(Token[] tokens)
        {
            _treeParse = new SyntaxTreeParse(this);
            _tokens = tokens;
            while (HasNextToken())
            {
                var current = GetNextToken();
                
                if (current.Type == TokenType.Keyword)
                {
                    _treeParse.NextKeyword(current);
                }
                
            }
        }

        /// <summary>
        /// 获取当前正在解析的token
        /// </summary>
        internal Token? GetCurrentToken()
        {
            if (_currIndex < _tokens.Length)
            {
                return _tokens[_currIndex];
            }

            return null;
        }
        
        /// <summary>
        /// 获取下一个token, 会移动指针
        /// </summary>
        internal Token? GetNextToken()
        {
            _currIndex++;
            if (_currIndex < _tokens.Length)
            {
                return _tokens[_currIndex];
            }

            return null;
        }

        /// <summary>
        /// 尝试获取下一个token, 不会移动指针
        /// </summary>
        /// <param name="offset">获取token的偏移量</param>
        internal Token? TryGetNextToken(int offset = 1)
        {
            if (_currIndex < _tokens.Length - offset)
            {
                return _tokens[_currIndex + offset];
            }

            return null;
        }

        internal bool HasNextToken()
        {
            return _currIndex < _tokens.Length - 1;
        }
    }
}