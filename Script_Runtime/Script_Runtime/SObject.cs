
public abstract class SObject : ICall
{
    public virtual SValue __GetValue__(string key)
    {
        return SValue.Null;
    }

    public virtual SValue __InvokeMethod__(string funcName,  params SValue[] ps)
    {
        return SValue.Null;
    }
}