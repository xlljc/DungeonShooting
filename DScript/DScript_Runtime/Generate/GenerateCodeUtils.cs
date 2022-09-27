
public static class GenerateCodeUtils
{
    /// <summary>
    /// 获取值的函数名称
    /// </summary>
    public const string GetValueMethodName = "__GetValue";

    /// <summary>
    /// 设置值的函数名称
    /// </summary>
    public const string SetValueMethodName = "__SetValue";

    /// <summary>
    /// 执行方法的函数名称
    /// </summary>
    public const string InvokeMethodName = "__InvokeMethod";

    /// <summary>
    /// 生成获取值函数代码并返回
    /// </summary>
    /// <param name="classDescribe">类定义数据</param>
    /// <param name="indentation">代码缩进数量</param>
    public static string GenerateGetValueCode(ClassDescribe classDescribe, int indentation)
    {
        //缩进
        var indentationStr = GetIndentation(indentation);
        //函数名称
        string str;
        if (classDescribe.BaseClass == null)
        {
            str = indentationStr + "public virtual SValue " + GetValueMethodName + "(string key)\n";
        }
        else
        {
            str = indentationStr + "public override SValue " + GetValueMethodName + "(string key)\n";
        }

        str += indentationStr + "{\n";

        string caseStr = "";
        //拼接字段
        foreach (var item in classDescribe.Fields)
        {
            var temp = item.Value;
            if (temp.IsPublic)
            {
                caseStr += indentationStr + $"        case \"{temp.Name}\":\n";
                caseStr += indentationStr + $"            return {temp.Name};\n";
            }
        }

        //拼接get属性
        foreach (var item in classDescribe.GetProperties)
        {
            var temp = item.Value;
            if (temp.IsPublic)
            {
                caseStr += indentationStr + $"        case \"{temp.Name}\":\n";
                caseStr += indentationStr + $"            return {temp.Name};\n";
            }
        }

        if (!string.IsNullOrEmpty(caseStr))
        {
            str += indentationStr + "    switch (key)\n";
            str += indentationStr + "    {\n";
            str += caseStr;
            str += indentationStr + "    }\n";
        }

        if (classDescribe.BaseClass == null)
        {
            str += indentationStr + "    return SValue.Null;\n";
        }
        else
        {
            str += indentationStr + $"    return base.{GetValueMethodName}(key);\n";
        }

        str += indentationStr + "}\n";
        return str;
    }

    /// <summary>
    /// 生成设置值函数代码并返回
    /// </summary>
    /// <param name="classDescribe">类定义数据</param>
    /// <param name="indentation">代码缩进数量</param>
    public static string GenerateSetValueCode(ClassDescribe classDescribe, int indentation)
    {
        //缩进
        var indentationStr = GetIndentation(indentation);
        //函数名称
        string str;
        if (classDescribe.BaseClass == null)
        {
            str = indentationStr + "public virtual void " + SetValueMethodName + "(string key, SValue value)\n";
        }
        else
        {
            str = indentationStr + "public override void " + SetValueMethodName + "(string key, SValue value)\n";
        }

        str += indentationStr + "{\n";

        string caseStr = "";
        //拼接字段
        foreach (var item in classDescribe.Fields)
        {
            var temp = item.Value;
            if (temp.IsPublic && !temp.Name.EndsWith("k__BackingField"))
            {
                caseStr += indentationStr + $"        case \"{temp.Name}\":\n";
                caseStr += indentationStr + $"            {temp.Name} = value;\n";
                caseStr += indentationStr + $"            return;\n";
            }
        }

        //拼接set属性
        foreach (var item in classDescribe.SetProperties)
        {
            var temp = item.Value;
            if (temp.IsPublic)
            {
                caseStr += indentationStr + $"        case \"{temp.Name}\":\n";
                caseStr += indentationStr + $"            {temp.Name} = value;\n";
                caseStr += indentationStr + $"            return;\n";
            }
        }

        if (!string.IsNullOrEmpty(caseStr))
        {
            str += indentationStr + "    switch (key)\n";
            str += indentationStr + "    {\n";
            str += caseStr;
            str += indentationStr + "    }\n";
        }

        if (classDescribe.BaseClass != null)
        {
            str += indentationStr + $"    base.{SetValueMethodName}(key, value);\n";
        }

        str += indentationStr + "}\n";
        return str;
    }

    /// <summary>
    /// 生成调用方法函数代码并返回
    /// </summary>
    /// <param name="classDescribe">类定义数据</param>
    /// <param name="indentation">代码缩进数量</param>
    /// <returns></returns>
    public static string GenerateInvokeCode(ClassDescribe classDescribe, int indentation)
    {
        //缩进
        var indentationStr = GetIndentation(indentation);

        string str;
        if (classDescribe.BaseClass == null)
        {
            str = indentationStr + "public virtual SValue " + InvokeMethodName + "(string key, params SValue[] ps)\n";
        }
        else
        {
            str = indentationStr + "public override SValue " + InvokeMethodName + "(string key, params SValue[] ps)\n";
        }

        str += indentationStr + "{\n";

        string caseStr = "";

        //拼接属性
        foreach (var item in classDescribe.Fields)
        {
            var temp = item.Value;
            if (temp.IsPublic)
            {
                caseStr += indentationStr + $"        case \"{temp.Name}\":\n";
                caseStr += indentationStr + $"            return {temp.Name}.Invoke(ps);\n";
            }
        }

        //拼接get属性
        foreach (var item in classDescribe.GetProperties)
        {
            var temp = item.Value;
            if (temp.IsPublic)
            {
                caseStr += indentationStr + $"        case \"{temp.Name}\":\n";
                caseStr += indentationStr + $"            return {temp.Name}.Invoke(ps);\n";
            }
        }

        //拼接函数
        foreach (var item in classDescribe.Methods)
        {
            caseStr += indentationStr + $"        case \"{item.Key}\":\n";
            caseStr += indentationStr + $"            switch (ps.Length)\n";
            caseStr += indentationStr + "            {\n";
            foreach (var mi in item.Value)
            {
                caseStr += indentationStr + $"                case {item.Key}:\n";
                var paramsStr = "";
                for (int i = 0; i < mi.ParamLength; i++)
                {
                    if (i > 0)
                    {
                        paramsStr += ", ";
                    }

                    paramsStr += "ps[" + i + "]";
                }

                caseStr += indentationStr + $"                    return {item.Key}({paramsStr});\n";
            }

            caseStr += indentationStr + "            }\n";
            caseStr += indentationStr + "            break;\n";
        }

        if (!string.IsNullOrEmpty(caseStr))
        {
            str += indentationStr + "    switch (key)\n";
            str += indentationStr + "    {\n";
            str += caseStr;
            str += indentationStr + "    }\n";
        }

        if (classDescribe.BaseClass == null)
        {
            str += indentationStr + "    return SValue.Null;\n";
        }
        else
        {
            str += indentationStr + $"    return base.{InvokeMethodName}(key, ps);\n";
        }

        str += indentationStr + "}\n";
        return str;
    }

    /// <summary>
    /// 获取缩进字符串
    /// </summary>
    /// <param name="count">缩进数量</param>
    private static string GetIndentation(int count)
    {
        var str = "";
        for (int i = 0; i < count; i++)
        {
            str += ' ';
        }

        return str;
    }
}