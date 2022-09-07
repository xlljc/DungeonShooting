using System;
using System.Threading;

public interface ISvalue
{
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
    
    SValueType GetValueType();
    object GetValue();

    public static ISvalue From(double num)
    {
        return new Number_Value();
    }
}

public struct Func_0_Value : ISvalue
{
    private Func<SValue> _value;
    public ISvalue.SValueType GetValueType()
    {
        return ISvalue.SValueType.Function;
    }

    public object GetValue()
    {
        return _value;
    }
}

public struct Number_Value : ISvalue
{
    private double _value;
    
    public Number_Value(double value)
    {
        _value = value;
    }
    
    public ISvalue.SValueType GetValueType()
    {
        return ISvalue.SValueType.Number;
    }

    public object GetValue()
    {
        return _value;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        new Thread(() =>
        {
            Thread.Sleep(2000);
            Console.WriteLine("start");

            //Test3();
            //Test2();
            //Test1();
            //Test4();
            Test5();

        }).Start();

        Console.Read();
    }

    public static void Test5()
    {
        var time5 = DateTime.Now.Ticks;
        for (int i = 0; i < 999999; i++)
        {
            Test();
        }

        var time6 = DateTime.Now.Ticks;
        Console.WriteLine("原生C#运行耗时(原生): " + (time6 - time5) / 10000f + "毫秒");
        
        var time3 = DateTime.Now.Ticks;
        object fun = new Func<SValue>(Test);
        for (int i = 0; i < 999999; i++)
        {
            ((Func<SValue>)fun)();
        }

        var time4 = DateTime.Now.Ticks;
        Console.WriteLine("原生C#运行耗时(转型): " + (time4 - time3) / 10000f + "毫秒");

        var time = DateTime.Now.Ticks;
        var test = new SValue(Test);
        for (int i = 0; i < 999999; i++)
        {
            test.Invoke();
        }
        var time2 = DateTime.Now.Ticks;
        Console.WriteLine("脚本运行耗时: " + (time2 - time) / 10000f + "毫秒");
    }
    
    public static void Test4()
    {
        var time3 = DateTime.Now.Ticks;
        for (int i = 0; i < 999999; i++)
        {
            Vector2Cs v = new Vector2Cs(1, 1);
            Vector2Cs v2 = new Vector2Cs(2, 3);
            var b = "1" + v + "11" + "222" + "333" + v2 + "444" + (v.ToString() + v2);
            //Console.WriteLine();
        }
        var time4 = DateTime.Now.Ticks;
        Console.WriteLine("原生C#运行耗时: " + (time4 - time3) / 10000f + "毫秒");
        
        var time = DateTime.Now.Ticks;
        for (SValue i = 0; i < 999999; i++)
        {
            SValue v = new Vector2(1, 1);
            SValue v2 = new Vector2(3, 3);
            var b = "1" + v + "11" + "222" + "333" + v2 + "444" + (v + v2);
            //var b = "1" + v;
        }
        var time2 = DateTime.Now.Ticks;
        Console.WriteLine("脚本运行耗时: " + (time2 - time) / 10000f + "毫秒");
    }

    /// <summary>
    /// 测试函数调用
    /// </summary>
    public static void Test3()
    {
        var time = DateTime.Now.Ticks;
        for (SValue i = 0; i < 999999; i++)
        {
            SValue a = new SValue.Function_2((b, c) =>
            {
                SValue d = b + 1 + c;
                return SValue.Null;
            });
            a.Invoke(1, 2);
        }
        var time2 = DateTime.Now.Ticks;
        Console.WriteLine("脚本运行耗时: " + (time2 - time) / 10000f + "毫秒");
    }

    /// <summary>
    /// 测试array基础api
    /// </summary>
    public static void Test2()
    {
        var arr = new SArray();
        arr.__InvokeMethod("add", 1);
        arr.__InvokeMethod("add", 2);
        arr.__InvokeMethod("add", 2);
        arr.__InvokeMethod("add", 2);
        arr.__InvokeMethod("add", 2);
        arr.__InvokeMethod("add", 2);
        arr.__InvokeMethod("add", 2);
        arr.__InvokeMethod("add", 5);
        arr.__InvokeMethod("add", 2);
        arr.__InvokeMethod("add", 2);
        arr.__InvokeMethod("add", 2);
        arr.__InvokeMethod("add", 2);
        arr.__InvokeMethod("add", 2);
        arr.__InvokeMethod("add", 2);
        arr.__InvokeMethod("add", "3");

        var time = DateTime.Now.Ticks;
        for (SValue i = 0; i < 999999; i++)
        {
            arr.__InvokeMethod("indexOf", 2);
            arr.__InvokeMethod("indexOf", "3");
            arr.__InvokeMethod("indexOf", "4");
        }
        var time2 = DateTime.Now.Ticks;
        Console.WriteLine("脚本运行耗时: " + (time2 - time) / 10000f + "毫秒");
    }

    /// <summary>
    /// 对比包裹代码与原生C#代码运行效率对比
    /// </summary>
    public static void Test1()
    {
        var time3 = DateTime.Now.Ticks;
        for (int i = 0; i < 999999; i++)
        {
            Vector2Cs vect1 = new Vector2Cs(1, 1);
            Vector2Cs vect2 = new Vector2Cs(2, 3);
            Vector2Cs vect3 = vect1.add(vect2);
            var v = vect3.squareLengtn();
        }
        var time4 = DateTime.Now.Ticks;
        Console.WriteLine("原生C#运行耗时: " + (time4 - time3) / 10000f + "毫秒");

        var time = DateTime.Now.Ticks;
        for (SValue i = 0; i < 999999; i++)
        {
            SValue vect1 = new Vector2(1, 1);
            SValue vect2 = new Vector2(2, 3);
            SValue vect3 = vect1.InvokeMethod("add", vect2);
            var v = vect3.InvokeMethod("squareLengtn").Value;
        }
        var time2 = DateTime.Now.Ticks;
        Console.WriteLine("脚本运行耗时: " + (time2 - time) / 10000f + "毫秒");
    }

    public static SValue Test()
    {
        return SValue.Null;
    }

}