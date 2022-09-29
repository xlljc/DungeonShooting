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
        private string[] _lexerStrings;

        public void FromSource(string sourceName, string code)
        {
            var list = new List<string>();

            var length = code.Length;
            //分析词法
            for (var i = 0; i < length; i++)
            {
                char c = code[i];
                if (c == length - 1)
                {
                    if (!IsEmpty(c))
                    {
                        list.Add(c.ToString());
                    }
                }

                if (IsSymbol(c)) //符号
                {
                    if (c == '"') //字符串
                    {
                        var sb = new StringBuilder();
                        sb.Append(c);
                        var tempNum = 0; //碰到连续转义斜杠次数
                        //解析字符串
                        for (i++; i < length; i++)
                        {
                            var cNext = code[i];
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

                        list.Add(sb.ToString());
                    }
                    else if (c == '/') //匹配
                    {
                        if (i <= length - 2)
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
                            list.Add(c.ToString());
                        }
                    }
                    else
                    {
                        list.Add(c.ToString());
                    }
                }
                else if (!IsEmpty(c)) //单词
                {
                    var sb = new StringBuilder();
                    sb.Append(c);
                    //解析单词
                    for (i++; i < length; i++)
                    {
                        var cNext = code[i];
                        if (IsEmpty(cNext) || IsSymbol(cNext))
                        {
                            i--;
                            break;
                        }

                        sb.Append(cNext);
                    }

                    list.Add(sb.ToString());
                }

                _lexerStrings = list.ToArray();
            }
        }

        public string[] GetLexerStrings()
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