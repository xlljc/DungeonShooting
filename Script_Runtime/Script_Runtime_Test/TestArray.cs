using System;

public class TestArray : SObject
{

    public SValue arr = SValue.Null;
    
    public TestArray()
    {
        
    }
    
    public override SValue __InvokeMethod(string funcName, params SValue[] ps)
    {
        return base.__InvokeMethod(funcName, ps);
    }

    public override SValue __GetValue(string key)
    {
        return base.__GetValue(key);
    }
}