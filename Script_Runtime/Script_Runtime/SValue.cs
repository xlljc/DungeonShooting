
using System;

public struct SValue
{
    public enum SValueType
    {
        Null,
        Number,
        BooleanTrue,
        BooleanFalse,
        String,
        Object,
    }

    private enum SObjectType
    {
        SObjectBase,
        Array,
        Other
    }
    
    private SValueType Type;

    public object Value;

    private SObjectType objectType;

    public SValue(SObjectBase v)
    {
        Type = SValueType.Object;
        Value = v;
        objectType = SObjectType.SObjectBase;
    }
    
    public SValue(SValue v)
    {
        Type = v.Type;
        Value = v.Value;
        objectType = v.objectType;
    }

    public SValue(object v, SValueType type)
    {
        Type = type;
        objectType = SObjectType.Other;
        if (type == SValueType.Number)
        {
            Value = (double)v;
        }
        else
        {
            Value = v;
            if (type == SValueType.Object)
            {
                if (v is SObjectBase)
                {
                    objectType = SObjectType.SObjectBase;
                }
            }
        }
    }

    public SValue __GetValue__(string key)
    {
        if (objectType == SObjectType.SObjectBase)
        {
            return ((SObjectBase)Value).__GetValue__(key);
        }
        return new SValue();
    }

    public SValue __GetValue__(SValue key)
    {
        return new SValue();
    }

    public void __SetValue__()
    {
        
    }
    
    public SValue __Invoke__(string key, params SValue[] ps)
    {
        if (objectType == SObjectType.SObjectBase)
        {
            return ((SObjectBase)Value).__Invoke__(key, ps);
        }
        return new SValue();
    }


    public static implicit operator SValue(double value)
    {
        return new SValue(value, SValueType.Number);
    }

    public static implicit operator SValue(bool value)
    {
        if (value)
            return new SValue(1, SValueType.BooleanTrue);
        return new SValue(0, SValueType.BooleanFalse);
    }

    public static implicit operator SValue(string value)
    {
        return new SValue(value, SValueType.String);
    }
    
    public static implicit operator SValue(SObjectBase value)
    {
        return new SValue(value);
    }

    public static SValue operator +(SValue v1, double num)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return new SValue((double)v1.Value + num, SValueType.Number);
            case SValueType.String:
                return new SValue(v1.Value + num.ToString(), SValueType.String);
            case SValueType.Object:
            case SValueType.Null:
                return new SValue(double.NaN, SValueType.Number);
            case SValueType.BooleanTrue:
                if (1 + num > 0)
                    return new SValue(1, SValueType.BooleanTrue);
                return new SValue(0, SValueType.BooleanFalse);
            case SValueType.BooleanFalse:
                if (num > 0)
                    return new SValue(1, SValueType.BooleanTrue);
                return new SValue(0, SValueType.BooleanFalse);
        }

        return new SValue(null, SValueType.Null);
    }

    public static SValue operator +(SValue v1, SValue v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new SValue((double)v1.Value + (double)v2.Value, SValueType.Number);
                    case SValueType.String:
                        return new SValue(v1.Value.ToString() + v2.Value, SValueType.String);
                    case SValueType.Object:
                    case SValueType.Null:
                        return new SValue(double.NaN, SValueType.Number);
                    case SValueType.BooleanTrue:
                        return new SValue((double)v1.Value + 1, SValueType.Number);
                    case SValueType.BooleanFalse:
                        return new SValue(v1.Value, SValueType.Number);
                }

                break;
            case SValueType.String:
                switch (v2.Type)
                {
                    case SValueType.Number:
                    case SValueType.Object:
                        return new SValue(v1.Value + v2.Value.ToString(), SValueType.String);
                    case SValueType.String:
                        return new SValue(v1.Value + (string)v2.Value, SValueType.String);
                    case SValueType.Null:
                        return new SValue(v1.Value + "null", SValueType.String);
                    case SValueType.BooleanTrue:
                        return new SValue(v1.Value + "true", SValueType.String);
                    case SValueType.BooleanFalse:
                        return new SValue(v1.Value + "false", SValueType.String);
                }

                break;
            case SValueType.Null:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new SValue(double.NaN, SValueType.Number);
                    case SValueType.Object:
                    case SValueType.String:
                        return new SValue("null" + v2.Value, SValueType.String);
                    case SValueType.Null:
                        return new SValue(null, SValueType.Null);
                    case SValueType.BooleanTrue:
                        return new SValue("nulltrue", SValueType.String);
                    case SValueType.BooleanFalse:
                        return new SValue("nullfalse", SValueType.String);
                }

                break;
            case SValueType.Object:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new SValue(double.NaN, SValueType.Number);
                    case SValueType.Object:
                        return new SValue(v1.Value.ToString() + v2.Value, SValueType.String);
                    case SValueType.String:
                        return new SValue(v1.Value + (string)v2.Value, SValueType.String);
                    case SValueType.Null:
                        return new SValue("null" + v2.Value, SValueType.Null);
                    case SValueType.BooleanTrue:
                        return new SValue("true" + v2.Value, SValueType.String);
                    case SValueType.BooleanFalse:
                        return new SValue("false" + v2.Value, SValueType.String);
                }

                break;
            case SValueType.BooleanTrue:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new SValue((double)v1.Value + 1, SValueType.Number);
                    case SValueType.Object:
                    case SValueType.String:
                        return new SValue("true" + v2.Value, SValueType.String);
                    case SValueType.Null:
                        return new SValue("truenull", SValueType.Null);
                    case SValueType.BooleanTrue:
                    case SValueType.BooleanFalse:
                        return new SValue(1, SValueType.BooleanTrue);
                }

                break;
            case SValueType.BooleanFalse:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new SValue(v1.Value, SValueType.Number);
                    case SValueType.Object:
                    case SValueType.String:
                        return new SValue("false" + v2.Value, SValueType.String);
                    case SValueType.Null:
                        return new SValue("falsenull", SValueType.Null);
                    case SValueType.BooleanTrue:
                        return new SValue(1, SValueType.BooleanTrue);
                    case SValueType.BooleanFalse:
                        return new SValue(0, SValueType.BooleanFalse);
                }

                break;
        }

        return new SValue(null, SValueType.Null);
    }

    public static SValue operator -(SValue v1, double num)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return new SValue((double)v1.Value - num, SValueType.Number);
            case SValueType.String:
                return new SValue(double.NaN, SValueType.Number);
            case SValueType.Object:
            case SValueType.Null:
                return new SValue(double.NaN, SValueType.Number);
            case SValueType.BooleanTrue:
                if (1 - num > 0)
                    return new SValue(1, SValueType.BooleanTrue);
                return new SValue(0, SValueType.BooleanFalse);
            case SValueType.BooleanFalse:
                if (num <= 0)
                    return new SValue(1, SValueType.BooleanTrue);
                return new SValue(0, SValueType.BooleanFalse);
        }

        return new SValue(null, SValueType.Null);
    }
    
    public static SValue operator -(SValue v1, SValue v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new SValue((double)v1.Value - (double)v2.Value, SValueType.Number);
                    case SValueType.String:
                    case SValueType.Object:
                    case SValueType.Null:
                        return new SValue(double.NaN, SValueType.Number);
                    case SValueType.BooleanTrue:
                        return new SValue((double)v1.Value - 1, SValueType.Number);
                    case SValueType.BooleanFalse:
                        return new SValue(v1.Value, SValueType.Number);
                }

                break;
            case SValueType.String:
                switch (v2.Type)
                {
                    case SValueType.Number:
                    case SValueType.Object:
                    case SValueType.String:
                    case SValueType.Null:
                    case SValueType.BooleanTrue:
                    case SValueType.BooleanFalse:
                        return new SValue(double.NaN, SValueType.Number);
                }

                break;
            case SValueType.Null:
                switch (v2.Type)
                {
                    case SValueType.Number:
                    case SValueType.Object:
                    case SValueType.String:
                    case SValueType.Null:
                    case SValueType.BooleanTrue:
                    case SValueType.BooleanFalse:
                        return new SValue(double.NaN, SValueType.Number);
                }

                break;
            case SValueType.Object:
                switch (v2.Type)
                {
                    case SValueType.Number:
                    case SValueType.Object:
                    case SValueType.String:
                    case SValueType.Null:
                    case SValueType.BooleanTrue:
                    case SValueType.BooleanFalse:
                        return new SValue(double.NaN, SValueType.Number);
                }

                break;
            case SValueType.BooleanTrue:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        if (1 - (double)v2.Value > 0)
                            return new SValue(1, SValueType.BooleanTrue);
                        return new SValue(0, SValueType.BooleanFalse);
                    case SValueType.Object:
                    case SValueType.String:
                    case SValueType.Null:
                    case SValueType.BooleanTrue:
                        return new SValue(0, SValueType.BooleanFalse);
                    case SValueType.BooleanFalse:
                        return new SValue(1, SValueType.BooleanTrue);
                }

                break;
            case SValueType.BooleanFalse:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        if ((double)v2.Value <= 0)
                            return new SValue(1, SValueType.BooleanTrue);
                        return new SValue(0, SValueType.BooleanFalse);
                    case SValueType.Object:
                    case SValueType.String:
                    case SValueType.Null:
                    case SValueType.BooleanTrue:
                    case SValueType.BooleanFalse:
                        return new SValue(0, SValueType.BooleanFalse);
                }

                break;
        }

        return new SValue(null, SValueType.Null);
    }


}
