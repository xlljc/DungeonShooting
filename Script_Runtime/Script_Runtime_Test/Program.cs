using System;
using System.Threading;

public class Program
{
    public static void Main(string[] args)
    {
        new Thread(() =>
        {
            Thread.Sleep(2000);
            Console.WriteLine("start");

            //Test3();
            Test2();
            //Test1();
        }).Start();

        Console.Read();
    }

    public static void Test4()
    {
        var time = DateTime.Now.Ticks;
        for (SValue i = 0; i < 999999; i++)
        {
            SValue v = new Vector2(1, 1);
            SValue v2 = new Vector2(3, 3);
            var b = $"1" + v + "11" + "222" + "333" + v2 + "444" + (v + v2);
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
            a.__Invoke__(1, 2);
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
        arr.__InvokeMethod__("add", 1);
        arr.__InvokeMethod__("add", 2);
        arr.__InvokeMethod__("add", 2);
        arr.__InvokeMethod__("add", 2);
        arr.__InvokeMethod__("add", 2);
        arr.__InvokeMethod__("add", 2);
        arr.__InvokeMethod__("add", 2);
        arr.__InvokeMethod__("add", 5);
        arr.__InvokeMethod__("add", 2);
        arr.__InvokeMethod__("add", 2);
        arr.__InvokeMethod__("add", 2);
        arr.__InvokeMethod__("add", 2);
        arr.__InvokeMethod__("add", 2);
        arr.__InvokeMethod__("add", 2);
        arr.__InvokeMethod__("add", "3");

        var time = DateTime.Now.Ticks;
        for (SValue i = 0; i < 999999; i++)
        {
            arr.__InvokeMethod__("indexOf", 2);
            arr.__InvokeMethod__("indexOf", "3");
            arr.__InvokeMethod__("indexOf", "4");
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
            //Console.WriteLine();
        }
        var time4 = DateTime.Now.Ticks;
        Console.WriteLine("原生C#运行耗时: " + (time4 - time3) / 10000f + "毫秒");

        var time = DateTime.Now.Ticks;
        for (SValue i = 0; i < 999999; i++)
        {
            SValue vect1 = new Vector2(1, 1);
            SValue vect2 = new Vector2(2, 3);
            SValue vect3 = vect1.__InvokeMethod__("add", vect2);
            var v = vect3.__InvokeMethod__("squareLengtn").Value;
        }
        var time2 = DateTime.Now.Ticks;
        Console.WriteLine("脚本运行耗时: " + (time2 - time) / 10000f + "毫秒");
    }

}