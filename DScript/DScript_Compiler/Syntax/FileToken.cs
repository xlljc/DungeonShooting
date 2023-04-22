using System;
using System.Collections.Generic;

namespace DScript.Compiler
{
    /// <summary>
    /// 对于单个文件的声明信息
    /// </summary>
    public class FileToken
    {
        public FileToken(string path, Token[] tokens, SyntaxTree tree)
        {
            Path = path;
            Tokens = tokens;
            SyntaxTree = tree;
        }
        
        /// <summary>
        /// 该文件的路径
        /// </summary>
        public string Path;
        
        /// <summary>
        /// 当前文件所包含的 token
        /// </summary>
        public Token[] Tokens;
        
        /// <summary>
        /// 语法树对象
        /// </summary>
        public SyntaxTree SyntaxTree;

        /// <summary>
        /// 导入的简化名称
        /// </summary>
        public Dictionary<string, ImportNode> ImportNode = new Dictionary<string, ImportNode>();
        /// <summary>
        /// 该文件中声明的命名空间
        /// </summary>
        public NamespaceNode NamespaceNode;
        /// <summary>
        /// 该文件中定义的成员
        /// </summary>
        public Dictionary<string, NodeBase> DefineNode = new Dictionary<string, NodeBase>();
        
        private bool _hasNamespace = false;
        private bool _hasClass = false;
        private ClassNode _classNode;
        private bool _hasFunction = false;
        
        /// <summary>
        /// 添加该文件中的导入语句
        /// </summary>
        public void AddImport(ImportNode importNode)
        {
            if (_hasNamespace) //已经存在命名空间
            {
                //导入语句必须在声明命名空间之前
                throw new System.Exception("xxx");
            }
            
            if (!ImportNode.ContainsKey(importNode.Name))
            {
                ImportNode.Add(importNode.Name, importNode);
            }
            else
            {
                //导入了相同的名称 {nextToken.Code}
                throw new System.Exception("xxx");
            }
        }

        /// <summary>
        /// 设置该文件内声明的命名空间
        /// </summary>
        public void SetNamespace(NamespaceNode namespaceNode)
        {
            if (_hasClass) //已经声明类了
            {
                //声明命名空间必须写在声明类之前
                throw new System.Exception("xxx");
            }
            else if (_hasNamespace) //已经声明命名空间了
            {
                //该文件已经声明命名空间了, 不能重复声明
                throw new System.Exception("xxx");
            }

            NamespaceNode = namespaceNode;
            _hasNamespace = true;
        }

        /// <summary>
        /// 获取或创建该文件下的命名空间
        /// </summary>
        public NamespaceNode GetOrCreateNamespace()
        {
            if (!_hasNamespace) //如果没有声明命名空间, 就使用global命名空间
            {
                NamespaceNode = SyntaxTree.Root;
                _hasNamespace = true;
            }

            return NamespaceNode;
        }

        /// <summary>
        /// 设置该类包含的类
        /// </summary>
        public void SetClass(ClassNode classNode)
        {
            if (_hasFunction) //已经声明过函数
            {
                //声明类必须写在声明函数之前
                throw new System.Exception("xxx");
            }
            else if (_hasClass) //已经声明过类了
            {
                //该文件已经声明类了, 不能重复声明
                throw new System.Exception("xxx");
            }

            var namespaceNode = GetOrCreateNamespace();
            classNode.SetNamespace(namespaceNode);

            _classNode = classNode;
            _hasClass = true;
            AddDefineNode(classNode);
        }

        /// <summary>
        /// 添加声明函数
        /// </summary>
        public void AddFunction(FunctionNode functionNode)
        {
            if (_hasClass) //放入类中
            {
                _classNode.AddFunction(functionNode);
            }
            else //放入命名空间中
            {
                _hasFunction = true;
                var namespaceNode = GetOrCreateNamespace();

                var nodeBase = namespaceNode.GetChild(functionNode.Name);
                if (nodeBase == null)
                {
                    var methodNode = new MethodNode(functionNode.Name);
                    methodNode.AddFunction(functionNode);
                    namespaceNode.AddChild(methodNode);
                }
                else if (nodeBase is MethodNode methodNode)
                {
                    methodNode.AddFunction(functionNode);
                }
                else
                {
                    //命名空间中已经声明过 'xxx' 的成员了
                    throw new System.Exception("xxx");
                }

                AddDefineNode(functionNode);
            }
        }

        /// <summary>
        /// 添加定义成员
        /// </summary>
        private void AddDefineNode(NodeBase nodeBase)
        {
            if (DefineNode.ContainsKey(nodeBase.Name))
            {
                //重复定义名称: name
                throw new System.Exception("xxx");
            }
            DefineNode.Add(nodeBase.Name, nodeBase);
        }
    }
}