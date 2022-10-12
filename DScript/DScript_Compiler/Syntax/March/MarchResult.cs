namespace DScript.Compiler
{
    /// <summary>
    /// 匹配结果
    /// </summary>
    internal class MarchResult
    {
        public bool Success;
        public int Start;
        public int End;

        public bool IsEmpty()
        {
            return Start == End;
        }
    }
}