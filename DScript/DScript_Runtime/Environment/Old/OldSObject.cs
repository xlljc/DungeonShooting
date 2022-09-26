
public abstract class OldSObject : OldIObject
{
    public virtual OldSValue toString()
    {
        return new OldSValue("[object: object]");
    }

    public override string ToString()
    {
        return (string)toString().Value;
    }

    public virtual OldSValue __GetValue(string key)
    {
        switch (key)
        {
            case "toString":
                return new OldSValue(toString);
        }
        return OldSValue.Null;
    }

    public void __SetValue(string key, OldSValue value)
    {
        
    }

    public virtual OldSValue __InvokeMethod(string funcName, params OldSValue[] ps)
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