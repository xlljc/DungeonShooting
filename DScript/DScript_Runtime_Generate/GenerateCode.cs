
using System.Reflection;
using System.Text.RegularExpressions;

public static class GenerateCode
{
    //IObject口下所有函数名称, 生成代码时会忽略这些函数
    private static readonly string[] Methods =
        { "__GetValue", "__SetValue", "__InvokeMethod", "ToString", "GetType", "Equals", "GetHashCode" };


    private const BindingFlags InstanceBindFlags =
        BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly;

    //开始执行生成代码操作
    public static void StartGenerate(Uri projectPath)
    {
        //扫描程序集
        var assembly = typeof(OldIObject).Assembly;
        var types = assembly.GetTypes();
        for (int i = 0; i < types.Length; i++)
        {
            var type = types[i];
            if (type != typeof(OldIObject) && typeof(OldIObject).IsAssignableFrom(type))
            {
                //Console.WriteLine(type.FullName + ", " + (type.BaseType == typeof(object)));
                string clsPath = FindFileByClsName(new DirectoryInfo(projectPath.AbsolutePath + "/Environment/"),
                    type.Name + ".cs");
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
        var indentation = 4;
        var classIndentation = 0;
        if (!string.IsNullOrEmpty(type.Namespace))
        {
            indentation = 8;
            classIndentation = 4;
        }

        string clsStr = File.ReadAllText(clsPath);
        var classDescribe = ClassDescribe.CreateFromType(type.Name, type);

        var setCode = GenerateCodeUtils.GenerateSetValueCode(classDescribe, indentation);
        //Console.WriteLine("------------- setCode: " + type.Name + "\n" + setCode);

        var getCode = GenerateCodeUtils.GenerateGetValueCode(classDescribe, indentation);
        //Console.WriteLine("------------- getCode: " + type.Name + "\n" + getCode);

        var invokeCode = GenerateCodeUtils.GenerateInvokeCode(classDescribe, indentation);
        //Console.WriteLine("------------- invokeCode: " + type.Name + "\n" + invokeCode);

        //替换字符串
        var matchMethod = MatchMethod(clsStr, GenerateCodeUtils.InvokeMethodName);
        if (matchMethod.Success)
        {
            clsStr = clsStr.Substring(0, matchMethod.Index) + invokeCode +
                     clsStr.Substring(matchMethod.Index + matchMethod.Length);
        }
        else
        {
            var matchClass = MatchClass(clsStr, classDescribe.Name);
            if (matchClass.Success)
            {
                var tempStr = matchClass.Value;
                var index = tempStr.LastIndexOf('}');
                tempStr = tempStr.Substring(0, index - classIndentation) + invokeCode + tempStr.Substring(index - classIndentation);
                
                clsStr = clsStr.Substring(0, matchClass.Index) + tempStr +
                         clsStr.Substring(matchClass.Index + matchClass.Length);
            }
        }
        
        matchMethod = MatchMethod(clsStr, GenerateCodeUtils.GetValueMethodName);
        if (matchMethod.Success)
        {
            clsStr = clsStr.Substring(0, matchMethod.Index) + getCode +
                     clsStr.Substring(matchMethod.Index + matchMethod.Length);
        }
        else
        {
            var matchClass = MatchClass(clsStr, classDescribe.Name);
            if (matchClass.Success)
            {
                var tempStr = matchClass.Value;
                var index = tempStr.LastIndexOf('}');
                tempStr = tempStr.Substring(0, index - classIndentation) + getCode + tempStr.Substring(index - classIndentation);
                
                clsStr = clsStr.Substring(0, matchClass.Index) + tempStr +
                         clsStr.Substring(matchClass.Index + matchClass.Length);
            }
        }
        
        matchMethod = MatchMethod(clsStr, GenerateCodeUtils.SetValueMethodName);
        if (matchMethod.Success)
        {
            clsStr = clsStr.Substring(0, matchMethod.Index) + setCode +
                     clsStr.Substring(matchMethod.Index + matchMethod.Length);
        }
        else
        {
            var matchClass = MatchClass(clsStr, classDescribe.Name);
            if (matchClass.Success)
            {
                var tempStr = matchClass.Value;
                var index = tempStr.LastIndexOf('}');
                tempStr = tempStr.Substring(0, index - classIndentation) + setCode + tempStr.Substring(index - classIndentation);
                
                clsStr = clsStr.Substring(0, matchClass.Index) + tempStr +
                         clsStr.Substring(matchClass.Index + matchClass.Length);
            }
        }

        Console.WriteLine("--------------------- class\n" + clsStr);
        //Console.WriteLine(match.Value);
    }


    //匹配函数代码
    private static Match MatchMethod(string code, string method)
    {
        var len = Regex.Match(Regex.Match(code, @$"[^\n]+{method}\(").Value, @"\s+").Value.Length;
        return Regex.Match(code, @$"[^\n]+{method}\([\s\S]+\s{{{len}}}\}}\r?\n?");
    }
    
    //匹配函数代码
    private static Match MatchClass(string code, string className)
    {
        var len = Regex.Match(Regex.Match(code, @$"[^\n]+class[\w\s]+{className}").Value, @"\s+").Value.Length;
        return Regex.Match(code, @$"[^\n]+class[\w\s]+{className}[\s\S]+\s{{{len}}}\}}\r?\n?");
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
