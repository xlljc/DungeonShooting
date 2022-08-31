
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

    public SValue(SValue v)
    {
        Type = v.Type;
        Value = v.Value;
        objectType = v.objectType;
    }

    public SValue(SObjectBase v)
    {
        if (v == null)
        {
            Type = SValueType.Null;
        }
        else
        {
            Type = SValueType.Object;
        }
        Value = v;
        objectType = SObjectType.SObjectBase;
    }

    public SValue(double v)
    {
        Type = SValueType.Number;
        Value = v;
        objectType = SObjectType.Other;
    }

    public SValue(string v)
    {
        Type = SValueType.String;
        Value = v;
        objectType = SObjectType.Other;
    }

    public static SValue Null
    {
        get
        {
            if (!_initNullValue)
            {
                _initNullValue = true;
                _null.Type = SValueType.Null;
            }
            return _null;
        }
    }
    private static bool _initNullValue;
    private static SValue _null;

    public static SValue True
    {
        get
        {
            if (!_initTrueValue)
            {
                _initTrueValue = true;
                _true.Type = SValueType.BooleanTrue;
                _true.Value = 1;
            }
            return _true;
        }
    }
    private static bool _initTrueValue;
    private static SValue _true;

    public static SValue False
    {
        get
        {
            if (!_initFalseValue)
            {
                _initFalseValue = true;
                _false.Type = SValueType.BooleanFalse;
                _false.Value = 0;
            }
            return _false;
        }
    }
    private static bool _initFalseValue;
    private static SValue _false;

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
        return new SValue(value);
    }

    public static implicit operator SValue(bool value)
    {
        return value ? True : False;
    }

    public static implicit operator SValue(string value)
    {
        return new SValue(value);
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
                return new SValue((double)v1.Value + num);
            case SValueType.String:
                return new SValue(v1.Value + num.ToString());
            case SValueType.Object:
            case SValueType.Null:
                return new SValue(double.NaN);
            case SValueType.BooleanTrue:
                if (1 + num > 0)
                    return True;
                return False;
            case SValueType.BooleanFalse:
                if (num > 0)
                    return True;
                return False;
        }

        return Null;
    }

    public static SValue operator +(SValue v1, SValue v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new SValue((double)v1.Value + (double)v2.Value);
                    case SValueType.String:
                        return new SValue(v1.Value.ToString() + v2.Value);
                    case SValueType.Object:
                    case SValueType.Null:
                        return new SValue(double.NaN);
                    case SValueType.BooleanTrue:
                        return new SValue((double)v1.Value + 1);
                    case SValueType.BooleanFalse:
                        return new SValue(v1);
                }

                break;
            case SValueType.String:
                switch (v2.Type)
                {
                    case SValueType.Number:
                    case SValueType.Object:
                        return new SValue(v1.Value + v2.Value.ToString());
                    case SValueType.String:
                        return new SValue(v1.Value + (string)v2.Value);
                    case SValueType.Null:
                        return new SValue(v1.Value + "null");
                    case SValueType.BooleanTrue:
                        return new SValue(v1.Value + "true");
                    case SValueType.BooleanFalse:
                        return new SValue(v1.Value + "false");
                }

                break;
            case SValueType.Null:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new SValue(double.NaN);
                    case SValueType.Object:
                    case SValueType.String:
                        return new SValue("null" + v2.Value);
                    case SValueType.Null:
                        return Null;
                    case SValueType.BooleanTrue:
                        return new SValue("nulltrue");
                    case SValueType.BooleanFalse:
                        return new SValue("nullfalse");
                }

                break;
            case SValueType.Object:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new SValue(double.NaN);
                    case SValueType.Object:
                        return new SValue(v1.Value.ToString() + v2.Value);
                    case SValueType.String:
                        return new SValue(v1.Value + (string)v2.Value);
                    case SValueType.Null:
                        return new SValue("null" + v2.Value);
                    case SValueType.BooleanTrue:
                        return new SValue("true" + v2.Value);
                    case SValueType.BooleanFalse:
                        return new SValue("false" + v2.Value);
                }

                break;
            case SValueType.BooleanTrue:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new SValue((double)v1.Value + 1);
                    case SValueType.Object:
                    case SValueType.String:
                        return new SValue("true" + v2.Value);
                    case SValueType.Null:
                        return new SValue("truenull");
                    case SValueType.BooleanTrue:
                    case SValueType.BooleanFalse:
                        return True;
                }

                break;
            case SValueType.BooleanFalse:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new SValue(v1);
                    case SValueType.Object:
                    case SValueType.String:
                        return new SValue("false" + v2.Value);
                    case SValueType.Null:
                        return new SValue("falsenull");
                    case SValueType.BooleanTrue:
                        return True;
                    case SValueType.BooleanFalse:
                        return False;
                }

                break;
        }

        return Null;
    }

    public static SValue operator -(SValue v1, double num)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return new SValue((double)v1.Value - num);
            case SValueType.String:
                return new SValue(double.NaN);
            case SValueType.Object:
            case SValueType.Null:
                return new SValue(double.NaN);
            case SValueType.BooleanTrue:
                if (1 - num > 0)
                    return True;
                return False;
            case SValueType.BooleanFalse:
                if (num <= 0)
                    return True;
                return False;
        }

        return Null;
    }

    public static SValue operator -(SValue v1, SValue v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new SValue((double)v1.Value - (double)v2.Value);
                    case SValueType.String:
                    case SValueType.Object:
                    case SValueType.Null:
                        return new SValue(double.NaN);
                    case SValueType.BooleanTrue:
                        return new SValue((double)v1.Value - 1);
                    case SValueType.BooleanFalse:
                        return new SValue(v1);
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
                        return new SValue(double.NaN);
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
                        return new SValue(double.NaN);
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
                        return new SValue(double.NaN);
                }

                break;
            case SValueType.BooleanTrue:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        if (1 - (double)v2.Value > 0)
                            return True;
                        return False;
                    case SValueType.Object:
                    case SValueType.String:
                    case SValueType.Null:
                    case SValueType.BooleanTrue:
                        return False;
                    case SValueType.BooleanFalse:
                        return True;
                }

                break;
            case SValueType.BooleanFalse:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        if ((double)v2.Value <= 0)
                            return True;
                        return False;
                    case SValueType.Object:
                    case SValueType.String:
                    case SValueType.Null:
                    case SValueType.BooleanTrue:
                    case SValueType.BooleanFalse:
                        return False;
                }

                break;
        }

        return Null;
    }


}
