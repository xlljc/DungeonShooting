namespace DScript.Compiler
{
    /// <summary>
    /// 函数节点
    /// </summary>
    public class FunctionNode : NodeBase
    {
        public Token[] Body;

        public int ParamLength = 0;
        
        public FunctionNode(string name, Token[] body) : base(name)
        {
            Body = body;
        }
    }
}