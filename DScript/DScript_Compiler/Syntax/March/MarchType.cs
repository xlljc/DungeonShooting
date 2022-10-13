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
        /// <summary>
        /// 匹配全称, 名称路径中可以包含关键字, 类似于: a, a.b.c, a.b.class
        /// </summary>
        FullKeyword,
        /// <summary>
        /// 匹配小括号组, 相当于 ()
        /// </summary>
        ParenthesesGroup,
        /// <summary>
        /// 匹配中括号组, 相当于 []
        /// </summary>
        BracketGroup,
        /// <summary>
        /// 匹配大括号组, 相当于 {}
        /// </summary>
        BraceGroup,
    }
}