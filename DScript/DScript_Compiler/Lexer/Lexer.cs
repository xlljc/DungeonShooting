using System;
using System.Collections.Generic;
using System.Text;

namespace DScript.Compiler
{
    /// <summary>
    /// 词法分析
    /// </summary>
    public class Lexer
    {
        private Token[] _lexerStrings;

        public void FromSource(string sourceName, string code)
        {
            var list = new List<Token>();
            //行
            var row = 1;
            //列
            var column = 0;
            //长度
            var length = code.Length;

            //分析词法
            for (var i = 0; i < length; i++)
            {
                char c = code[i];
                if (c == length - 1)
                {
                    if (!IsEmpty(c))
                    {
                        list.Add(new Token(c.ToString(), i, 1));
                    }
                }

                if (IsSymbol(c)) //符号
                {
                    if (c == ';') //跳过;
                    {
                    }
                    else if (c == '"') //字符串
                    {
                        var sb = new StringBuilder();
                        var index = i;
                        sb.Append(c);
                        var tempNum = 0; //碰到连续转义斜杠次数

                        //解析字符串
                        for (; i < length - 1; i++)
                        {
                            var cNext = code[i + 1];
                            if (cNext == '\n')
                            {
                                sb.Append(cNext); //碰到换行, 停止
                                break;
                            }
                            else if (cNext == '"')
                            {
                                if (tempNum % 2 == 0) // "未被成功转义, 直接停止
                                {
                                    sb.Append(cNext);
                                    break;
                                }
                            }

                            sb.Append(cNext);
                            tempNum = cNext == '\\' ? (tempNum + 1) : 0;
                        }

                        list.Add(new Token(sb.ToString(), index, sb.Length));
                    }
                    else if (c == '/') //匹配 /
                    {
                        if (i <= length - 2) //必须匹配两个字符
                        {
                            if (code[i + 1] == '/') //碰到单行注释 //
                            {
                                //跳过注释这一行
                                for (i = i + 2; i < length; i++)
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
                                for (i = i + 2; i < length - 1; i++)
                                {
                                    if (code[i] == '*' && code[i + 1] == '/')
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            list.Add(new Token(c.ToString(), i, 1));
                        }
                    }
                    else if (c == '=') //
                    {
                        if (i <= length - 2) //必须匹配两个字符
                        {
                            var cNext = code[i + 1];
                            if (cNext == '=' || cNext == '>') //匹配 ==, =>
                            {
                                list.Add(new Token(c.ToString() + cNext, i++, 2));
                            }
                            else
                            {
                                list.Add(new Token(c.ToString(), i, 1));
                            }
                        }
                        else
                        {
                            list.Add(new Token(c.ToString(), i, 1));
                        }
                    }
                    else if (c == '>')
                    {
                        if (i <= length - 2) //必须匹配两个字符
                        {
                            var cNext = code[i + 1];
                            if (cNext == '=' || cNext == '>') //匹配 >=, >>
                            {
                                list.Add(new Token(c.ToString() + cNext, i++, 2));
                            }
                            else
                            {
                                list.Add(new Token(c.ToString(), i, 1));
                            }
                        }
                        else
                        {
                            list.Add(new Token(c.ToString(), i, 1));
                        }
                    }
                    else if (c == '<')
                    {
                        if (i <= length - 2) //必须匹配两个字符
                        {
                            var cNext = code[i + 1];
                            if (cNext == '=' || cNext == '<') //匹配 <=, <<
                            {
                                list.Add(new Token(c.ToString() + cNext, i++, 2));
                            }
                            else
                            {
                                list.Add(new Token(c.ToString(), i, 1));
                            }
                        }
                        else
                        {
                            list.Add(new Token(c.ToString(), i, 1));
                        }
                    }
                    else
                    {
                        list.Add(new Token(c.ToString(), i, 1));
                    }
                }
                else if (!IsEmpty(c)) //单词
                {
                    var index = i;
                    var sb = new StringBuilder();
                    sb.Append(c);
                    //解析单词
                    for (; i < length - 1; i++)
                    {
                        var cNext = code[i + 1];
                        if (IsEmpty(cNext) || IsSymbol(cNext))
                        {
                            break;
                        }

                        sb.Append(cNext);
                    }

                    list.Add(new Token(sb.ToString(), index, sb.Length));
                }
            }

            _lexerStrings = list.ToArray();
        }

        public Token[] GetLexerStrings()
        {
            return _lexerStrings;
        }

        //返回是否为符号
        private bool IsSymbol(char c)
        {
            return c == '|' || c == '&' || c == '>' || c == '<' || c == '=' || c == '"' || c == '(' || c == ')' ||
                   c == '{' || c == '}' || c == '[' || c == ']' || c == '+' || c == '-' || c == '*' || c == '/' ||
                   c == '%' || c == ';' || c == '!' || c == '^' || c == '~' || c == '?' || c == ':' || c == '.';
        }

        //返回是否为空
        private bool IsEmpty(char c)
        {
            return c == '\n' || c == ' ' || c == '\t' || c == '\r';
        }

        private bool IsWord(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9');
        }
    }
}