
public class SMap : ICall
{
    public SValue __GetValue__(string key)
    {
        return SValue.Null;
    }

    public SValue __Invoke__(string funcName, params SValue[] ps)
    {
        return SValue.Null;
    }
}