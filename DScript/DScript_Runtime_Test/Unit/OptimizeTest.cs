
using System;
using System.Reflection;
using System.Threading;
using DScript.Runtime;
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
            SValue a = new Number_SValue(0);
            for (int i = 0; i < 999999; i++)
            {
                a = a.Operator_SinceAdd();
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
            SValue v = new String_SValue("11");
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
            for (SValue i = SValue.One; i.Operator_Less_Double(999999); i = i.Operator_SinceAdd())
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
                SValue item1 = new String_SValue("22");
                SValue item2 = new Number_SValue(111);
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
            SValue a = SValue.EmptyString;
            for (int i = 0; i < 999999; i++)
            {
                SValue b = new String_SValue("1");
                SValue c = new String_SValue("2");
                a = b.Operator_Append_Add_Double(i).Operator_Add_SValue(c);
            }
        });
    }

    [Fact(DisplayName = "测试转型和调用函数性能")]
    public void Test7()
    {
        ExecuteTime.Run("test7 转型", () =>
        {
            object obj = new Test7_Cls();
            for (int i = 0; i < 999999; i++)
            {
                string temp = ((Test7_Cls)obj).obj;
            }
        });

        ExecuteTime.Run("test7 调用函数", () =>
        {
            Test7_Cls obj = new Test7_Cls();
            for (int i = 0; i < 999999; i++)
            {
                string temp = obj.GetValue() as string;
            }
        });
    }

    public class Test7_Cls
    {
        public string obj = "abc";

        public object GetValue()
        {
            return obj;
        }
    }

    [Fact(DisplayName = "测试判断性能")]
    public void Test8()
    {
        OldSValue oldobj1 = 123;
        OldSValue oldobj2 = new OldSArray();
        OldSValue oldobj3 = "abb";
        OldSValue oldobj4 = 456;
        ExecuteTime.Run("test8 老写法", () =>
        {
            for (int i = 0; i < 999999; i++)
            {
                var v1 = oldobj1 == oldobj2;
                var v2 = oldobj2 == oldobj3;
                var v3 = oldobj1 == oldobj3;
                var v4 = oldobj2 == oldobj3;
                var v21 = oldobj1 > oldobj2;
                var v22 = oldobj2 <= oldobj3;
                var v23 = oldobj1 <= oldobj3;
                var v24 = oldobj1 <= oldobj4;
            }
        });

        SValue obj1 = new Number_SValue(123);
        SValue obj2 = new Object_SValue(new SObject());
        SValue obj3 = new String_SValue("abb");
        SValue obj4 = new Number_SValue(456);
        ExecuteTime.Run("test8 新写法", () =>
        {
            for (int i = 0; i < 999999; i++)
            {
                var v1 = obj1.Operator_Equal_SValue(obj2);
                var v2 = obj2.Operator_Equal_SValue(obj3);
                var v3 = obj1.Operator_Equal_SValue(obj3);
                var v4 = obj2.Operator_Equal_SValue(obj3);
                var v21 = obj1.Operator_Greater_SValue(obj2);
                var v22 = obj2.Operator_Less_Equal_SValue(obj3);
                var v23 = obj1.Operator_Less_Equal_SValue(obj3);
                var v24 = obj1.Operator_Less_Equal_SValue(obj4);
            }
        });
    }

    [Fact(DisplayName = "测试string拼接性能")]
    public void Test9()
    {
        ExecuteTime.Run("test9 string+string", () =>
        {
            string a = "";
            for (int i = 0; i < 999999; i++)
            {
                a = "abc" + i;
            }
        });

        ExecuteTime.Run("test9 string.Concat", () =>
        {
            string a = "";
            for (int i = 0; i < 999999; i++)
            {
                a = string.Concat("abc" + i);
            }
        });

        ExecuteTime.Run("test9 string.Format", () =>
        {
            string a = "";
            for (int i = 0; i < 999999; i++)
            {
                a = string.Format("abc{0}", i);
            }
        });

        ExecuteTime.Run("test9 $string", () =>
        {
            string a = "";
            for (int i = 0; i < 999999; i++)
            {
                a = $"abc{i}";
            }
        });
    }

    [Fact(DisplayName = "number和其他类型相加性能")]
    public void Test11()
    {
        ExecuteTime.Run("test11 原生C#", () =>
        {
            object obj = new object();
            var func = new OldSValue.Function_3((a, b, c) => { return OldSValue.Null; });
            for (int i = 0; i < 999999; i++)
            {
                int num = 1;
                var a = num + "111";
                var b = num.ToString() + obj;
                var c = num.ToString() + func;
                var d = num.ToString() + true;
            }
        });

        ExecuteTime.Run("test11 老写法", () =>
        {
            OldSValue obj = new OldSArray();
            var func = new OldSValue(new OldSValue.Function_3((a, b, c) => { return OldSValue.Null; }));
            for (int i = 0; i < 999999; i++)
            {
                OldSValue num = 1;
                var a = num + new OldSValue("111");
                var b = num + obj;
                var c = num + func;
                var d = num + true;
            }
        });
        
        ExecuteTime.Run("test11 新写法", () =>
        {
            var obj = new Object_SValue(new SObject());
            var func = new Function_1_SValue(Test3_Func3);
            Number_SValue num = new Number_SValue(1);
            for (int i = 0; i < 999999; i++)
            {
                var a = num.Operator_Add_SValue(new String_SValue("111"));
                var b = num.Operator_Add_SValue(obj);
                var c = num.Operator_Add_SValue(func);
                var d = num.Operator_Add_SValue(SValue.True);
            }
        });
    }

}