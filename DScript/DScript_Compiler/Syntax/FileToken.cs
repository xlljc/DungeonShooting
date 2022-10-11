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
        public Dictionary<string, ImportNode> Import = new Dictionary<string, ImportNode>();
        /// <summary>
        /// 该文件中声明的命名空间
        /// </summary>
        public NamespaceNode NamespaceNode;

        public void AddImport()
        {
            
        }
    }
}