using System.Collections.Generic;

namespace DScript.Compiler
{
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

        public void AddImport()
        {
            
        }
    }
}