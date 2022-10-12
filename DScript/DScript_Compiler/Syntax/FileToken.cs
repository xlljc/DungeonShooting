using System;
using System.Collections.Generic;

namespace DScript.Compiler
{
    /// <summary>
    /// 对于单个文件的声明信息
    /// </summary>
    public class FileToken
    {
        public FileToken(string path, Token[] tokens)
        {
            Path = path;
            Tokens = tokens;
        }
        
        public string Path;
        public Token[] Tokens;

        /// <summary>
        /// 导入的简化名称
        /// </summary>
        private Dictionary<string, ImportNode> Import = new Dictionary<string, ImportNode>();
        /// <summary>
        /// 该文件中声明的命名空间
        /// </summary>
        private NamespaceNode NamespaceNode;

        private bool _hasImport = false;
        private bool _hasNamespace = false;
        private bool _hasClass = false;
        private bool _hasFunction = false;
        
        public void AddImport(string importName, ImportNode importNode)
        {
            if (_hasNamespace) //已经存在命名空间
            {
                //导入语句必须在声明命名空间之前
                throw new Exception("xxx");
            }
            
            if (!Import.ContainsKey(importName))
            {
                Import.Add(importName, importNode);
                _hasImport = true;
            }
            else
            {
                //导入了相同的名称 {nextToken.Code}
                throw new Exception("xxx");
            }
        }

        public void SetNamespace(NamespaceNode namespaceNode)
        {
            if (_hasClass) //已经声明类了
            {
                //声明命名空间必须写在声明类之前
                throw new Exception("xxx");
            }
            else if (_hasNamespace) //已经声明命名空间了
            {
                //该文件已经声明命名空间了, 不能重复声明
                throw new Exception("xxx");
            }

            NamespaceNode = namespaceNode;
            _hasNamespace = true;
        }
    }
}