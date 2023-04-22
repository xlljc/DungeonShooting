namespace DScript.Compiler
{
    /// <summary>
    /// 用于描述导入名称的节点
    /// </summary>
    public class ImportNode : NodeBase
    {
        /// <summary>
        /// 导入前的全称
        /// </summary>
        public string FullName;

        public ImportNode(string importName, string fullName) : base(importName)
        {
            FullName = fullName;
        }
    }
}