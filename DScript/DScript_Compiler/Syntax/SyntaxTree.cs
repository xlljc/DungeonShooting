
using System;
using System.Collections.Generic;

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
        private Dictionary<string, FileToken> _fileTokens = new Dictionary<string, FileToken>();
        private string _currFile;
        private FileToken _currFileToken;
        private int _currIndex = -1;

        public SyntaxTree()
        {
            _treeParse = new SyntaxTreeParse(this);
        }

        public void ParseToken(string path, Token[] tokens)
        {
            if (_fileTokens.ContainsKey(path))
            {
                //相同的文件名
                throw new Exception("xxx");
            }

            FileToken fileToken = new FileToken(path, tokens);
            _fileTokens.Add(path, fileToken);
            _currFileToken = fileToken;

            _currIndex = -1;
            Token? current;
            while ((current = GetNextToken()) != null)
            {
                if (current.Type == TokenType.Keyword)
                {
                    _treeParse.NextKeyword(current, fileToken);
                }
            }
        }

        /// <summary>
        /// 获取当前正在解析的token
        /// </summary>
        internal Token? GetCurrentToken()
        {
            if (_currFile == null || !_fileTokens.ContainsKey(_currFile))
            {
                return null;
            }

            var tokens = _fileTokens[_currFile].Tokens;
            if (_currIndex < tokens.Length)
            {
                return tokens[_currIndex];
            }

            return null;
        }

        /// <summary>
        /// 获取下一个token, 会移动指针
        /// </summary>
        internal Token? GetNextToken()
        {
            if (_currFileToken == null)
            {
                return null;
            }

            _currIndex++;
            var tokens = _currFileToken.Tokens;
            if (_currIndex < tokens.Length)
            {
                return tokens[_currIndex];
            }

            return null;
        }

        /// <summary>
        /// 获取下一个非换行 token, 会移动指针, 返回的 bool 代表是否碰到换行
        /// </summary>
        internal bool GetNextTokenIgnoreLineFeed(out Token token)
        {
            if (_currFileToken == null)
            {
                token = null;
                return false;
            }

            _currIndex++;
            var tokens = _currFileToken.Tokens;

            bool flag = false;
            for (; _currIndex < tokens.Length; _currIndex++)
            {
                var tk = tokens[_currIndex];
                if (tk.Type == TokenType.LineFeed) //匹配到换行
                {
                    flag = true;
                }
                else
                {
                    token = tk;
                    return flag;
                }
            }

            token = null;
            return flag;
        }

        /// <summary>
        /// 尝试获取下一个token, 不会移动指针
        /// </summary>
        /// <param name="offset">获取token的偏移量</param>
        internal Token? TryGetNextToken(int offset = 1)
        {
            if (_currFileToken == null)
            {
                return null;
            }

            var tokens = _currFileToken.Tokens;
            if (_currIndex < tokens.Length - offset)
            {
                return tokens[_currIndex + offset];
            }

            return null;
        }

        /// <summary>
        /// 尝试获取下一个 token, 忽略换行, 不会移动指针
        /// </summary>
        /// <param name="offset">获取token的偏移量</param>
        internal bool TryGetNextTokenIgnoreLineFeed(out Token token, int offset = 1)
        {
            if (_currFileToken == null)
            {
                token = null;
                return false;
            }

            var tokens = _currFileToken.Tokens;

            bool flag = false;
            for (var i = _currIndex + 1; i < tokens.Length; i++)
            {
                var tk = tokens[i];
                if (tk.Type == TokenType.LineFeed) //匹配到换行
                {
                    flag = true;
                }
                else
                {
                    token = tk;
                    return flag;
                }
            }

            token = null;
            return flag;
        }

        /// <summary>
        /// 返回是否有下一个token
        /// </summary>
        internal bool HasNextToken()
        {
            if (_currFileToken == null)
            {
                return false;
            }

            var tokens = _currFileToken.Tokens;
            return _currIndex < tokens.Length - 1;
        }

        /// <summary>
        /// 回退解析索引
        /// </summary>
        internal int RollbackTokenIndex()
        {
            if (_currIndex == -1)
            {
                return -1;
            }

            if (_currIndex > 0)
            {
                _currIndex--;
            }
            else
            {
                _currIndex = 0;
            }

            return _currIndex;
        }

        /// <summary>
        /// 获取解析索引
        /// </summary>
        internal int GetTokenIndex()
        {
            return _currIndex;
        }

        internal Token[] CopyTokens(int start, int end, bool ignoreLineFeed = true)
        {
            if (_currFileToken == null)
            {
                return null;
            }

            var tokens = _currFileToken.Tokens;
            List<Token> newArr = new List<Token>();
            for (int i = start; i < end; i++)
            {
                var temp = tokens[i];
                if (!ignoreLineFeed || temp.Type != TokenType.LineFeed)
                {
                    newArr.Add(temp);
                }
            }

            return newArr.ToArray();
        }
    }
}