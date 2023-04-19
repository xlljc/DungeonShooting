
namespace DScript.Generate
{
    public class FieldDescribe : Describe
    {
        /// <summary>
        /// 是否公开
        /// </summary>
        public bool IsPublic { get; set; } = true;

        /// <summary>
        /// 是否是静态
        /// </summary>
        public bool IsStatic { get; set; } = false;

        public FieldDescribe(string name) : base(name)
        {

        }

    }
}