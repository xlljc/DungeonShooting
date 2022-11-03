namespace DScript.Compiler
{
    public enum MemberType
    {
        Variable,
        Property,
        Method,
    }
    
    public class MemberData
    {
        public NodeBase Node;
        public bool IsStatic;
        public MemberType MemberType;
        
        public MemberData(VariableNode node, bool isStatic)
        {
            Node = node;
            IsStatic = isStatic;
            MemberType = MemberType.Variable;
        }
        
        public MemberData(PropertyNode node, bool isStatic)
        {
            Node = node;
            IsStatic = isStatic;
            MemberType = MemberType.Property;
        }
        
        public MemberData(MethodNode node, bool isStatic)
        {
            Node = node;
            IsStatic = isStatic;
            MemberType = MemberType.Method;
        }
    }
}