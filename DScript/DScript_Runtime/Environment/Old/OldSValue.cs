
using System;

public struct OldSValue
{

    #region 定义函数委托

    public delegate OldSValue Function_0();

    public delegate OldSValue Function_1(OldSValue p0);

    public delegate OldSValue Function_2(OldSValue p0, OldSValue p1);

    public delegate OldSValue Function_3(OldSValue p0, OldSValue p1, OldSValue p2);

    public delegate OldSValue Function_4(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3);

    public delegate OldSValue Function_5(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3, OldSValue p4);

    public delegate OldSValue Function_6(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3, OldSValue p4, OldSValue p5);

    public delegate OldSValue Function_7(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3, OldSValue p4, OldSValue p5, OldSValue p6);

    public delegate OldSValue Function_8(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3, OldSValue p4, OldSValue p5, OldSValue p6,
        OldSValue p7);

    public delegate OldSValue Function_9(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3, OldSValue p4, OldSValue p5, OldSValue p6,
        OldSValue p7, OldSValue p8);

    public delegate OldSValue Function_10(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3, OldSValue p4, OldSValue p5, OldSValue p6,
        OldSValue p7, OldSValue p8, OldSValue p9);

    public delegate OldSValue Function_11(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3, OldSValue p4, OldSValue p5, OldSValue p6,
        OldSValue p7, OldSValue p8, OldSValue p9, OldSValue p10);

    public delegate OldSValue Function_12(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3, OldSValue p4, OldSValue p5, OldSValue p6,
        OldSValue p7, OldSValue p8, OldSValue p9, OldSValue p10, OldSValue p11);

    public delegate OldSValue Function_13(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3, OldSValue p4, OldSValue p5, OldSValue p6,
        OldSValue p7, OldSValue p8, OldSValue p9, OldSValue p10, OldSValue p11, OldSValue p12);

    public delegate OldSValue Function_14(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3, OldSValue p4, OldSValue p5, OldSValue p6,
        OldSValue p7, OldSValue p8, OldSValue p9, OldSValue p10, OldSValue p11, OldSValue p12, OldSValue p13);

    public delegate OldSValue Function_15(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3, OldSValue p4, OldSValue p5, OldSValue p6,
        OldSValue p7, OldSValue p8, OldSValue p9, OldSValue p10, OldSValue p11, OldSValue p12, OldSValue p13, OldSValue p14);

    public delegate OldSValue Function_16(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3, OldSValue p4, OldSValue p5, OldSValue p6,
        OldSValue p7, OldSValue p8, OldSValue p9, OldSValue p10, OldSValue p11, OldSValue p12, OldSValue p13, OldSValue p14, OldSValue p15);

    public delegate OldSValue Function_0_Params(params OldSValue[] ps);

    public delegate OldSValue Function_1_Params(OldSValue p0, params OldSValue[] ps);

    public delegate OldSValue Function_2_Params(OldSValue p0, OldSValue p1, params OldSValue[] ps);

    public delegate OldSValue Function_3_Params(OldSValue p0, OldSValue p1, OldSValue p2, params OldSValue[] ps);

    public delegate OldSValue Function_4_Params(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3, params OldSValue[] ps);

    public delegate OldSValue Function_5_Params(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3, OldSValue p4, params OldSValue[] ps);

    public delegate OldSValue Function_6_Params(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3, OldSValue p4, OldSValue p5,
        params OldSValue[] ps);

    public delegate OldSValue Function_7_Params(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3, OldSValue p4, OldSValue p5,
        OldSValue p6, params OldSValue[] ps);

    public delegate OldSValue Function_8_Params(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3, OldSValue p4, OldSValue p5,
        OldSValue p6, OldSValue p7, params OldSValue[] ps);

    public delegate OldSValue Function_9_Params(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3, OldSValue p4, OldSValue p5,
        OldSValue p6, OldSValue p7, OldSValue p8, params OldSValue[] ps);

    public delegate OldSValue Function_10_Params(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3, OldSValue p4, OldSValue p5,
        OldSValue p6, OldSValue p7, OldSValue p8, OldSValue p9, params OldSValue[] ps);

    public delegate OldSValue Function_11_Params(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3, OldSValue p4, OldSValue p5,
        OldSValue p6, OldSValue p7, OldSValue p8, OldSValue p9, OldSValue p10, params OldSValue[] ps);

    public delegate OldSValue Function_12_Params(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3, OldSValue p4, OldSValue p5,
        OldSValue p6, OldSValue p7, OldSValue p8, OldSValue p9, OldSValue p10, OldSValue p11, params OldSValue[] ps);

    public delegate OldSValue Function_13_Params(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3, OldSValue p4, OldSValue p5,
        OldSValue p6, OldSValue p7, OldSValue p8, OldSValue p9, OldSValue p10, OldSValue p11, OldSValue p12, params OldSValue[] ps);

    public delegate OldSValue Function_14_Params(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3, OldSValue p4, OldSValue p5,
        OldSValue p6, OldSValue p7, OldSValue p8, OldSValue p9, OldSValue p10, OldSValue p11, OldSValue p12, OldSValue p13, params OldSValue[] ps);

    public delegate OldSValue Function_15_Params(OldSValue p0, OldSValue p1, OldSValue p2, OldSValue p3, OldSValue p4, OldSValue p5,
        OldSValue p6, OldSValue p7, OldSValue p8, OldSValue p9, OldSValue p10, OldSValue p11, OldSValue p12, OldSValue p13, OldSValue p14,
        params OldSValue[] ps);

    #endregion

    public enum SValueType
    {
        /// <summary>
        /// 空类型
        /// </summary>
        Null,

        /// <summary>
        /// 数字类型
        /// </summary>
        Number,

        /// <summary>
        /// 值为 true
        /// </summary>
        BooleanTrue,

        /// <summary>
        /// 值为 false
        /// </summary>
        BooleanFalse,

        /// <summary>
        /// 字符串类型
        /// </summary>
        String,

        /// <summary>
        /// 函数类型
        /// </summary>
        Function,

        /// <summary>
        /// 对象类型
        /// </summary>
        Object,
    }

    private enum SObjectType
    {
        /// <summary>
        /// 类
        /// </summary>
        Class,

        /// <summary>
        /// 字典
        /// </summary>
        Map,

        /// <summary>
        /// 数组
        /// </summary>
        Array,

        /// <summary>
        /// 参数数量为0的函数
        /// </summary>
        Function_0,

        /// <summary>
        /// 参数数量为1的函数
        /// </summary>
        Function_1,

        /// <summary>
        /// 参数数量为2的函数
        /// </summary>
        Function_2,

        /// <summary>
        /// 参数数量为3的函数
        /// </summary>
        Function_3,

        /// <summary>
        /// 参数数量为4的函数
        /// </summary>
        Function_4,

        /// <summary>
        /// 参数数量为5的函数
        /// </summary>
        Function_5,

        /// <summary>
        /// 参数数量为6的函数
        /// </summary>
        Function_6,

        /// <summary>
        /// 参数数量为7的函数
        /// </summary>
        Function_7,

        /// <summary>
        /// 参数数量为8的函数
        /// </summary>
        Function_8,

        /// <summary>
        /// 参数数量为9的函数
        /// </summary>
        Function_9,

        /// <summary>
        /// 参数数量为10的函数
        /// </summary>
        Function_10,

        /// <summary>
        /// 参数数量为11的函数
        /// </summary>
        Function_11,

        /// <summary>
        /// 参数数量为12的函数
        /// </summary>
        Function_12,

        /// <summary>
        /// 参数数量为13的函数
        /// </summary>
        Function_13,

        /// <summary>
        /// 参数数量为14的函数
        /// </summary>
        Function_14,

        /// <summary>
        /// 参数数量为15的函数
        /// </summary>
        Function_15,

        /// <summary>
        /// 参数数量为16的函数
        /// </summary>
        Function_16,

        /// <summary>
        /// 参数数量为1的函数, 但最后一个参数为可变参数
        /// </summary>
        Function_0_Params,

        /// <summary>
        /// 参数数量为2的函数, 但最后一个参数为可变参数
        /// </summary>
        Function_1_Params,

        /// <summary>
        /// 参数数量为3的函数, 但最后一个参数为可变参数
        /// </summary>
        Function_2_Params,

        /// <summary>
        /// 参数数量为4的函数, 但最后一个参数为可变参数
        /// </summary>
        Function_3_Params,

        /// <summary>
        /// 参数数量为5的函数, 但最后一个参数为可变参数
        /// </summary>
        Function_4_Params,

        /// <summary>
        /// 参数数量为6的函数, 但最后一个参数为可变参数
        /// </summary>
        Function_5_Params,

        /// <summary>
        /// 参数数量为7的函数, 但最后一个参数为可变参数
        /// </summary>
        Function_6_Params,

        /// <summary>
        /// 参数数量为8的函数, 但最后一个参数为可变参数
        /// </summary>
        Function_7_Params,

        /// <summary>
        /// 参数数量为9的函数, 但最后一个参数为可变参数
        /// </summary>
        Function_8_Params,

        /// <summary>
        /// 参数数量为10的函数, 但最后一个参数为可变参数
        /// </summary>
        Function_9_Params,

        /// <summary>
        /// 参数数量为11的函数, 但最后一个参数为可变参数
        /// </summary>
        Function_10_Params,

        /// <summary>
        /// 参数数量为12的函数, 但最后一个参数为可变参数
        /// </summary>
        Function_11_Params,

        /// <summary>
        /// 参数数量为13的函数, 但最后一个参数为可变参数
        /// </summary>
        Function_12_Params,

        /// <summary>
        /// 参数数量为14的函数, 但最后一个参数为可变参数
        /// </summary>
        Function_13_Params,

        /// <summary>
        /// 参数数量为15的函数, 但最后一个参数为可变参数
        /// </summary>
        Function_14_Params,

        /// <summary>
        /// 参数数量为16的函数, 但最后一个参数为可变参数
        /// </summary>
        Function_15_Params,

        /// <summary>
        /// 其他类型
        /// </summary>
        Other
    }

    /// <summary>
    /// 当前值的类型
    /// </summary>
    public SValueType Type;

    /// <summary>
    /// 值数据
    /// </summary>
    public object Value;

    /// <summary>
    /// 被包裹实例类型
    /// </summary>
    private SObjectType objectType;

    #region 创建SValue

    public OldSValue(OldSValue v)
    {
        Type = v.Type;
        Value = v.Value;
        objectType = v.objectType;
    }

    public OldSValue(OldSObject v)
    {
        Type = SValueType.Object;
        Value = v;
        objectType = SObjectType.Class;
    }

    public OldSValue(OldSArray v)
    {
        Type = SValueType.Object;
        Value = v;
        objectType = SObjectType.Array;
    }

    public OldSValue(OldSMap v)
    {
        Type = SValueType.Object;
        Value = v;
        objectType = SObjectType.Map;
    }

    public OldSValue(double v)
    {
        Type = SValueType.Number;
        Value = v;
        objectType = SObjectType.Other;
    }

    public OldSValue(string v)
    {
        Type = SValueType.String;
        Value = v;
        objectType = SObjectType.Other;
    }

    public OldSValue(Function_0 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_0;
    }

    public OldSValue(Function_1 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_1;
    }

    public OldSValue(Function_2 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_2;
    }

    public OldSValue(Function_3 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_3;
    }

    public OldSValue(Function_4 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_4;
    }

    public OldSValue(Function_5 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_5;
    }

    public OldSValue(Function_6 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_6;
    }

    public OldSValue(Function_7 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_7;
    }

    public OldSValue(Function_8 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_8;
    }

    public OldSValue(Function_9 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_9;
    }

    public OldSValue(Function_10 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_10;
    }

    public OldSValue(Function_11 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_11;
    }

    public OldSValue(Function_12 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_12;
    }

    public OldSValue(Function_13 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_13;
    }

    public OldSValue(Function_14 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_14;
    }

    public OldSValue(Function_15 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_15;
    }

    public OldSValue(Function_16 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_16;
    }

    public OldSValue(Function_0_Params v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_0_Params;
    }

    public OldSValue(Function_1_Params v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_1_Params;
    }

    public OldSValue(Function_2_Params v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_2_Params;
    }

    public OldSValue(Function_3_Params v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_3_Params;
    }

    public OldSValue(Function_4_Params v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_4_Params;
    }

    public OldSValue(Function_5_Params v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_5_Params;
    }

    public OldSValue(Function_6_Params v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_6_Params;
    }

    public OldSValue(Function_7_Params v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_7_Params;
    }

    public OldSValue(Function_8_Params v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_8_Params;
    }

    public OldSValue(Function_9_Params v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_9_Params;
    }

    public OldSValue(Function_10_Params v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_10_Params;
    }

    public OldSValue(Function_11_Params v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_11_Params;
    }

    public OldSValue(Function_12_Params v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_12_Params;
    }

    public OldSValue(Function_13_Params v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_13_Params;
    }

    public OldSValue(Function_14_Params v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_14_Params;
    }

    public OldSValue(Function_15_Params v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_15_Params;
    }

    public static ref readonly OldSValue Null
    {
        get
        {
            if (!_initNullValue)
            {
                _initNullValue = true;
                _null.Type = SValueType.Null;
                _false.objectType = SObjectType.Other;
            }

            return ref _null;
        }
    }

    private static bool _initNullValue;
    private static OldSValue _null;

    public static ref readonly OldSValue True
    {
        get
        {
            if (!_initTrueValue)
            {
                _initTrueValue = true;
                _true.Type = SValueType.BooleanTrue;
                _false.objectType = SObjectType.Other;
                _true.Value = 1;
            }

            return ref _true;
        }
    }

    private static bool _initTrueValue;
    private static OldSValue _true;

    public static ref readonly OldSValue False
    {
        get
        {
            if (!_initFalseValue)
            {
                _initFalseValue = true;
                _false.Type = SValueType.BooleanFalse;
                _false.objectType = SObjectType.Other;
                _false.Value = 0;
            }

            return ref _false;
        }
    }

    private static bool _initFalseValue;
    private static OldSValue _false;

    #endregion

    #region 属性和方法的操作

    public OldSValue GetValue(string key)
    {
        switch (objectType)
        {
            case SObjectType.Class:
                return ((OldSObject)Value).__GetValue(key);
            case SObjectType.Array:
                return ((OldSArray)Value).__GetValue(key);
            case SObjectType.Map:
                return ((OldSMap)Value).__GetValue(key);
        }

        if (Type == SValueType.Null)
        {
            //空指针异常
        }

        return Null;
    }

    public OldSValue GetValue(double key)
    {
        if (objectType == SObjectType.Array)
        {
            return ((OldSArray)Value).__GetValue((int)key);
        }
        else
        {
            //只有array支持number类型索引取值
        }

        return Null;
    }

    public OldSValue GetValue(OldSValue key)
    {
        return Null;
    }

    public void SetValue()
    {

    }

    public OldSValue InvokeMethod(string key, params OldSValue[] ps)
    {
        switch (objectType)
        {
            case SObjectType.Class:
                return ((OldSObject)Value).__InvokeMethod(key, ps);
            case SObjectType.Array:
                return ((OldSArray)Value).__InvokeMethod(key, ps);
        }

        //不可执行函数, 报错
        return Null;
    }

    private static Func<object, OldSValue[], OldSValue>[] list;
    
    static OldSValue()
    {
        list = new Func<object, OldSValue[], OldSValue>[36];
        list[3] = (value, ps) =>
        {
            AssertUtils.AssertParamsLength(0, ps.Length);
            return ((Function_0)value)();
        };
    }
    
    public OldSValue Invoke(params OldSValue[] ps)
    {
        if (objectType == SObjectType.Function_1)
        {
            return ((Function_1)Value)(ps[0]);
        }

        switch (objectType)
        {
            case SObjectType.Function_0:
                AssertUtils.AssertParamsLength(0, ps.Length);
                return ((Function_0)Value)();
            case SObjectType.Function_1:
                AssertUtils.AssertParamsLength(1, ps.Length);
                return ((Function_1)Value)(ps[0]);
            case SObjectType.Function_2:
                AssertUtils.AssertParamsLength(2, ps.Length);
                return ((Function_2)Value)(ps[0], ps[1]);
            case SObjectType.Function_3:
                AssertUtils.AssertParamsLength(3, ps.Length);
                return ((Function_3)Value)(ps[0], ps[1], ps[2]);
            case SObjectType.Function_4:
                AssertUtils.AssertParamsLength(4, ps.Length);
                return ((Function_4)Value)(ps[0], ps[1], ps[2], ps[3]);
            case SObjectType.Function_5:
                AssertUtils.AssertParamsLength(5, ps.Length);
                return ((Function_5)Value)(ps[0], ps[1], ps[2], ps[3], ps[4]);
            case SObjectType.Function_6:
                AssertUtils.AssertParamsLength(6, ps.Length);
                return ((Function_6)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5]);
            case SObjectType.Function_7:
                AssertUtils.AssertParamsLength(7, ps.Length);
                return ((Function_7)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6]);
            case SObjectType.Function_8:
                AssertUtils.AssertParamsLength(8, ps.Length);
                return ((Function_8)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7]);
            case SObjectType.Function_9:
                AssertUtils.AssertParamsLength(9, ps.Length);
                return ((Function_9)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7], ps[8]);
            case SObjectType.Function_10:
                AssertUtils.AssertParamsLength(10, ps.Length);
                return ((Function_10)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7], ps[8], ps[9]);
            case SObjectType.Function_11:
                AssertUtils.AssertParamsLength(11, ps.Length);
                return ((Function_11)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7], ps[8], ps[9],
                    ps[10]);
            case SObjectType.Function_12:
                AssertUtils.AssertParamsLength(12, ps.Length);
                return ((Function_12)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7], ps[8], ps[9],
                    ps[10], ps[11]);
            case SObjectType.Function_13:
                AssertUtils.AssertParamsLength(13, ps.Length);
                return ((Function_13)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7], ps[8], ps[9],
                    ps[10], ps[11], ps[12]);
            case SObjectType.Function_14:
                AssertUtils.AssertParamsLength(14, ps.Length);
                return ((Function_14)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7], ps[8], ps[9],
                    ps[10], ps[11], ps[12], ps[13]);
            case SObjectType.Function_15:
                AssertUtils.AssertParamsLength(15, ps.Length);
                return ((Function_15)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7], ps[8], ps[9],
                    ps[10], ps[11], ps[12], ps[13], ps[14]);
            case SObjectType.Function_16:
                AssertUtils.AssertParamsLength(16, ps.Length);
                return ((Function_16)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7], ps[8], ps[9],
                    ps[10], ps[11], ps[12], ps[13], ps[14], ps[15]);
            case SObjectType.Function_0_Params:
                return ((Function_0_Params)Value)(ps);
            case SObjectType.Function_1_Params:
                AssertUtils.AssertParamsLength(0, ps.Length);
                return ((Function_1_Params)Value)(ps[0], CutParams(ps, 1));
            case SObjectType.Function_2_Params:
                AssertUtils.AssertParamsLength(1, ps.Length);
                return ((Function_2_Params)Value)(ps[0], ps[1], CutParams(ps, 2));
            case SObjectType.Function_3_Params:
                AssertUtils.AssertParamsLength(2, ps.Length);
                return ((Function_3_Params)Value)(ps[0], ps[1], ps[2], CutParams(ps, 3));
            case SObjectType.Function_4_Params:
                AssertUtils.AssertParamsLength(3, ps.Length);
                return ((Function_4_Params)Value)(ps[0], ps[1], ps[2], ps[3], CutParams(ps, 4));
            case SObjectType.Function_5_Params:
                AssertUtils.AssertParamsLength(4, ps.Length);
                return ((Function_5_Params)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], CutParams(ps, 5));
            case SObjectType.Function_6_Params:
                AssertUtils.AssertParamsLength(5, ps.Length);
                return ((Function_6_Params)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], CutParams(ps, 6));
            case SObjectType.Function_7_Params:
                AssertUtils.AssertParamsLength(6, ps.Length);
                return ((Function_7_Params)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], CutParams(ps, 7));
            case SObjectType.Function_8_Params:
                AssertUtils.AssertParamsLength(7, ps.Length);
                return ((Function_8_Params)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7],
                    CutParams(ps, 8));
            case SObjectType.Function_9_Params:
                AssertUtils.AssertParamsLength(8, ps.Length);
                return ((Function_9_Params)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7], ps[8],
                    CutParams(ps, 9));
            case SObjectType.Function_10_Params:
                AssertUtils.AssertParamsLength(9, ps.Length);
                return ((Function_10_Params)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7], ps[8], ps[9],
                    CutParams(ps, 10));
            case SObjectType.Function_11_Params:
                AssertUtils.AssertParamsLength(10, ps.Length);
                return ((Function_11_Params)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7], ps[8], ps[9],
                    ps[10], CutParams(ps, 11));
            case SObjectType.Function_12_Params:
                AssertUtils.AssertParamsLength(11, ps.Length);
                return ((Function_12_Params)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7], ps[8], ps[9],
                    ps[10], ps[11], CutParams(ps, 12));
            case SObjectType.Function_13_Params:
                AssertUtils.AssertParamsLength(12, ps.Length);
                return ((Function_13_Params)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7], ps[8], ps[9],
                    ps[10], ps[11], ps[12], CutParams(ps, 13));
            case SObjectType.Function_14_Params:
                AssertUtils.AssertParamsLength(13, ps.Length);
                return ((Function_14_Params)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7], ps[8], ps[9],
                    ps[10], ps[11], ps[12], ps[13], CutParams(ps, 14));
            case SObjectType.Function_15_Params:
                AssertUtils.AssertParamsLength(14, ps.Length);
                return ((Function_15_Params)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7], ps[8], ps[9],
                    ps[10], ps[11], ps[12], ps[13], ps[14], CutParams(ps, 15));
        }

        //不是function, 报错
        return Null;
    }

    #endregion

    #region 自动转型

    public static implicit operator OldSValue(double value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(bool value)
    {
        return value ? True : False;
    }

    public static implicit operator OldSValue(string value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(OldSObject value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(OldSArray value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(OldSMap value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_0 value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_1 value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_2 value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_3 value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_4 value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_5 value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_6 value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_7 value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_8 value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_9 value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_10 value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_11 value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_12 value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_13 value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_14 value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_15 value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_16 value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_0_Params value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_1_Params value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_2_Params value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_3_Params value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_4_Params value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_5_Params value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_6_Params value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_7_Params value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_8_Params value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_9_Params value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_10_Params value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_11_Params value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_12_Params value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_13_Params value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_14_Params value)
    {
        return new OldSValue(value);
    }

    public static implicit operator OldSValue(Function_15_Params value)
    {
        return new OldSValue(value);
    }

    #endregion

    #region 运算符重载

    public static OldSValue operator +(OldSValue v1, double num)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return new OldSValue((double)v1.Value + num);
            case SValueType.String:
                return new OldSValue(v1.Value + num.ToString());
            case SValueType.Object:
            case SValueType.Function:
            case SValueType.Null:
                return new OldSValue(double.NaN);
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

    public static OldSValue operator +(double num, OldSValue v2)
    {
        switch (v2.Type)
        {
            case SValueType.Number:
                return new OldSValue(num + (double)v2.Value);
            case SValueType.String:
                return new OldSValue(num.ToString() + v2.Value);
            case SValueType.Object:
            case SValueType.Function:
            case SValueType.Null:
                return new OldSValue(double.NaN);
            case SValueType.BooleanTrue:
                return new OldSValue(num + 1);
            case SValueType.BooleanFalse:
                return new OldSValue(num);
        }

        return Null;
    }

    public static OldSValue operator +(OldSValue v1, string v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
            case SValueType.Object:
            case SValueType.String:
                return new OldSValue(v1.Value + v2);
            case SValueType.Null:
                return new OldSValue("null" + v1.Value);
            case SValueType.Function:
                return new OldSValue("[function]" + v1.Value);
            case SValueType.BooleanTrue:
                return new OldSValue("true" + v1.Value);
            case SValueType.BooleanFalse:
                return new OldSValue("false" + v1.Value);
        }

        return Null;
    }

    public static OldSValue operator +(string v1, OldSValue v2)
    {
        switch (v2.Type)
        {
            case SValueType.Number:
            case SValueType.Object:
            case SValueType.String:
                return new OldSValue(v1 + v2.Value);
            case SValueType.Null:
                return new OldSValue(v1 + "null");
            case SValueType.Function:
                return new OldSValue(v1 + "[function]");
            case SValueType.BooleanTrue:
                return new OldSValue(v1 + "true");
            case SValueType.BooleanFalse:
                return new OldSValue(v1 + "false");
        }

        return Null;
    }

    public static OldSValue operator +(OldSValue v1, OldSValue v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new OldSValue((double)v1.Value + (double)v2.Value);
                    case SValueType.String:
                        return new OldSValue(v1.Value.ToString() + v2.Value);
                    case SValueType.Function:
                        return new OldSValue(v1.Value + "[function]");
                    case SValueType.Object:
                        return new OldSValue(v1.Value + "[object]");
                    case SValueType.Null:
                        return new OldSValue(double.NaN);
                    case SValueType.BooleanTrue:
                        return new OldSValue((double)v1.Value + 1);
                    case SValueType.BooleanFalse:
                        return new OldSValue(v1);
                }

                break;
            case SValueType.String:
                switch (v2.Type)
                {
                    case SValueType.Number:
                    case SValueType.Object:
                        return new OldSValue(v1.Value + v2.Value.ToString());
                    case SValueType.String:
                        return new OldSValue(v1.Value + (string)v2.Value);
                    case SValueType.Null:
                        return new OldSValue(v1.Value + "null");
                    case SValueType.Function:
                        return new OldSValue(v1.Value + "[function]");
                    case SValueType.BooleanTrue:
                        return new OldSValue(v1.Value + "true");
                    case SValueType.BooleanFalse:
                        return new OldSValue(v1.Value + "false");
                }

                break;
            case SValueType.Null:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new OldSValue(double.NaN);
                    case SValueType.Object:
                    case SValueType.String:
                        return new OldSValue("null" + v2.Value);
                    case SValueType.Null:
                        return Null;
                    case SValueType.BooleanTrue:
                        return new OldSValue("nulltrue");
                    case SValueType.BooleanFalse:
                        return new OldSValue("nullfalse");
                    case SValueType.Function:
                        return new OldSValue("null[function]");
                }

                break;
            case SValueType.Object:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new OldSValue(double.NaN);
                    case SValueType.Object:
                        return new OldSValue(v1.Value.ToString() + v2.Value);
                    case SValueType.String:
                        return new OldSValue(v1.Value + (string)v2.Value);
                    case SValueType.Null:
                        return new OldSValue("null" + v2.Value);
                    case SValueType.BooleanTrue:
                        return new OldSValue("true" + v2.Value);
                    case SValueType.BooleanFalse:
                        return new OldSValue("false" + v2.Value);
                    case SValueType.Function:
                        return new OldSValue("[function]" + v2.Value);
                }

                break;
            case SValueType.BooleanTrue:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new OldSValue((double)v1.Value + 1);
                    case SValueType.Object:
                    case SValueType.String:
                        return new OldSValue("true" + v2.Value);
                    case SValueType.Function:
                        return new OldSValue("true[function]");
                    case SValueType.Null:
                        return new OldSValue("truenull");
                    case SValueType.BooleanTrue:
                    case SValueType.BooleanFalse:
                        return True;
                }

                break;
            case SValueType.BooleanFalse:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new OldSValue(v1);
                    case SValueType.Object:
                    case SValueType.String:
                        return new OldSValue("false" + v2.Value);
                    case SValueType.Function:
                        return new OldSValue("false[function]");
                    case SValueType.Null:
                        return new OldSValue("falsenull");
                    case SValueType.BooleanTrue:
                        return True;
                    case SValueType.BooleanFalse:
                        return False;
                }

                break;
            case SValueType.Function:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new OldSValue(double.NaN);
                    case SValueType.Object:
                    case SValueType.String:
                        return new OldSValue("[function]" + v2.Value);
                    case SValueType.Function:
                        return new OldSValue("[function][function]");
                    case SValueType.Null:
                        return new OldSValue("[function]null");
                    case SValueType.BooleanTrue:
                        return new OldSValue("[function]true");
                    case SValueType.BooleanFalse:
                        return new OldSValue("[function]false");
                }

                break;
        }

        return Null;
    }

    public static OldSValue operator -(OldSValue v1, double num)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return new OldSValue((double)v1.Value - num);
            case SValueType.String:
            case SValueType.Object:
            case SValueType.Function:
            case SValueType.Null:
                return new OldSValue(double.NaN);
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

    public static OldSValue operator -(double num, OldSValue v2)
    {
        switch (v2.Type)
        {
            case SValueType.Number:
                return new OldSValue(num - (double)v2.Value);
            case SValueType.String:
            case SValueType.Object:
            case SValueType.Function:
            case SValueType.Null:
                return new OldSValue(double.NaN);
            case SValueType.BooleanTrue:
                return new OldSValue(num - 1);
            case SValueType.BooleanFalse:
                return new OldSValue(num);
        }

        return Null;
    }

    public static OldSValue operator -(OldSValue v1, OldSValue v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new OldSValue((double)v1.Value - (double)v2.Value);
                    case SValueType.String:
                    case SValueType.Object:
                    case SValueType.Function:
                    case SValueType.Null:
                        return new OldSValue(double.NaN);
                    case SValueType.BooleanTrue:
                        return new OldSValue((double)v1.Value - 1);
                    case SValueType.BooleanFalse:
                        return new OldSValue(v1);
                }

                break;
            case SValueType.String:
                return new OldSValue(double.NaN);
            case SValueType.Null:
                return new OldSValue(double.NaN);
            case SValueType.Object:
                return new OldSValue(double.NaN);
            case SValueType.Function:
                return new OldSValue(double.NaN);
            case SValueType.BooleanTrue:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        if (1 - (double)v2.Value > 0)
                            return True;
                        return False;
                    case SValueType.Object:
                    case SValueType.String:
                    case SValueType.Function:
                    case SValueType.BooleanTrue:
                        return False;
                    case SValueType.Null:
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
                    case SValueType.Function:
                    case SValueType.BooleanTrue:
                    case SValueType.BooleanFalse:
                        return False;
                }

                break;
        }

        return Null;
    }

    public static OldSValue operator *(OldSValue v1, double num)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return new OldSValue((double)v1.Value * num);
            case SValueType.String:
            case SValueType.Object:
            case SValueType.Function:
            case SValueType.Null:
                return new OldSValue(double.NaN);
            case SValueType.BooleanTrue:
                if (num > 0)
                    return True;
                return False;
            case SValueType.BooleanFalse:
                return False;
        }

        return Null;
    }

    public static OldSValue operator *(double num, OldSValue v2)
    {
        switch (v2.Type)
        {
            case SValueType.Number:
                return new OldSValue(num * (double)v2.Value);
            case SValueType.String:
            case SValueType.Object:
            case SValueType.Function:
            case SValueType.Null:
                return new OldSValue(double.NaN);
            case SValueType.BooleanTrue:
                return new OldSValue(num);
            case SValueType.BooleanFalse:
                return new OldSValue(0);
        }

        return Null;
    }

    public static OldSValue operator *(OldSValue v1, OldSValue v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new OldSValue((double)v1.Value * (double)v2.Value);
                    case SValueType.String:
                    case SValueType.Object:
                    case SValueType.Function:
                    case SValueType.Null:
                        return new OldSValue(double.NaN);
                    case SValueType.BooleanTrue:
                        return new OldSValue(v1);
                    case SValueType.BooleanFalse:
                        return new OldSValue(0);
                }

                break;
            case SValueType.String:
                return new OldSValue(double.NaN);
            case SValueType.Null:
                return new OldSValue(double.NaN);
            case SValueType.Object:
                return new OldSValue(double.NaN);
            case SValueType.Function:
                return new OldSValue(double.NaN);
            case SValueType.BooleanTrue:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        if ((double)v2.Value > 0)
                            return True;
                        return False;
                    case SValueType.Object:
                    case SValueType.String:
                    case SValueType.Function:
                    case SValueType.BooleanTrue:
                        return True;
                    case SValueType.Null:
                    case SValueType.BooleanFalse:
                        return False;
                }

                break;
            case SValueType.BooleanFalse:
                return False;
        }

        return Null;
    }

    public static OldSValue operator /(OldSValue v1, double num)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return new OldSValue((double)v1.Value / num);
            case SValueType.String:
            case SValueType.Object:
            case SValueType.Function:
            case SValueType.Null:
                return new OldSValue(double.NaN);
            case SValueType.BooleanTrue:
                if (num > 0)
                    return True;
                return False;
            case SValueType.BooleanFalse:
                return False;
        }

        return Null;
    }

    public static OldSValue operator /(double num, OldSValue v2)
    {
        switch (v2.Type)
        {
            case SValueType.Number:
                return new OldSValue(num / (double)v2.Value);
            case SValueType.String:
            case SValueType.Object:
            case SValueType.Function:
            case SValueType.Null:
                return new OldSValue(double.NaN);
            case SValueType.BooleanTrue:
                return new OldSValue(num);
            case SValueType.BooleanFalse:
                return new OldSValue(num / 0);
        }

        return Null;
    }

    public static OldSValue operator /(OldSValue v1, OldSValue v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new OldSValue((double)v1.Value / (double)v2.Value);
                    case SValueType.String:
                    case SValueType.Object:
                    case SValueType.Function:
                    case SValueType.Null:
                    case SValueType.BooleanFalse:
                        return new OldSValue(double.NaN);
                    case SValueType.BooleanTrue:
                        return new OldSValue(v1);
                }

                break;
            case SValueType.String:
                return new OldSValue(double.NaN);
            case SValueType.Null:
                return new OldSValue(double.NaN);
            case SValueType.Object:
                return new OldSValue(double.NaN);
            case SValueType.Function:
                return new OldSValue(double.NaN);
            case SValueType.BooleanTrue:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        if ((double)v2.Value > 0)
                            return True;
                        return False;
                    case SValueType.Object:
                    case SValueType.String:
                    case SValueType.Function:
                    case SValueType.BooleanTrue:
                        return True;
                    case SValueType.Null:
                    case SValueType.BooleanFalse:
                        return new OldSValue(double.NaN);
                }

                break;
            case SValueType.BooleanFalse:
                switch (v2.Type)
                {
                    case SValueType.Number:
                    case SValueType.Object:
                    case SValueType.String:
                    case SValueType.Function:
                    case SValueType.BooleanTrue:
                        return False;
                    case SValueType.Null:
                    case SValueType.BooleanFalse:
                        return new OldSValue(double.NaN);
                }

                break;
        }

        return Null;
    }

    public static bool operator ==(OldSValue v1, double v2)
    {
        if (v1.Type == SValueType.Number)
        {
            return (double)v1.Value == v2;
        }

        return false;
    }

    public static bool operator ==(double v1, OldSValue v2)
    {
        if (v2.Type == SValueType.Number)
        {
            return v1 == (double)v2.Value;
        }

        return false;
    }

    public static bool operator ==(OldSValue v1, string v2)
    {
        if (v1.Type == SValueType.String)
        {
            return (string)v1.Value == v2;
        }

        return false;
    }

    public static bool operator ==(string v1, OldSValue v2)
    {
        if (v2.Type == SValueType.String)
        {
            return v1 == (string)v2.Value;
        }

        return false;
    }

    public static bool operator ==(OldSValue v1, OldSValue v2)
    {
        if (v1.Type == SValueType.Number && v2.Type == SValueType.Number)
        {
            return (double)v1.Value == (double)v2.Value;
        }

        return v1.Value == v2.Value;
    }


    public static bool operator !=(OldSValue v1, double v2)
    {
        if (v1.Type == SValueType.Number)
        {
            return (double)v1.Value != v2;
        }

        return true;
    }

    public static bool operator !=(double v1, OldSValue v2)
    {
        if (v2.Type == SValueType.Number)
        {
            return v1 != (double)v2.Value;
        }

        return true;
    }

    public static bool operator !=(OldSValue v1, string v2)
    {
        if (v1.Type == SValueType.String)
        {
            return (string)v1.Value != v2;
        }

        return true;
    }

    public static bool operator !=(string v1, OldSValue v2)
    {
        if (v2.Type == SValueType.String)
        {
            return v1 != (string)v2.Value;
        }

        return true;
    }

    public static bool operator !=(OldSValue v1, OldSValue v2)
    {
        if (v1.Type == SValueType.Number && v2.Type == SValueType.Number)
        {
            return (double)v1.Value != (double)v2.Value;
        }

        return v1.Value != v2.Value;
    }


    public static bool operator <(OldSValue v1, double v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return (double)v1.Value < v2;
            case SValueType.BooleanTrue:
                return 1d < v2;
            case SValueType.BooleanFalse:
                return 0d < v2;
        }

        return false;
    }

    public static bool operator <(double v1, OldSValue v2)
    {
        switch (v2.Type)
        {
            case SValueType.Number:
                return v1 < (double)v2.Value;
            case SValueType.BooleanTrue:
                return v1 < 1d;
            case SValueType.BooleanFalse:
                return v1 < 0d;
        }

        return false;
    }

    public static bool operator <(OldSValue v1, OldSValue v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return (double)v1.Value < (double)v2.Value;
                    case SValueType.BooleanTrue:
                        return (double)v1.Value < 1d;
                    case SValueType.BooleanFalse:
                        return (double)v1.Value < 0d;
                }

                break;
            case SValueType.BooleanTrue:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return 1d < (double)v2.Value;
                    case SValueType.BooleanTrue:
                        return false;
                    case SValueType.BooleanFalse:
                        return false;
                }

                break;
            case SValueType.BooleanFalse:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return 0d < (double)v2.Value;
                    case SValueType.BooleanTrue:
                        return true;
                    case SValueType.BooleanFalse:
                        return false;
                }

                break;
        }

        return false;
    }

    public static bool operator >(OldSValue v1, double v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return (double)v1.Value > v2;
            case SValueType.BooleanTrue:
                return 1d > v2;
            case SValueType.BooleanFalse:
                return 0d > v2;
        }

        return false;
    }

    public static bool operator >(double v1, OldSValue v2)
    {
        switch (v2.Type)
        {
            case SValueType.Number:
                return v1 > (double)v2.Value;
            case SValueType.BooleanTrue:
                return v1 > 1d;
            case SValueType.BooleanFalse:
                return v1 > 0d;
        }

        return false;
    }

    public static bool operator >(OldSValue v1, OldSValue v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return (double)v1.Value > (double)v2.Value;
                    case SValueType.BooleanTrue:
                        return (double)v1.Value > 1d;
                    case SValueType.BooleanFalse:
                        return (double)v1.Value > 0d;
                }

                break;
            case SValueType.BooleanTrue:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return 1d > (double)v2.Value;
                    case SValueType.BooleanTrue:
                        return false;
                    case SValueType.BooleanFalse:
                        return true;
                }

                break;
            case SValueType.BooleanFalse:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return 0d > (double)v2.Value;
                    case SValueType.BooleanTrue:
                        return false;
                    case SValueType.BooleanFalse:
                        return false;
                }

                break;
        }

        return false;
    }

    public static bool operator <=(OldSValue v1, double v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return (double)v1.Value <= v2;
            case SValueType.BooleanTrue:
                return (double)v1.Value <= 1d;
            case SValueType.BooleanFalse:
                return (double)v1.Value <= 0d;
        }

        return false;
    }

    public static bool operator <=(double v1, OldSValue v2)
    {
        switch (v2.Type)
        {
            case SValueType.Number:
                return v1 <= (double)v2.Value;
            case SValueType.BooleanTrue:
                return v1 <= 1d;
            case SValueType.BooleanFalse:
                return v1 <= 0d;
        }

        return false;
    }

    public static bool operator <=(OldSValue v1, OldSValue v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return (double)v1.Value <= (double)v2.Value;
                    case SValueType.BooleanTrue:
                        return (double)v1.Value <= 1d;
                    case SValueType.BooleanFalse:
                        return (double)v1.Value <= 0d;
                }

                break;
            case SValueType.BooleanTrue:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return 1d <= (double)v2.Value;
                    case SValueType.BooleanTrue:
                        return true;
                    case SValueType.BooleanFalse:
                        return false;
                }

                break;
            case SValueType.BooleanFalse:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return 0d < (double)v2.Value;
                    case SValueType.BooleanTrue:
                        return true;
                    case SValueType.BooleanFalse:
                        return true;
                }

                break;
        }

        return false;
    }

    public static bool operator >=(OldSValue v1, double v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return (double)v1.Value >= v2;
            case SValueType.BooleanTrue:
                return (double)v1.Value >= 1d;
            case SValueType.BooleanFalse:
                return (double)v1.Value >= 0d;
        }

        return false;
    }

    public static bool operator >=(double v1, OldSValue v2)
    {
        switch (v2.Type)
        {
            case SValueType.Number:
                return v1 >= (double)v2.Value;
            case SValueType.BooleanTrue:
                return v1 >= 1d;
            case SValueType.BooleanFalse:
                return v1 >= 0d;
        }

        return false;
    }

    public static bool operator >=(OldSValue v1, OldSValue v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return (double)v1.Value >= (double)v2.Value;
                    case SValueType.BooleanTrue:
                        return (double)v1.Value >= 1d;
                    case SValueType.BooleanFalse:
                        return (double)v1.Value >= 0d;
                }

                break;
            case SValueType.BooleanTrue:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return 1d >= (double)v2.Value;
                    case SValueType.BooleanTrue:
                        return true;
                    case SValueType.BooleanFalse:
                        return true;
                }

                break;
            case SValueType.BooleanFalse:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return 0d >= (double)v2.Value;
                    case SValueType.BooleanTrue:
                        return false;
                    case SValueType.BooleanFalse:
                        return true;
                }

                break;
        }

        return false;
    }

    public static OldSValue operator ++(OldSValue v1)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                v1.Value = (double)v1.Value + 1d;
                return v1;
            case SValueType.BooleanTrue:
            case SValueType.BooleanFalse:
                return True;
        }

        return new OldSValue(double.NaN);
    }

    public static OldSValue operator --(OldSValue v1)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                v1.Value = (double)v1.Value - 1d;
                return v1;
            case SValueType.BooleanTrue:
            case SValueType.BooleanFalse:
                return False;
        }

        return new OldSValue(double.NaN);
    }

    public static OldSValue operator -(OldSValue v1)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return new OldSValue(-(double)v1.Value);
            case SValueType.BooleanTrue:
            case SValueType.BooleanFalse:
                return False;
        }

        return new OldSValue(double.NaN);
    }

    public static OldSValue operator +(OldSValue v1)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return new OldSValue(v1);
            case SValueType.BooleanTrue:
                return True;
            case SValueType.BooleanFalse:
                return False;
        }

        return new OldSValue(double.NaN);
    }

    public static OldSValue operator !(OldSValue v1)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return (double)v1.Value > 0 ? False : True;
            case SValueType.BooleanTrue:
                return False;
            case SValueType.BooleanFalse:
                return True;
        }

        return False;
    }

    public static bool operator true(OldSValue v1)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return (double)v1.Value > 0;
            case SValueType.BooleanTrue:
                return true;
            case SValueType.BooleanFalse:
            case SValueType.Null:
                return false;
            case SValueType.String:
                return ((string)v1.Value).Length != 0;
        }

        return true;
    }

    public static bool operator false(OldSValue v1)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return (double)v1.Value <= 0;
            case SValueType.BooleanTrue:
                return false;
            case SValueType.BooleanFalse:
            case SValueType.Null:
                return true;
            case SValueType.String:
                return ((string)v1.Value).Length == 0;
        }

        return false;
    }

    #endregion

    #region 公共函数

    public bool IsNumber()
    {
        return Type == SValueType.Number;
    }
    
    public bool IsString()
    {
        return Type == SValueType.String;
    }

    public bool IsObject()
    {
        return Type == SValueType.Object;
    }
    
    public bool IsBoolean()
    {
        return Type == SValueType.BooleanTrue || Type == SValueType.BooleanFalse;
    }
    
    public bool IsNull()
    {
        return Type == SValueType.Null;
    }
    
    public bool IsFunction()
    {
        return Type == SValueType.Function;
    }
    
    public bool IsArray()
    {
        return Value is OldSArray;
    }
    
    public bool IsMap()
    {
        return Value is OldSMap;
    }
    
    public int GetParamLength()
    {
        switch (objectType)
        {
            case SObjectType.Function_0:
                return 0;
            case SObjectType.Function_1:
                return 1;
            case SObjectType.Function_2:
                return 2;
            case SObjectType.Function_3:
                return 3;
            case SObjectType.Function_4:
                return 4;
            case SObjectType.Function_5:
                return 5;
            case SObjectType.Function_6:
                return 6;
            case SObjectType.Function_7:
                return 7;
            case SObjectType.Function_8:
                return 8;
            case SObjectType.Function_9:
                return 9;
            case SObjectType.Function_10:
                return 10;
            case SObjectType.Function_11:
                return 11;
            case SObjectType.Function_12:
                return 12;
            case SObjectType.Function_13:
                return 13;
            case SObjectType.Function_14:
                return 14;
            case SObjectType.Function_15:
                return 15;
            case SObjectType.Function_16:
                return 16;
            case SObjectType.Function_0_Params:
                return 1;
            case SObjectType.Function_1_Params:
                return 2;
            case SObjectType.Function_2_Params:
                return 3;
            case SObjectType.Function_3_Params:
                return 4;
            case SObjectType.Function_4_Params:
                return 5;
            case SObjectType.Function_5_Params:
                return 6;
            case SObjectType.Function_6_Params:
                return 7;
            case SObjectType.Function_7_Params:
                return 8;
            case SObjectType.Function_8_Params:
                return 9;
            case SObjectType.Function_9_Params:
                return 10;
            case SObjectType.Function_10_Params:
                return 11;
            case SObjectType.Function_11_Params:
                return 12;
            case SObjectType.Function_12_Params:
                return 13;
            case SObjectType.Function_13_Params:
                return 14;
            case SObjectType.Function_14_Params:
                return 15;
            case SObjectType.Function_15_Params:
                return 16;
        }
        return 0;
    }

    public bool HasDynamicParam()
    {
        switch (objectType)
        {
            case SObjectType.Function_0_Params:
            case SObjectType.Function_1_Params:
            case SObjectType.Function_2_Params:
            case SObjectType.Function_3_Params:
            case SObjectType.Function_4_Params:
            case SObjectType.Function_5_Params:
            case SObjectType.Function_6_Params:
            case SObjectType.Function_7_Params:
            case SObjectType.Function_8_Params:
            case SObjectType.Function_9_Params:
            case SObjectType.Function_10_Params:
            case SObjectType.Function_11_Params:
            case SObjectType.Function_12_Params:
            case SObjectType.Function_13_Params:
            case SObjectType.Function_14_Params:
            case SObjectType.Function_15_Params:
                return true;
        }

        return false;
    }
    
    public override string ToString()
    {
        switch (Type)
        {
            case SValueType.Function:
                return "function";
            case SValueType.Null:
                return "null";
            case SValueType.Number:
                return Value.ToString();
            case SValueType.Object:
                return Value.ToString();
            case SValueType.BooleanTrue:
                return "true";
            case SValueType.BooleanFalse:
                return "false";
            case SValueType.String:
                return (string)Value;
        }

        return Value.ToString();
    }
    
    #endregion

    /// <summary>
    /// 剪切参数
    /// </summary>
    internal static OldSValue[] CutParams(OldSValue[] ps, int index)
    {
        OldSValue[] arr = new OldSValue[ps.Length - index];
        for (int i = 0; index < ps.Length; index++, i++)
        {
            arr[i] = ps[index];
        }

        return arr;
    }
    
}
