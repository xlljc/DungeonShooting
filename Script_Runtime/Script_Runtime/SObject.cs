
public abstract class SObject : ICall
{
    public virtual SValue __GetValue__(string key)
    {
        return SValue.Null;
    }

    public virtual SValue __Invoke__(string funcName,  params SValue[] ps)
    {
        return SValue.Null;
    }
}