
using System.Collections.Generic;

public class OldSArray : OldIObject
{
    private List<OldSValue> _arr;

    public OldSArray(params OldSValue[] values)
    {
        _arr = new List<OldSValue>(values);
    }

    public OldSValue length => _arr.Count;

    public OldSValue indexOf(OldSValue v)
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

    public OldSValue lastIndexOf(OldSValue v)
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

    public OldSValue add(OldSValue v)
    {
        _arr.Add(v);
        return OldSValue.Null;
    }

    public OldSValue delete(OldSValue index)
    {
        AssertUtils.AssertIsNumber(index);
        var i = (int)(double)index.Value;
        if (i < 0 || i >= _arr.Count)
        {
            return OldSValue.Null;
        }

        var v = _arr[i];
        _arr.RemoveAt(i);
        return v;
    }

    public OldSValue clear()
    {
        _arr.Clear();
        return OldSValue.Null;
    }

    public OldSValue __GetValue(string key)
    {
        switch (key)
        {
            case "length":
                return length;
        }
        return OldSValue.Null;
    }

    public virtual void __SetValue(string key, OldSValue value)
    {

    }


    public OldSValue __GetValue(int key)
    {
        if (key < 0 || key >= _arr.Count)
        {
            return OldSValue.Null;
        }

        return _arr[key];
    }

    public virtual OldSValue toString()
    {
        return new OldSValue("[object: object]");
    }

    public override string ToString()
    {
        return (string)toString().Value;
    }
    //
    public virtual OldSValue __InvokeMethod(string key, params OldSValue[] ps)
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
        return OldSValue.Null;
    }
    //
}