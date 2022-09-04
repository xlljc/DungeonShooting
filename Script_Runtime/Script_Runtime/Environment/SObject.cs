
public abstract class SObject : IObject
{
    public virtual SValue toString()
    {
        return new SValue("[object: object]");
    }

    public override string ToString()
    {
        return (string)toString().Value;
    }

    public virtual SValue __GetValue(string key)
    {
        switch (key)
        {
            case "toString":
                return new SValue(toString);
        }
        return SValue.Null;
    }

    public void __SetValue(string key, SValue value)
    {
        
    }

    public virtual SValue __InvokeMethod(string funcName, params SValue[] ps)
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