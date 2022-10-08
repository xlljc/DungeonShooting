namespace DScript.Compiler
{
    /// <summary>
    /// 关键字列表
    /// </summary>
    public static class Keyword
    {
        /// <summary>
        /// 包含所有关键字的列表
        /// </summary>
        public static readonly string[] KeywordList =
        {
            "var",
            "namespace",
            "this",
            "class",
            "extends",
            "func",
            "get",
            "set",
            "import",
            "static",
            "return",
            "for",
            "switch",
            "case",
            "break",
            "default",
            "while",
            "do",
            "is",
            "repeat",
            "null",
            "true",
            "false",
            "readonly",
            "enum",
            "private",
            "super",
            "if",
            "else",
            "continue",
            "macro"
        };

        /// <summary>
        /// 返回是否是关键字
        /// </summary>
        public static bool IsKeyword(string word)
        {
            for (var i = 0; i < KeywordList.Length; i++)
            {
                if (KeywordList[i] == word)
                {
                    return true;
                }
            }

            return false;
        }

    }
}