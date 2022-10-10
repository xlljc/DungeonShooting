namespace DScript.Compiler
{
    /// <summary>
    /// 用于描述导入名称的节点
    /// </summary>
    public class ImportNode : NodeBase
    {
        /// <summary>
        /// 导入时简化的名称
        /// </summary>
        public Token ImportName;
        
        /// <summary>
        /// 导入前的全称
        /// </summary>
        public Token[] FullName;

        public ImportNode(string name, Token importName, Token[] fullName) : base(name)
        {
            ImportName = importName;
            FullName = fullName;
        }
    }
}