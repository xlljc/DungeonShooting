using System;

namespace DScript.Compiler
{
    internal static class MarchUtils
    {
        
        public static void March(SyntaxTree syntaxTree, MarchData[] marchTypes, Action<MarchResult> callback)
        {
            //记录起始位置
            var marchResult = new MarchResult();
            marchResult.Start = syntaxTree.GetTokenIndex() + 1;
            for (var i = 0; i < marchTypes.Length; i++)
            {
                var item = marchTypes[i];
                if (item.IsCode)
                {
                    syntaxTree.GetNextTokenIgnoreLineFeed(out var tempToken);
                    if (tempToken.Code != item.Code) //匹配失败
                    {
                        marchResult.Success = false;
                        marchResult.End = syntaxTree.GetTokenIndex();
                        callback(marchResult);
                        return;
                    }
                }
                else
                {
                    if (item.MarchType == MarchType.Word) //匹配单个单词
                    {
                        syntaxTree.GetNextTokenIgnoreLineFeed(out var tempToken);
                        if (tempToken.Type != TokenType.Word) //匹配失败
                        {
                            marchResult.Success = false;
                            marchResult.End = syntaxTree.GetTokenIndex();
                            callback(marchResult);
                            return;
                        }
                    }
                    else if (item.MarchType == MarchType.FullWord)
                    {
                        if (!MarchFullName(syntaxTree)) //匹配失败
                        {
                            marchResult.Success = false;
                            marchResult.End = syntaxTree.GetTokenIndex();
                            callback(marchResult);
                            return;
                        }
                    }
                }
            }

            marchResult.Success = true;
            marchResult.End = syntaxTree.GetTokenIndex();
            callback(marchResult);
        }

        //返回是否匹配成功
        private static bool MarchFullName(SyntaxTree syntaxTree)
        {
            //是否能结束匹配
            bool canEnd = false;
            Token tempToken;

            while (syntaxTree.HasNextToken())
            {
                //获取下一个token
                var lineFeed = syntaxTree.GetNextTokenIgnoreLineFeed(out tempToken);
                if (canEnd)
                {
                    if (tempToken == null) //文件结束
                    {
                        //完成
                        return true;
                    }
                    else if (tempToken.Type == TokenType.Dot) //碰到逗号, 继续匹配
                    {
                        canEnd = false;
                    }
                    else if (tempToken.Type == TokenType.Semicolon) //碰到分号, 直接结束
                    {
                        //完成
                        return true;
                    }
                    else if (lineFeed) //换了行, 结束
                    {
                        //回滚索引
                        syntaxTree.RollbackTokenIndex();
                        //完成
                        return true;
                    }
                    else //语法错误
                    {
                        return false;
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
                        return false;
                    }
                }
            }

            return false;
        }
        
    }
}