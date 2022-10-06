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
    }
}