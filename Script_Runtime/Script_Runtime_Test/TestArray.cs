public class TestArray : SObject
{

    public SValue arr = SValue.Null;
    
    public TestArray()
    {
        arr = new SArray();
        arr.__Invoke__("add", 1);
        arr.__Invoke__("add", 2);
        arr.__Invoke__("add",  "3");
        Console.WriteLine("indexOf(2): " + arr.__Invoke__("indexOf", 2).Value);
        Console.WriteLine("indexOf('3'): " + arr.__Invoke__("indexOf", "3").Value);
        Console.WriteLine("indexOf('4'): " + arr.__Invoke__("indexOf", "4").Value);
        Console.WriteLine("delete(2): " + arr.__Invoke__("delete", 2).Value);
    }
    
    public override SValue __Invoke__(string funcName, params SValue[] ps)
    {
        return base.__Invoke__(funcName, ps);
    }

    public override SValue __GetValue__(string key)
    {
        return base.__GetValue__(key);
    }
}