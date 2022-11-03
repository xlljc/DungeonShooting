using System.Collections.Generic;

namespace DScript.Compiler
{
    public class MethodNode : NodeBase
    {
        public Dictionary<int, FunctionNode> Functions = new Dictionary<int, FunctionNode>();

        public MethodNode(string name) : base(name)
        {
        }
    }
}