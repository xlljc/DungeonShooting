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
        public NamespaceNode NamespaceNode;

        /// <summary>
        /// 全名
        /// </summary>
        public string FullName;

        /// <summary>
        /// 父类名称
        /// </summary>
        public readonly string ParentName;
        
        public ClassNode(string name, string parent) : base(name)
        {
            ParentName = parent;
            FullName = name;
        }

        /// <summary>
        /// 设置该类的命名空间
        /// </summary>
        /// <param name="namespaceNode"></param>
        public void SetNamespace(NamespaceNode namespaceNode)
        {
            NamespaceNode = namespaceNode;
            NamespaceNode.AddChild(this);
            FullName = NamespaceNode.FullName + "." + Name;
        }

        public void AddFunction(FunctionNode functionNode)
        {
            
        }
    }
}