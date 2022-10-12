namespace DScript.Compiler
{
    /// <summary>
    /// Token类型
    /// </summary>
    public enum TokenType
    {
        /// <summary>
        /// 关键字
        /// </summary>
        Keyword,
        /// <summary>
        /// 单词
        /// </summary>
        Word,
        /// <summary>
        /// 字符串
        /// </summary>
        String,
        /// <summary>
        /// 数字类型
        /// </summary>
        Number,
        /// <summary>
        /// 各种符号
        /// </summary>
        Symbol,
        /// <summary>
        /// 换行
        /// </summary>
        LineFeed,
        /// <summary>
        /// 分号
        /// </summary>
        Semicolon,
        /// <summary>
        /// 点
        /// </summary>
        Dot,
        /// <summary>
        /// 大括号 (左)
        /// </summary>
        BraceLeft,
        /// <summary>
        /// 中括号 (左)
        /// </summary>
        BracketLeft,
        /// <summary>
        /// 小括号 (左)
        /// </summary>
        ParenthesesLeft,
        /// <summary>
        /// 大括号 (右)
        /// </summary>
        BraceRight,
        /// <summary>
        /// 中括号 (右)
        /// </summary>
        BracketRight,
        /// <summary>
        /// 小括号 (右)
        /// </summary>
        ParenthesesRight,
    }
}