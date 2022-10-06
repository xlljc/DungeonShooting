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
        
        public ClassNode(NamespaceNode namespaceNode, string name) : base(name)
        {
            NamespaceNode = namespaceNode;
            NamespaceNode.AddChild(this);
        }
    }
}