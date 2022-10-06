namespace DScript.Compiler
{
    public abstract class NodeBase
    {
        public readonly string Name;

        public NodeBase(string name)
        {
            Name = name;
        }
    }
}