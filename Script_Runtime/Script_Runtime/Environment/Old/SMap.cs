
public class SMap : IObject
{
    
    public SValue toString()
    {
        return new SValue("[object: object]");
    }

    public override string ToString()
    {
        return (string)toString().Value;
    }
    
    public SValue __GetValue(string key)
    {
        return SValue.Null;
    }

    public void __SetValue(string key, SValue value)
    {
        
    }

    public SValue __InvokeMethod(string funcName, params SValue[] ps)
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
        return SValue.Null;
    }
}