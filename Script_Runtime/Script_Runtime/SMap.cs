
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
    
    public SValue __GetValue__(string key)
    {
        return SValue.Null;
    }

    public SValue __InvokeMethod__(string funcName, params SValue[] ps)
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