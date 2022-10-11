namespace DScript.Compiler
{
    internal enum MarchType
    {
        /// <summary>
        /// 匹配当个单词
        /// </summary>
        Word,
        /// <summary>
        /// 匹配全称, 类似于: a 或者 a.b.c
        /// </summary>
        FullWord,
    }
}