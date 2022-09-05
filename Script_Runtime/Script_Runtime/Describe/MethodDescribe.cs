
public class MethodDescribe : Describe
{
    public bool IsPublic { get; set; } = true;

    public int ParamLength { get; set; } = 0;

    public bool IsDynamicParam { get; set; } = false;

    public MethodDescribe(string name) : base(name)
    {
        
    }
    
    
    
}