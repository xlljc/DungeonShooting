using System;

public class TestArray : SObject
{

    public SValue arr = SValue.Null;
    
    public TestArray()
    {
        
    }
    
    public override SValue __InvokeMethod__(string funcName, params SValue[] ps)
    {
        return base.__InvokeMethod__(funcName, ps);
    }

    public override SValue __GetValue__(string key)
    {
        return base.__GetValue__(key);
    }
}