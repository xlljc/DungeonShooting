
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

    private SDataType test1_type = SDataType.Function_16;
    
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
            OldSValue a = 0;
            for (int i = 0; i < 999999; i++)
            {
                new Test2_Class(i);
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
            var a = SValue.Create(0);
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
            OldSValue func = new OldSValue(Test3_Func2);
            OldSValue v = "11";
            for (int i = 0; i < 999999; i++)
            {
                func.Invoke(v);
            }
        });
        
        ExecuteTime.Run("test3 新写法", () =>
        {
            var func = SValue.Create(Test3_Func3);
            var v = SValue.Create("11");
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
            for (SValue i = SValue.Create(0);i < 999999; i++)
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
                var item1 = SValue.Create("22");
                var item2 = SValue.Create(111);
            }
        });
    }
}