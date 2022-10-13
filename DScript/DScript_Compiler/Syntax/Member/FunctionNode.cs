namespace DScript.Compiler
{
    /// <summary>
    /// 函数节点
    /// </summary>
    public class FunctionNode : NodeBase
    {
        public Token[] Body;
        
        public FunctionNode(string name, Token[] body) : base(name)
        {
            Body = body;
        }
    }
}