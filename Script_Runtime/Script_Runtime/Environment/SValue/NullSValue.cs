
using System;

internal class NullSValue : ISValue
{

    public static NullSValue Instance
    {
        get
        {
            if (_init == false)
            {
                _init = true;
                _inst = new NullSValue();
            }

            return _inst;
        }
    }

    private static NullSValue _inst;
    private static bool _init;
    
    private NullSValue()
    {
    }
    
    public SValueType GetValueType()
    {
        return SValueType.Null;
    }

    public SDataType GetDataType()
    {
        return SDataType.Null;
    }

    public object GetValue()
    {
        throw new NullReferenceException();
    }

    public ISValue Invoke(params ISValue[] ps)
    {
        throw new NullReferenceException();
    }

    public ISValue InvokeMethod(string key, params ISValue[] ps)
    {
        throw new NullReferenceException();
    }

    public ISValue GetProperty(string key)
    {
        throw new NullReferenceException();
    }

    public void SetProperty(string key, ISValue value)
    {
        throw new NullReferenceException();
    }
}