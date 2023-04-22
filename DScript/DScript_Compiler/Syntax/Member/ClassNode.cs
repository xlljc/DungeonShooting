using System.Collections.Generic;

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

        /// <summary>
        /// 类中定义的成员
        /// </summary>
        public readonly Dictionary<string, MemberInfo> Members = new Dictionary<string, MemberInfo>();

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

        /// <summary>
        /// 添加函数
        /// </summary>
        public void AddFunction(FunctionNode functionNode)
        {
            if (!Members.TryGetValue(functionNode.Name, out var memberData))
            {
                var methodNode = new MethodNode(functionNode.Name);
                memberData = new MemberInfo(methodNode, false);
                methodNode.AddFunction(functionNode);
                Members.Add(methodNode.Name, memberData);
            }
            else
            {
                if (memberData.MemberType != MemberType.Method)
                {
                    //已经声明过 'xxx' 的成员了
                    throw new System.Exception("xxx");
                }

                var methodNode = (MethodNode)memberData.Node;
                methodNode.AddFunction(functionNode);
            }
        }
    }
}