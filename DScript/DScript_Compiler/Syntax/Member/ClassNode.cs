namespace DScript.Compiler
{
    /// <summary>
    /// 类节点
    /// </summary>
    public class ClassNode : NodeBase
    {
        /// <summary>
        /// 所属命名空间
        /// </summary>
        public readonly NamespaceNode NamespaceNode;

        /// <summary>
        /// 全名
        /// </summary>
        public readonly string FullName;

        /// <summary>
        /// 父类名称
        /// </summary>
        public readonly string ParentName;
        
        public ClassNode(NamespaceNode namespaceNode, string name, string parent) : base(name)
        {
            NamespaceNode = namespaceNode;
            NamespaceNode.AddChild(this);
            FullName = NamespaceNode.FullName + "." + name;
            ParentName = parent;
        }
    }
}