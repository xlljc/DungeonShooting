
using System.Collections.Generic;

public class SArray : IObject
{
    private List<SValue> _arr;

    public SArray(params SValue[] values)
    {
        _arr = new List<SValue>(values);
    }

    public SValue length => _arr.Count;

    public SValue indexOf(SValue v)
    {
        for (int i = 0; i < _arr.Count; i++)
        {
            if (_arr[i] == v)
            {
                return i;
            }
        }
        return -1;
    }

    public SValue lastIndexOf(SValue v)
    {
        for (int i = _arr.Count - 1; i >= 0; i--)
        {
            if (_arr[i] == v)
            {
                return i;
            }
        }
        return -1;
    }

    public SValue add(SValue v)
    {
        _arr.Add(v);
        return SValue.Null;
    }

    public SValue delete(SValue index)
    {
        AssertUtils.AssertIsNumber(index);
        var i = (int)(double)index.Value;
        if (i < 0 || i >= _arr.Count)
        {
            return SValue.Null;
        }

        var v = _arr[i];
        _arr.RemoveAt(i);
        return v;
    }

    public SValue clear()
    {
        _arr.Clear();
        return SValue.Null;
    }

    public SValue __GetValue(string key)
    {
        switch (key)
        {
            case "length":
                return length;
        }
        return SValue.Null;
    }

    public virtual void __SetValue(string key, SValue value)
    {

    }


    public SValue __GetValue(int key)
    {
        if (key < 0 || key >= _arr.Count)
        {
            return SValue.Null;
        }

        return _arr[key];
    }

    public virtual SValue toString()
    {
        return new SValue("[object: object]");
    }

    public override string ToString()
    {
        return (string)toString().Value;
    }
    
    public virtual SValue __InvokeMethod(string key, params SValue[] ps)
    {
        switch (key)
        {
            case "length":
                return length.Invoke(ps);
            case "indexOf":
                switch (ps.Length)
                {
                    case 1:
                        return indexOf(ps[0]);
                }
                break;
            case "lastIndexOf":
                switch (ps.Length)
                {
                    case 1:
                        return lastIndexOf(ps[0]);
                }
                break;
            case "add":
                switch (ps.Length)
                {
                    case 1:
                        return add(ps[0]);
                }
                break;
            case "delete":
                switch (ps.Length)
                {
                    case 1:
                        return delete(ps[0]);
                }
                break;
            case "clear":
                switch (ps.Length)
                {
                    case 0:
                        return clear();
                }
                break;
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