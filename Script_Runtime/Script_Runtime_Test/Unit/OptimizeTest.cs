
using System;
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
    //测试枚举是否影响switch性能
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
}