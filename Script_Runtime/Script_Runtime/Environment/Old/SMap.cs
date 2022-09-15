
public class SMap : IObject
{
    
    public OldSValue toString()
    {
        return new OldSValue("[object: object]");
    }

    public override string ToString()
    {
        return (string)toString().Value;
    }
    
    public OldSValue __GetValue(string key)
    {
        return OldSValue.Null;
    }

    public void __SetValue(string key, OldSValue value)
    {
        
    }

    public OldSValue __InvokeMethod(string funcName, params OldSValue[] ps)
    {
        switch (funcName)
        {
            case "toString":
                switch (ps.Length)
                {
                    case 0:
                        return toString();
                }

                break;
        }
        return OldSValue.Null;
    }
}