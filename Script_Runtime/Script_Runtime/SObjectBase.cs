
public abstract class SObjectBase
{
    public abstract SValue __GetValue__(string key);

    public abstract SValue __Invoke__(string funcName,  params SValue[] ps);
}