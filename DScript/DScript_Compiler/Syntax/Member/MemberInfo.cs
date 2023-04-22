namespace DScript.Compiler
{
    /// <summary>
    /// 成员类型
    /// </summary>
    public enum MemberType
    {
        /// <summary>
        /// 变量
        /// </summary>
        Variable,
        /// <summary>
        /// 属性
        /// </summary>
        Property,
        /// <summary>
        /// 函数
        /// </summary>
        Method,
    }
    
    public class MemberInfo
    {
        /// <summary>
        /// 成员实例
        /// </summary>
        public NodeBase Node;
        /// <summary>
        /// 是否是静态
        /// </summary>
        public bool IsStatic;
        /// <summary>
        /// 成员类型
        /// </summary>
        public MemberType MemberType;
        
        public MemberInfo(VariableNode node, bool isStatic)
        {
            Node = node;
            IsStatic = isStatic;
            MemberType = MemberType.Variable;
        }
        
        public MemberInfo(PropertyNode node, bool isStatic)
        {
            Node = node;
            IsStatic = isStatic;
            MemberType = MemberType.Property;
        }
        
        public MemberInfo(MethodNode node, bool isStatic)
        {
            Node = node;
            IsStatic = isStatic;
            MemberType = MemberType.Method;
        }
    }
}