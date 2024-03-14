#if TOOLS
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Godot;

namespace Generator;

/// <summary>
/// 生成 Buff 属性表
/// </summary>
public static class BuffGenerator
{
    private const string SavePath = "src/game/manager/BuffRegister.cs";
    
    public static bool Generate()
    {
        try
        {
            var buffInfos = new Dictionary<string, BuffInfo>();
            var types = typeof(BuffGenerator).Assembly.GetTypes();
            //包含[BuffAttribute]特性
            var enumerable = types.Where(type =>
                type.IsClass && !type.IsAbstract && type.IsAssignableTo(typeof(BuffFragment)));
            foreach (var type in enumerable)
            {
                var attribute = (BuffAttribute)type.GetCustomAttribute(typeof(BuffAttribute), false);
                if (attribute != null)
                {
                    if (buffInfos.ContainsKey(attribute.BuffName))
                    {
                        GD.PrintErr($"Buff '{attribute.BuffName}' 重名!");
                        return false;
                    }
                    var buffInfo = new BuffInfo(attribute.BuffName, attribute.Description, type);
                    buffInfos.Add(attribute.BuffName, buffInfo);
                    //判断重写参数情况
                    //1参数
                    var methodInfo1 = type.GetMethod(nameof(BuffFragment.InitParam),
                        BindingFlags.Instance | BindingFlags.Public, new Type[] { typeof(float) });
                    if (methodInfo1 != null &&
                        methodInfo1.GetBaseDefinition().DeclaringType != methodInfo1.DeclaringType)
                    {
                        buffInfo.Params.Add(1);
                    }

                    //2参数
                    var methodInfo2 = type.GetMethod(nameof(BuffFragment.InitParam),
                        BindingFlags.Instance | BindingFlags.Public, new Type[] { typeof(float), typeof(float) });
                    if (methodInfo2 != null &&
                        methodInfo2.GetBaseDefinition().DeclaringType != methodInfo2.DeclaringType)
                    {
                        buffInfo.Params.Add(2);
                    }

                    //3参数
                    var methodInfo3 = type.GetMethod(nameof(BuffFragment.InitParam),
                        BindingFlags.Instance | BindingFlags.Public,
                        new Type[] { typeof(float), typeof(float), typeof(float) });
                    if (methodInfo3 != null &&
                        methodInfo3.GetBaseDefinition().DeclaringType != methodInfo3.DeclaringType)
                    {
                        buffInfo.Params.Add(3);
                    }

                    //4参数
                    var methodInfo4 = type.GetMethod(nameof(BuffFragment.InitParam),
                        BindingFlags.Instance | BindingFlags.Public,
                        new Type[] { typeof(float), typeof(float), typeof(float), typeof(float) });
                    if (methodInfo4 != null &&
                        methodInfo4.GetBaseDefinition().DeclaringType != methodInfo4.DeclaringType)
                    {
                        buffInfo.Params.Add(4);
                    }
                }
            }
            
            GenerateCode(buffInfos);
        }
        catch (Exception e)
        {
            GD.PrintErr(e.Message + "\n" + e.StackTrace);
            return false;
        }

        return true;
    }
    
    private static void GenerateCode(Dictionary<string, BuffInfo> buffInfo)
    {
        var str = "";
        foreach (var kv in buffInfo)
        {
            var info = kv.Value;
            var s = "";
            for (var i = 0; i < info.Params.Count; i++)
            {
                if (i > 0) s += ", ";
                s += info.Params[i];
            }

            str += $"        BuffInfos.Add(\"{info.Name}\", new BuffInfo(\"{info.Name}\", null, new List<int>() {{ {s} }}, typeof({info.Type.FullName})));\n";
        }
        
        var code = $"using System.Collections.Generic;\n" +
                   $"/// <summary>\n" +
                   $"/// buff 注册类, 调用 Init() 函数初始化数据\n" +
                   $"/// 注意: 该类为 Tools 面板下自动生成的, 请不要手动编辑!\n" +
                   $"/// </summary>\n" +
                   $"public class BuffRegister\n" +
                   $"{{\n" +
                   $"    /// <summary>\n" +
                   $"    /// 所有 buff 信息\n" +
                   $"    /// </summary>\n" +
                   $"    public static Dictionary<string, BuffInfo> BuffInfos {{ get; private set; }}\n" +
                   $"    /// <summary>\n" +
                   $"    /// 初始化 buff\n" +
                   $"    /// </summary>\n" +
                   $"    public static void Init()\n" +
                   $"    {{\n" +
                   $"        BuffInfos = new Dictionary<string, BuffInfo>();\n" +
                   str +
                   $"    }}\n" +
                   $"}}";
        File.WriteAllText(SavePath, code);
    }
}
#endif
