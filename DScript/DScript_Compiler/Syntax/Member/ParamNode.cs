namespace DScript.Compiler
{
    public class ParamNode : NodeBase
    {
        public string TypeName;
        
        public ParamNode(string name, string typeName) : base(name)
        {
            TypeName = typeName;
        }
    }
}