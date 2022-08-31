
public abstract class SObjectBase
{
    public virtual SValue __GetValue__(string key)
    {
        return new SValue();
    }

    public virtual SValue __Invoke__(string funcName,  params SValue[] ps)
    {
        return new SValue();
    }
}