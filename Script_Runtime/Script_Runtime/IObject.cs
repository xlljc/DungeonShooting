
public interface IObject
{
    SValue toString();
    
    SValue __GetValue__(string key);

    SValue __InvokeMethod__(string funcName, params SValue[] ps);
}