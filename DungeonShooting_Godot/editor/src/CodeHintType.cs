namespace DScript.GodotEditor
{
    
    /// <summary>
    /// 用于区分代码提示项的类型
    /// </summary>
    public enum CodeHintType
    {
        /// <summary>
        /// 关键字
        /// </summary>
        Keyword,
        /// <summary>
        /// 命名空间
        /// </summary>
        Namespace,
        /// <summary>
        /// 类
        /// </summary>
        Class,
        /// <summary>
        /// 枚举
        /// </summary>
        Enum,
        /// <summary>
        /// 函数
        /// </summary>
        Function,
        /// <summary>
        /// 变量
        /// </summary>
        Variable,
        /// <summary>
        /// 字段
        /// </summary>
        Field,
    }
}