
using System;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

/// <summary>
/// 优化runtim方案测试
/// </summary>
public class OptimizeTest : UnitTest
{
    public OptimizeTest(ITestOutputHelper helper) : base(helper)
    {
    }

    private DataType test1_type = DataType.Function_16;

    //测试枚举是否影响switch性能, 结果: 并不影响
    [Fact]
    public void Test1()
    {
        ExecuteTime.Run("测试循环", () =>
        {
            for (int i = 0; i < 999999; i++)
            {
                switch (test1_type)
                {
                    case DataType.Function_0:
                        break;
                    case DataType.Function_1:
                        break;
                    case DataType.Function_2:
                        break;
                    case DataType.Function_3:
                        break;
                    case DataType.Function_4:
                        break;
                    case DataType.Function_5:
                        break;
                    case DataType.Function_6:
                        break;
                    case DataType.Function_7:
                        break;
                    case DataType.Function_8:
                        break;
                    case DataType.Function_9:
                        break;
                    case DataType.Function_10:
                        break;
                    case DataType.Function_11:
                        break;
                    case DataType.Function_12:
                        break;
                    case DataType.Function_13:
                        break;
                    case DataType.Function_14:
                        break;
                    case DataType.Function_15:
                        break;
                    case DataType.Function_16:
                        break;
                }
            }
        });
    }

    [Fact(DisplayName = "测试自加自减性能")]
    public void Test2()
    {
        ExecuteTime.Run("test3 原生C#", () =>
        {
            var a = 0;
            for (int i = 0; i < 999999; i++)
            {
                a++;
            }
        });

        ExecuteTime.Run("test3 老写法", () =>
        {
            OldSValue a = 0;
            for (int i = 0; i < 999999; i++)
            {
                a++;
            }
        });

        ExecuteTime.Run("test3 新写法", () =>
        {
            SValue a = 0;
            for (int i = 0; i < 999999; i++)
            {
                a++;
            }
        });
    }

    [Fact(DisplayName = "测试函数执行性能")]
    public void Test3()
    {
        ExecuteTime.Run("test3 原生C#", () =>
        {
            for (int i = 0; i < 999999; i++)
            {
                Test3_Func1("11");
            }
        });

        ExecuteTime.Run("test3 反射C#", () =>
        {
            var method = GetType().GetMethod("Test3_Func1", BindingFlags.Instance | BindingFlags.NonPublic);
            for (int i = 0; i < 999999; i++)
            {
                method.Invoke(this, new object[] { "11" });
            }
        });

        ExecuteTime.Run("test3 老写法", () =>
        {
            OldSValue func = new OldSValue(Test3_Func2);
            OldSValue v = "11";
            for (int i = 0; i < 999999; i++)
            {
                func.Invoke(v);
            }
        });

        ExecuteTime.Run("test3 新写法", () =>
        {
            SValue func = new Function_1_SValue(Test3_Func3);
            SValue v = "11";
            for (int i = 0; i < 999999; i++)
            {
                func.Invoke(v);
            }
        });
    }


    private object Test3_Func1(string str)
    {
        return null;
    }

    private OldSValue Test3_Func2(OldSValue str)
    {
        return OldSValue.Null;
    }

    private SValue Test3_Func3(SValue str)
    {
        return SValue.Null;
    }

    [Fact(DisplayName = "测试循环性能")]
    public void Test4()
    {
        ExecuteTime.Run("test4 原生C#", () =>
        {
            for (int i = 0; i < 999999; i++)
            {

            }
        });

        ExecuteTime.Run("test4 老写法", () =>
        {
            for (OldSValue i = 0; i < 999999; i++)
            {

            }
        });

        ExecuteTime.Run("test4 新写法", () =>
        {
            for (SValue i = 0; i < 999999; i++)
            {

            }
        });
    }

    [Fact(DisplayName = "测试创建SValue对象性能")]
    public void Test5()
    {
        ExecuteTime.Run("test5", () =>
        {
            //23 - 25
            for (int i = 0; i < 999999; i++)
            {
                SValue item1 = "22";
                SValue item2 = 111;
            }
        });
    }

    [Fact(DisplayName = "测试string性能")]
    public void Test6()
    {
        ExecuteTime.Run("test6 原生C#", () =>
        {
            string a = "";
            for (int i = 0; i < 999999; i++)
            {
                string b = "1";
                string c = "2";
                a = i + b + c;
            }
        });

        ExecuteTime.Run("test6 老写法", () =>
        {
            OldSValue a = "";
            for (int i = 0; i < 999999; i++)
            {
                OldSValue b = "1";
                OldSValue c = "2";
                a = i + b + c;
            }
        });

        ExecuteTime.Run("test6 新写法", () =>
        {
            SValue a = "";
            for (int i = 0; i < 999999; i++)
            {
                SValue b = "1";
                SValue c = "2";
                a = i + b + c;
            }
        });
    }
}