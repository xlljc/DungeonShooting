using System;
using System.Collections.Generic;

namespace DScript.Compiler
{
    /// <summary>
    /// 命名空间节点
    /// </summary>
    public class NamespaceNode : NodeBase
    {
        public static NamespaceNode FromNamespace(string fullName)
        {
            var ns = fullName.Split('.');
            if (ns == null || ns.Length == 0)
            {
                return null;
            }

            var rootName = ns[0];
            if (!RootNamespaceNodePool.TryGetValue(rootName, out var root))
            {
                root = new NamespaceNode(rootName, rootName, null);
                RootNamespaceNodePool.Add(rootName, root);
            }

            NamespaceNode tempNode = root;
            string fName = rootName;
            for (var i = 1; i < ns.Length; i++)
            {
                var name = ns[i];
                fName += "." + name;
                var child = tempNode.GetChild(name);
                if (child == null)
                {
                    var namespaceNode = new NamespaceNode(fName, name, tempNode);
                    tempNode.AddChild(namespaceNode);
                    tempNode = namespaceNode;
                }
                else if (child is NamespaceNode namespaceNode)
                {
                    tempNode = namespaceNode;
                }
                else
                {
                    //已经声明了该名称的成员了, 不能再次声明为命名空间
                    throw new Exception("xxx");
                }
            }

            return tempNode;
        }

        /// <summary>
        /// 根命名空间
        /// </summary>
        private static readonly Dictionary<string, NamespaceNode> RootNamespaceNodePool = new Dictionary<string, NamespaceNode>();

        /// <summary>
        /// 命名空间包含的成员
        /// </summary>
        public readonly Dictionary<string, NodeBase> Children = new Dictionary<string, NodeBase>();

        /// <summary>
        /// 当前命名空间全名称
        /// </summary>
        public readonly string FullName;
        
        /// <summary>
        /// 父级
        /// </summary>
        public readonly NamespaceNode Parent;

        private NamespaceNode(string fullName, string name, NamespaceNode parent) : base(name)
        {
            FullName = fullName;
            Parent = parent;
        }

        /// <summary>
        /// 往命名空间中添加成员
        /// </summary>
        /// <param name="nodeBase">成员对象</param>
        public void AddChild(NodeBase nodeBase)
        {
            if (Children.ContainsKey(nodeBase.Name))
            {
                //命名空间下已经包含相同的成员了
                throw new Exception("xxx");
            }
            Children.Add(nodeBase.Name, nodeBase);
        }
        
        /// <summary>
        /// 根据名称获取成员节点对象, 如果找不到指定成员, 就返回 null
        /// </summary>
        /// <param name="name">成员名称</param>
        public NodeBase GetChild(string name)
        {
            Children.TryGetValue(name, out var nodeBase);
            return nodeBase;
        }
    }
}