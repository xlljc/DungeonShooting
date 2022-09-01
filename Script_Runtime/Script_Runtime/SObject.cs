
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

    public virtual SValue __GetValue__(string key)
    {
        return SValue.Null;
    }

    public virtual SValue __InvokeMethod__(string funcName, params SValue[] ps)
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