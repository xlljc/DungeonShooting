namespace DScript.Compiler
{
    /// <summary>
    /// 匹配结果
    /// </summary>
    internal class MarchResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success;
        /// <summary>
        /// 起始位置
        /// </summary>
        public int Start;
        /// <summary>
        /// 结束位置
        /// </summary>
        public int End;

        /// <summary>
        /// 结果是否为空
        /// </summary>
        public bool IsEmpty()
        {
            return Start == End;
        }
    }
}