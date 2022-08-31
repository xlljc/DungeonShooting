
using System.Collections.Generic;

public class SArray : ICall
{

    private List<SValue> _arr;

    public SArray(params SValue[] values)
    {
        _arr = new List<SValue>(values);
    }

    public SValue length()
    {
        return _arr.Count;
    }

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

    public SValue __GetValue__(string key)
    {
        return SValue.Null;
    }

    public SValue __GetValue__(int key)
    {
        if (key < 0 || key >= _arr.Count)
        {
            return SValue.Null;
        }

        return _arr[key];
    }

    public SValue __Invoke__(string funcName, params SValue[] ps)
    {
        switch (funcName)
        {
            case "length":
                switch (ps.Length)
                {
                    case 0:
                        return length();
                }

                break;
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
        }

        return SValue.Null;
    }
}