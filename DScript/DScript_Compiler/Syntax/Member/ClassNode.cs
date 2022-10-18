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
        
        public readonly Dictionary<string, Dictionary<int, FunctionNode>> Functions = new Dictionary<string, Dictionary<int, FunctionNode>>();
        public readonly Dictionary<string, VariableNode> Variables = new Dictionary<string, VariableNode>();
        public readonly Dictionary<string, PropertyNode> Properties = new Dictionary<string, PropertyNode>();

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
            if (!Functions.TryGetValue(functionNode.Name, out var dictionary))
            {
                dictionary = new Dictionary<int, FunctionNode>();
                dictionary.Add(functionNode.ParamLength, functionNode);
                Functions.Add(functionNode.Name, dictionary);
            }
            else
            {
                if (!dictionary.TryAdd(functionNode.ParamLength, functionNode))
                {
                    //已经声明了相同参数长度的函数 'xxx' 了
                    throw new Exception("xxx");
                }
            }
        }
    }
}