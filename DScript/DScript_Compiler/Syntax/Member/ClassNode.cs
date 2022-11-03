using System;
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

        public readonly Dictionary<string, MemberData> Members = new Dictionary<string, MemberData>();

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
            if (!Members.TryGetValue(functionNode.Name, out var memberData))
            {
                var methodNode = new MethodNode(functionNode.Name);
                memberData = new MemberData(methodNode, false);
                methodNode.Functions.Add(functionNode.ParamLength, functionNode);
                Members.Add(methodNode.Name, memberData);
            }
            else
            {
                if (memberData.MemberType != MemberType.Method)
                {
                    //已经声明过 'xxx' 的成员了
                    throw new Exception("xxx");
                }

                var methodNode = (MethodNode)memberData.Node;
                if (!methodNode.Functions.TryAdd(functionNode.ParamLength, functionNode))
                {
                    //已经声明了相同参数长度的函数 'xxx' 了
                    throw new Exception("xxx");
                }
            }
        }
    }
}