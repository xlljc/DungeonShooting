
using System;
using System.Collections.Generic;
using System.Threading;

public class Program
{
    public static string GetKey(int i)
    {
        string[] keys = { "111", "999" };
        return keys[i];
    }
    
    public static void Test6()
    {
        string key = GetKey(1);
        Dictionary<string, Func<object, ISValue>> obj = new Dictionary<string, Func<object, ISValue>>();
        obj.Add("111", o => ISValue.Null);
        obj.Add("222", o => ISValue.Null);
        obj.Add("333", o => ISValue.Null);
        obj.Add("444", o => ISValue.Null);
        obj.Add("555", o => ISValue.Null);
        obj.Add("666", o => ISValue.Null);
        obj.Add("777", o => ISValue.Null);
        obj.Add("888", o => ISValue.Null);
        obj.Add("999", o => ISValue.Null);

        var time5 = DateTime.Now.Ticks;
        for (int i = 0; i < 999999; i++)
        {
            var v = new Func<ISValue>(() =>
            {
                if (key == "111")
                {
                    return ISValue.Null;
                }
                else if (key == "222")
                {
                    return ISValue.Null;
                }
                else if (key == "333")
                {
                    return ISValue.Null;
                }
                else if (key == "444")
                {
                    return ISValue.Null;
                }
                else if (key == "555")
                {
                    return ISValue.Null;
                }
                else if (key == "666")
                {
                    return ISValue.Null;
                }
                else if (key == "777")
                {
                    return ISValue.Null;
                }
                else if (key == "888")
                {
                    return ISValue.Null;
                }
                else if (key == "999")
                {
                    return ISValue.Null;
                }

                return ISValue.Null;
            })();

        }

        var time6 = DateTime.Now.Ticks;
        Console.WriteLine("if 速度: " + (time6 - time5) / 10000f + "毫秒");
        
        var time3 = DateTime.Now.Ticks;
        for (int i = 0; i < 999999; i++)
        {
            var v = new Func<ISValue>(() =>
            {
                switch (key)
                {
                    case "111": return ISValue.Null;
                    case "222": return ISValue.Null;
                    case "333": return ISValue.Null;
                    case "444": return ISValue.Null;
                    case "555": return ISValue.Null;
                    case "666": return ISValue.Null;
                    case "777": return ISValue.Null;
                    case "888": return ISValue.Null;
                    case "999": return ISValue.Null;
                }
                return ISValue.Null;
            })();

        }

        var time4 = DateTime.Now.Ticks;
        Console.WriteLine("switch 速度: " + (time4 - time3) / 10000f + "毫秒");

        var time = DateTime.Now.Ticks;
        for (int i = 0; i < 999999; i++)
        {
            var v = new Func<ISValue>(() =>
            {
                return obj[key](1);
            })();
        }
        var time2 = DateTime.Now.Ticks;
        Console.WriteLine("字典速度: " + (time2 - time) / 10000f + "毫秒");
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

        var time = DateTime.Now.Ticks;
        var test = ISValue.Create(Test);
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

    public static ISValue Test()
    {
        return ISValue.Null;
    }

}