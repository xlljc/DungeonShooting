
using System.Reflection;
using System.Text.RegularExpressions;

public static class GenerateCode
{
    //IObject口下所有函数名称, 生成代码时会忽略这些函数
    private static readonly string[] Methods = { "__GetValue", "__SetValue", "__InvokeMethod", "ToString", "GetType", "Equals", "GetHashCode" };
    
    
    private const BindingFlags InstanceBindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly;
    
    //开始执行生成代码操作
    public static void StartGenerate(Uri projectPath)
    {
        //扫描程序集
        var assembly = typeof(IObject).Assembly;
        var types = assembly.GetTypes();
        for (int i = 0; i < types.Length; i++)
        {
            var type = types[i];
            if (type != typeof(IObject) && typeof(IObject).IsAssignableFrom(type))
            {
                //Console.WriteLine(type.FullName + ", " + (type.BaseType == typeof(object)));
                string clsPath = FindFileByClsName(new DirectoryInfo(projectPath.AbsolutePath + "/Environment/"), type.Name + ".cs");
                if (!string.IsNullOrEmpty(clsPath))
                {
                    GenerateFileCode(clsPath, type);
                }
            }
        }
    }

    private static void GenerateFileCode(string clsPath, Type type)
    {
        //代码缩进数量
        var indentation = string.IsNullOrEmpty(type.Namespace) ? 4 : 8;
        
        string clsStr = File.ReadAllText(clsPath);
        //字段
        var fields = type.GetFields(InstanceBindFlags).Where(m => !m.IsSpecialName).ToArray();
        //属性
        var properties = type.GetProperties(InstanceBindFlags).Where(m => !m.IsSpecialName).ToArray();
        //函数
        var methods = type.GetMethods(InstanceBindFlags).Where(m => !m.IsSpecialName).ToArray();
        
        var setCode = GenerateSetCode(indentation, "__SetValue", fields, properties);
        Console.WriteLine("------------- setCode: " + type.Name + "\n" + setCode);
        
        var getCode = GenerateGetCode(indentation, "__GetValue", fields, properties);
        Console.WriteLine("------------- getCode: " + type.Name + "\n" + getCode);
        
        var invokeCode = GenerateInvokeCode(indentation, "__InvokeMethod", fields, properties, methods);
        Console.WriteLine("------------- invokeCode: " + type.Name + "\n" + invokeCode);
        
        var match = MatchMethod(clsStr, "__InvokeMethod");
        //Console.WriteLine(match.Value);
    }

    private static string GenerateSetCode(int indentation, string method, FieldInfo[] fieldInfos, PropertyInfo[] propertyInfos)
    {
        var indentationStr = "";
        for (int i = 0; i < indentation; i++)
        {
            indentationStr += ' ';
        }
        var str = indentationStr + "public virtual void " + method + "(string key, SValue value)\n";
        str += indentationStr + "{\n";
        
        string caseStr = "";

        for (int i = 0; i < fieldInfos.Length; i++)
        {
            var temp = fieldInfos[i];
            if (temp.IsPublic && !temp.Name.EndsWith("k__BackingField"))
            {
                caseStr += indentationStr + $"        case \"{temp.Name}\":\n";
                caseStr += indentationStr + $"            {temp.Name} = value;\n";
                caseStr += indentationStr + $"            break;\n";
            }
        }
        for (int i = 0; i < propertyInfos.Length; i++)
        {
            var temp = propertyInfos[i];
            var setMethod = temp.SetMethod;
            if (setMethod != null && setMethod.IsPublic)
            {
                caseStr += indentationStr + $"        case \"{temp.Name}\":\n";
                caseStr += indentationStr + $"            {temp.Name} = value;\n";
                caseStr += indentationStr + $"            break;\n";
            }
        }

        if (!string.IsNullOrEmpty(caseStr))
        {
            str += indentationStr + "    switch (key)\n";
            str += indentationStr + "    {\n";
            str += caseStr;
            str += indentationStr + "    }\n";
        }
        
        str += indentationStr + "}\n";
        return str;
    }
    
    private static string GenerateGetCode(int indentation, string method, FieldInfo[] fieldInfos, PropertyInfo[] propertyInfos)
    {
        var indentationStr = "";
        for (int i = 0; i < indentation; i++)
        {
            indentationStr += ' ';
        }
        var str = indentationStr + "public virtual SValue " + method + "(string key)\n";
        str += indentationStr + "{\n";
        
        string caseStr = "";

        for (int i = 0; i < fieldInfos.Length; i++)
        {
            var temp = fieldInfos[i];
            if (temp.IsPublic && !temp.Name.EndsWith("k__BackingField"))
            {
                caseStr += indentationStr + $"        case \"{temp.Name}\":\n";
                caseStr += indentationStr + $"            return {temp.Name};\n";
            }
        }
        for (int i = 0; i < propertyInfos.Length; i++)
        {
            var temp = propertyInfos[i];
            var getMethod = temp.GetMethod;
            if (getMethod != null && getMethod.IsPublic)
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
        
        str += indentationStr + "    return SValue.Null;\n";
        str += indentationStr + "}\n";
        return str;
    }
    
    private static string GenerateInvokeCode(int indentation, string method, FieldInfo[] fieldInfos, PropertyInfo[] propertyInfos, MethodInfo[] methodInfos)
    {
        var indentationStr = "";
        for (int i = 0; i < indentation; i++)
        {
            indentationStr += ' ';
        }
        var str = indentationStr + "public virtual SValue " + method + "(string key, params SValue[] ps)\n";
        str += indentationStr + "{\n";
        
        string caseStr = "";

        for (int i = 0; i < fieldInfos.Length; i++)
        {
            var temp = fieldInfos[i];
            if (temp.IsPublic && !temp.Name.EndsWith("k__BackingField"))
            {
                caseStr += indentationStr + $"        case \"{temp.Name}\":\n";
                caseStr += indentationStr + $"            return {temp.Name}.Invoke(ps);\n";
            }
        }
        for (int i = 0; i < propertyInfos.Length; i++)
        {
            var temp = propertyInfos[i];
            var getMethod = temp.GetMethod;
            if (getMethod != null && getMethod.IsPublic)
            {
                caseStr += indentationStr + $"        case \"{temp.Name}\":\n";
                caseStr += indentationStr + $"            return {temp.Name}.Invoke(ps);\n";
            }
        }

        //将函数归类排列
        var dic = new Dictionary<string, Dictionary<int, MethodInfo>>();
        for (int i = 0; i < methodInfos.Length; i++)
        {
            var temp = methodInfos[i];
            if (temp.IsPublic && !Methods.Contains(temp.Name))
            {
                if (!dic.TryGetValue(temp.Name, out var methodMap))
                {
                    methodMap = new Dictionary<int, MethodInfo>();
                    dic.Add(temp.Name, methodMap);
                }
                methodMap.Add(temp.GetParameters().Length, temp);
            }
        }
        
        //拼接函数
        foreach (var item in dic)
        {
            caseStr += indentationStr + $"        case \"{item.Key}\":\n";
            caseStr += indentationStr + $"            switch (ps.Length)\n";
            caseStr += indentationStr + "            {\n";
            foreach (var mi in item.Value)
            {
                caseStr += indentationStr + $"                case {mi.Key}:\n";
                var paramsStr = "";
                for (int i = 0; i < mi.Key; i++)
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
        
        str += indentationStr + "    return SValue.Null;\n";
        str += indentationStr + "}\n";
        return str;
    }

    
    //匹配函数代码
    private static Match MatchMethod(string code, string method)
    {
        var len = Regex.Match(Regex.Match(code, @$"[^\n]+{method}\(").Value, @"\s+").Value.Length;
        return Regex.Match(code, @$"[^\n]+{method}\([\s\S]+\s{{{len}}}}}");
    }
    
    //根据类名寻找源文件路径
    private static string FindFileByClsName(DirectoryInfo directoryInfo, string clsName)
    {
        var files = directoryInfo.GetFiles();

        foreach (var item in files)
        {
            if (item.Name == clsName)
            {
                return item.FullName;
            }
        }
        
        var directories = directoryInfo.GetDirectories();
        foreach (var item in directories)
        {
            var result = FindFileByClsName(directoryInfo, clsName);
            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }
        }
        return null;
    }
}
