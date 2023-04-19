namespace DScript.Generate
{
    public class MethodDescribe : Describe
    {
        /// <summary>
        /// 是否公开
        /// </summary>
        public bool IsPublic { get; set; } = true;

        /// <summary>
        /// 参数长度
        /// </summary>
        public int ParamLength { get; set; } = 0;

        /// <summary>
        /// 是否有动态参数
        /// </summary>
        public bool IsDynamicParam { get; set; } = false;

        /// <summary>
        /// 是否是静态
        /// </summary>
        public bool IsStatic { get; set; } = false;

        public MethodDescribe(string name) : base(name)
        {

        }
    }
}