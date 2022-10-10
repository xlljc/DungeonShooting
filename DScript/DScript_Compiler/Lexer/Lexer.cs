using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DScript.Compiler
{
    /// <summary>
    /// 词法分析
    /// </summary>
    public class Lexer
    {
        private Token[] _lexerStrings;

        /// <summary>
        /// 从源码解析词法
        /// </summary>
        /// <param name="code">源代码</param>
        public void FromSource(string code)
        {
            var list = new List<Token>();
            //行
            var row = 1;
            //列
            var column = 1;
            //长度
            var length = code.Length;
            var i = 0;

            Action moveNext = () =>
            {
                if (i < length)
                {
                    //判断是否换行
                    if (code[i++] == '\n')
                    {
                        row += 1;
                        column = 1;
                    }
                    else
                    {
                        column++;
                    }
                }
            };
            
            //分析词法
            for (;i < length; moveNext())
            {
                char c = code[i];

                if (IsSymbol(c)) //符号
                {
                    if (c == '\n') //换行
                    {
                        if (list.Count > 0)
                        {
                            //去除多余的分号和换行符
                            var prev = list[list.Count - 1];
                            if (prev.Type != TokenType.LineFeed && prev.Code != ";")
                            {
                                list.Add(new Token(string.Empty, i, 1, row, column, TokenType.LineFeed));
                            }
                        }
                    }
                    else if (c == ';') //分号
                    {
                        if (list.Count > 0)
                        {
                            //去除多余的分号和换行符
                            var prev = list[list.Count - 1];
                            if (prev.Type != TokenType.LineFeed && prev.Code != ";")
                            {
                                list.Add(new Token(";", i, 1, row, column, TokenType.Semicolon));
                            }
                        }
                    }
                    else if (c == '.') //点
                    {
                        list.Add(new Token(".", i, 1, row, column, TokenType.Dot));
                    }
                    else if (c == '"') //字符串
                    {
                        var sb = new StringBuilder();
                        var index = i;
                        sb.Append(c);
                        var tempNum = 0; //碰到连续转义斜杠次数

                        var startRow = row;
                        var startColumn = column;
                        
                        //解析字符串
                        for (; i < length - 1; moveNext())
                        {
                            var cNext = code[i + 1];
                            if (cNext == '\n')
                            {
                                sb.Append(cNext); //碰到换行, 停止
                                moveNext();
                                break;
                            }
                            else if (cNext == '"')
                            {
                                if (tempNum % 2 == 0) // "未被成功转义, 直接停止
                                {
                                    sb.Append(cNext);
                                    moveNext();
                                    break;
                                }
                            }

                            sb.Append(cNext);
                            tempNum = cNext == '\\' ? (tempNum + 1) : 0;
                        }

                        list.Add(new Token(sb.ToString(), index, sb.Length, startRow, startColumn, TokenType.String));
                    }
                    else if (c == '/') //匹配注释
                    {
                        if (i <= length - 2) //必须匹配两个字符
                        {
                            if (code[i + 1] == '/') //碰到单行注释 //
                            {
                                //跳过注释这一行
                                for (i = i + 2; i < length; moveNext())
                                {
                                    if (code[i] == '\n')
                                    {
                                        break;
                                    }
                                }
                            }
                            else if (code[i + 1] == '*') //碰到多行注释
                            {
                                //跳过注释这一段
                                for (i = i + 2; i < length - 1; moveNext())
                                {
                                    if (code[i] == '*' && code[i + 1] == '/')
                                    {
                                        moveNext();
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            list.Add(new Token(c.ToString(), i, 1, row, column, TokenType.Symbol));
                        }
                    }
                    else if (c == '=') //
                    {
                        if (i <= length - 2) //必须匹配两个字符
                        {
                            var cNext = code[i + 1];
                            if (cNext == '=' || cNext == '>') //匹配 ==, =>
                            {
                                list.Add(new Token(c.ToString() + cNext, i, 2, row, column, TokenType.Symbol));
                                moveNext();
                            }
                            else //匹配 =
                            {
                                list.Add(new Token(c.ToString(), i, 1, row, column, TokenType.Symbol));
                            }
                        }
                        else //匹配 =
                        {
                            list.Add(new Token(c.ToString(), i, 1, row, column, TokenType.Symbol));
                        }
                    }
                    else if (c == '>')
                    {
                        if (i <= length - 2) //必须匹配两个字符
                        {
                            var cNext = code[i + 1];
                            if (cNext == '=' || cNext == '>') //匹配 >=, >>
                            {
                                var cNext2 = code[i + 2];
                                if (cNext == '>' && cNext2 == '=') //匹配 >>=
                                {
                                    list.Add(new Token(c.ToString() + cNext + cNext2, i, 3, row, column, TokenType.Symbol));
                                    moveNext();
                                    moveNext();
                                }
                                else
                                {
                                    list.Add(new Token(c.ToString() + cNext, i, 2, row, column, TokenType.Symbol));
                                    moveNext();
                                }
                            }
                            else //匹配 >
                            {
                                list.Add(new Token(c.ToString(), i, 1, row, column, TokenType.Symbol));
                            }
                        }
                        else //匹配 >
                        {
                            list.Add(new Token(c.ToString(), i, 1, row, column, TokenType.Symbol));
                        }
                    }
                    else if (c == '<')
                    {
                        if (i <= length - 2) //必须匹配两个字符
                        {
                            var cNext = code[i + 1];
                            if (cNext == '=' || cNext == '<') //匹配 <=, <<
                            {
                                var cNext2 = code[i + 2];
                                if (cNext == '<' && cNext2 == '=') //匹配 <<=
                                {
                                    list.Add(new Token(c.ToString() + cNext + cNext2, i, 3, row, column, TokenType.Symbol));
                                    moveNext();
                                    moveNext();
                                }
                                else
                                {
                                    list.Add(new Token(c.ToString() + cNext, i, 2, row, column, TokenType.Symbol));
                                    moveNext();
                                }
                            }
                            else //匹配 <
                            {
                                list.Add(new Token(c.ToString(), i, 1, row, column, TokenType.Symbol));
                            }
                        }
                        else //匹配 <
                        {
                            list.Add(new Token(c.ToString(), i, 1, row, column, TokenType.Symbol));
                        }
                    }
                    else if (c == '+')
                    {
                        if (i <= length - 2) //必须匹配两个字符
                        {
                            var cNext = code[i + 1];
                            if (cNext == '+' || cNext == '=') //匹配 ++ +=
                            {
                                list.Add(new Token(c.ToString() + cNext, i, 2, row, column, TokenType.Symbol));
                                moveNext();
                            }
                            else //匹配 +
                            {
                                list.Add(new Token(c.ToString(), i, 1, row, column, TokenType.Symbol));
                            }
                        }
                        else //匹配 +
                        {
                            list.Add(new Token(c.ToString(), i, 1, row, column, TokenType.Symbol));
                        }
                    }
                    else if (c == '-')
                    {
                        if (i <= length - 2) //必须匹配两个字符
                        {
                            var cNext = code[i + 1];
                            if (cNext == '-' || cNext == '=') //匹配 -- -=
                            {
                                list.Add(new Token(c.ToString() + cNext, i, 2, row, column, TokenType.Symbol));
                                moveNext();
                            }
                            else //匹配 -
                            {
                                list.Add(new Token(c.ToString(), i, 1, row, column, TokenType.Symbol));
                            }
                        }
                        else //匹配 -
                        {
                            list.Add(new Token(c.ToString(), i, 1, row, column, TokenType.Symbol));
                        }
                    }
                    else if (c == '*' || c == '/' || c == '%' || c == '!' || c == '&' || c == '^' || c == '|')
                    {
                        if (i <= length - 2) //必须匹配两个字符
                        {
                            var cNext = code[i + 1];
                            if (cNext == '=') //匹配
                            {
                                list.Add(new Token(c.ToString() + cNext, i, 2, row, column, TokenType.Symbol));
                                moveNext();
                            }
                            else //匹配
                            {
                                list.Add(new Token(c.ToString(), i, 1, row, column, TokenType.Symbol));
                            }
                        }
                        else //匹配
                        {
                            list.Add(new Token(c.ToString(), i, 1, row, column, TokenType.Symbol));
                        }
                    }
                    else
                    {
                        list.Add(new Token(c.ToString(), i, 1, row, column, TokenType.Symbol));
                    }
                }
                else if (!IsEmpty(c)) //单词
                {
                    var startRow = row;
                    var startColumn = column;
                    
                    var index = i;
                    var sb = new StringBuilder();
                    sb.Append(c);
                    //解析单词
                    for (; i < length - 1; moveNext())
                    {
                        var cNext = code[i + 1];
                        if (IsEmpty(cNext) || IsSymbol(cNext))
                        {
                            break;
                        }

                        sb.Append(cNext);
                    }

                    var word = sb.ToString();
                    if (Keyword.IsKeyword(word))
                    {
                        list.Add(new Token(word, index, sb.Length, startRow, startColumn, TokenType.Keyword));
                    }
                    else if (Regex.IsMatch(word, "^[0-9]+$"))
                    {
                        list.Add(new Token(word, index, sb.Length, startRow, startColumn, TokenType.Number));
                    }
                    else
                    {
                        list.Add(new Token(word, index, sb.Length, startRow, startColumn, TokenType.Word));
                    }
                }
            }

            _lexerStrings = list.ToArray();
        }

        /// <summary>
        /// 返回解析出来的所有词法token
        /// </summary>
        public Token[] GetLexerStrings()
        {
            return _lexerStrings;
        }

        //返回是否为符号
        private bool IsSymbol(char c)
        {
            return c == '|' || c == '&' || c == '>' || c == '<' || c == '=' || c == '"' || c == '(' || c == ')' ||
                   c == '{' || c == '}' || c == '[' || c == ']' || c == '+' || c == '-' || c == '*' || c == '/' ||
                   c == '%' || c == ';' || c == '!' || c == '^' || c == '~' || c == '?' || c == ':' || c == '.' ||
                   c == '\n' || c == ',';
        }

        //返回是否为空
        private bool IsEmpty(char c)
        {
            return c == '\n' || c == ' ' || c == '\t' || c == '\r';
        }
    }
}