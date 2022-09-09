
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

    private SDataType test1_type = SDataType.Function_15_Params;
    
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
                    case SDataType.Function_0:
                        break;
                    case SDataType.Function_1:
                        break;
                    case SDataType.Function_2:
                        break;
                    case SDataType.Function_3:
                        break;
                    case SDataType.Function_4:
                        break;
                    case SDataType.Function_5:
                        break;
                    case SDataType.Function_6:
                        break;
                    case SDataType.Function_7:
                        break;
                    case SDataType.Function_8:
                        break;
                    case SDataType.Function_9:
                        break;
                    case SDataType.Function_10:
                        break;
                    case SDataType.Function_11:
                        break;
                    case SDataType.Function_12:
                        break;
                    case SDataType.Function_13:
                        break;
                    case SDataType.Function_14:
                        break;
                    case SDataType.Function_15:
                        break;
                    case SDataType.Function_16:
                        break;
                    case SDataType.Function_0_Params:
                        break;
                    case SDataType.Function_1_Params:
                        break;
                    case SDataType.Function_2_Params:
                        break;
                    case SDataType.Function_3_Params:
                        break;
                    case SDataType.Function_4_Params:
                        break;
                    case SDataType.Function_5_Params:
                        break;
                    case SDataType.Function_6_Params:
                        break;
                    case SDataType.Function_7_Params:
                        break;
                    case SDataType.Function_8_Params:
                        break;
                    case SDataType.Function_9_Params:
                        break;
                    case SDataType.Function_10_Params:
                        break;
                    case SDataType.Function_11_Params:
                        break;
                    case SDataType.Function_12_Params:
                        break;
                    case SDataType.Function_13_Params:
                        break;
                    case SDataType.Function_14_Params:
                        break;
                    case SDataType.Function_15_Params:
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
        
        ExecuteTime.Run("test3 创建对象", () =>
        {
            SValue a = 0;
            for (int i = 0; i < 999999; i++)
            {
                new Test2_Class(i);
            }
        });
        
        ExecuteTime.Run("test3 老写法", () =>
        {
            SValue a = 0;
            for (int i = 0; i < 999999; i++)
            {
                a++;
            }
        });
        
        ExecuteTime.Run("test3 新写法", () =>
        {
            var a = ISValue.Create(0);
            for (int i = 0; i < 999999; i++)
            {
                a++;
            }
        });
    }

    private struct Test2_Class
    {
        private double i;

        public Test2_Class(double i)
        {
            this.i = i;
        }
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
                method.Invoke(this, new object[]{ "11" });
            }
        });

        ExecuteTime.Run("test3 老写法", () =>
        {
            SValue func = new SValue(Test3_Func2);
            SValue v = "11";
            for (int i = 0; i < 999999; i++)
            {
                func.Invoke(v);
            }
        });
        
        ExecuteTime.Run("test3 新写法", () =>
        {
            var func = ISValue.Create(Test3_Func3);
            var v = ISValue.Create("11");
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
    
    private SValue Test3_Func2(SValue str)
    {
        return SValue.Null;
    }
    
    private ISValue Test3_Func3(ISValue str)
    {
        return ISValue.Null;
    }
}