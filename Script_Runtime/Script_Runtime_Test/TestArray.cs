using System;

public class TestArray : SObject
{

    public OldSValue arr = OldSValue.Null;
    
    public TestArray()
    {
        
    }
    
    public override OldSValue __InvokeMethod(string funcName, params OldSValue[] ps)
    {
        return base.__InvokeMethod(funcName, ps);
    }

    public override OldSValue __GetValue(string key)
    {
        return base.__GetValue(key);
    }
}