
public interface ICall
{
    SValue __GetValue__(string key);

    SValue __Invoke__(string funcName, params SValue[] ps);
}