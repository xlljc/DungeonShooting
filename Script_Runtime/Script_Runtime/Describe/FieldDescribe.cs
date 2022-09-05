
public class FieldDescribe : Describe
{
    public bool IsPublic { get; set; } = true;

    public FieldDescribe(string name) : base(name)
    {
        
    }
    
}