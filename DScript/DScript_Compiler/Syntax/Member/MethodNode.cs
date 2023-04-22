using System.Collections.Generic;

namespace DScript.Compiler
{
    public class MethodNode : NodeBase
    {
        public Dictionary<int, FunctionNode> Functions = new Dictionary<int, FunctionNode>();

        public MethodNode(string name) : base(name)
        {
        }

        public void AddFunction(FunctionNode functionNode)
        {
            var len = functionNode.ParamLength;
            if (Functions.ContainsKey(len))
            {
                //已经声明了相同参数长度的函数 'xxx' 了
                throw new System.Exception("xxx");
            }
            else
            {
                Functions.Add(len, functionNode);
            }
        }
    }
}