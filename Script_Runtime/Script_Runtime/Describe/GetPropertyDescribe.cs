
public class GetPropertyDescribe : Describe
{
    public bool IsPublic { get; set; } = true;
    
    public GetPropertyDescribe(string name) : base(name)
    {
        
    }
}