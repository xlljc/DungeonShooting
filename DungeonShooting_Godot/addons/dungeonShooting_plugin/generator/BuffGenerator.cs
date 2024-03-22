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
    public static bool Generate()
    {
        PropFragmentRegister.Init();
        var outStr = "# 道具逻辑属性表\n\n";
        
        outStr += GetSplit("Buff 属性片段");
        outStr += GetTableTitle();
        foreach (var fragment in PropFragmentRegister.BuffFragmentInfos)
        {
            outStr += GetTableLine(fragment.Value);
        }
        outStr += "\n\n";
        
        outStr += GetSplit("主动道具使用条件片段");
        outStr += GetTableTitle();
        foreach (var fragment in PropFragmentRegister.ConditionFragmentInfos)
        {
            outStr += GetTableLine(fragment.Value);
        }
        outStr += "\n\n";
        
        outStr += GetSplit("主动道具使用效果片段");
        outStr += GetTableTitle();
        foreach (var fragment in PropFragmentRegister.EffectFragmentInfos)
        {
            outStr += GetTableLine(fragment.Value);
        }
        outStr += "\n\n";
        
        outStr += GetSplit("主动道具充能条件片段");
        outStr += GetTableTitle();
        foreach (var fragment in PropFragmentRegister.ChargeFragmentInfos)
        {
            outStr += GetTableLine(fragment.Value);
        }
        outStr += "\n\n";

        if (!Directory.Exists("buffTable"))
        {
            Directory.CreateDirectory("buffTable");
        }
        File.WriteAllText("buffTable/BuffTable.md", outStr);
        return true;
    }

    private static string GetSplit(string title)
    {
        return $"---\n### {title}\n";
    }

    private static string GetTableTitle()
    {
        return $"| 属性名称 | 描述 | 参数 |\n" +
               $"|-|-|-|\n";
    }

    private static string GetTableLine(PropFragmentInfo fragmentInfo)
    {
        var arg = "";
        for (var i = 0; i < fragmentInfo.ArgInfos.Count; i++)
        {
            var argInfo = fragmentInfo.ArgInfos[i];
            if (i > 0)
            {
                arg += "<br/>";
            }
            arg += $"参数{argInfo.ArgIndex}: {argInfo.Description}";
        }

        return  $"| {fragmentInfo.Name} | {fragmentInfo.Description} | {arg} |\n";
    }
}
#endif
