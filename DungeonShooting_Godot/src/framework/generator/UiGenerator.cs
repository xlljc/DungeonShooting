
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Godot;

namespace Generator;

/// <summary>
/// Ui类生成器
/// </summary>
public static class UiGenerator
{
    private static int _nodeIndex = 0;

    /// <summary>
    /// 根据指定ui节点生成相应的Ui类, 并保存到指定路径下
    /// </summary>
    public static void GenerateUi(Node control, string path)
    {
        _nodeIndex = 0;
        var uiNode = EachNode(control);
        var code = GenerateClassCode(uiNode);
        File.WriteAllText(path, code);
    }

    /// <summary>
    /// 从编辑器中生成ui代码
    /// </summary>
    public static void GenerateUiFromEditor(Node control, string path)
    {
        _nodeIndex = 0;
        var uiNode = EachNodeFromEditor(control);
        var code = GenerateClassCode(uiNode);
        File.WriteAllText(path, code);
    }

    private static string GenerateClassCode(UiNodeInfo uiNodeInfo)
    {
        return $"namespace UI.{uiNodeInfo.OriginName};\n\n" +
               $"/// <summary>\n" + 
               $"/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失\n" + 
               $"/// </summary>\n" + 
               $"public abstract partial class {uiNodeInfo.OriginName} : UiBase\n" +
               $"{{\n" +
               GeneratePropertyListClassCode("", uiNodeInfo.OriginName + ".", uiNodeInfo, "    ") +
               $"\n\n" +
               GenerateAllChildrenClassCode(uiNodeInfo.OriginName + ".", uiNodeInfo, "    ") +
               $"}}\n";
    }

    private static string GenerateAllChildrenClassCode(string parent, UiNodeInfo uiNodeInfo, string retraction)
    {
        var str = "";
        if (uiNodeInfo.Children != null)
        {
            for (var i = 0; i < uiNodeInfo.Children.Count; i++)
            {
                var item = uiNodeInfo.Children[i];
                str += GenerateAllChildrenClassCode(parent + item.OriginName + ".", item, retraction);
                str += GenerateChildrenClassCode(parent, item, retraction);
            }
        }

        return str;
    }
    
    private static string GenerateChildrenClassCode(string parent, UiNodeInfo uiNodeInfo, string retraction)
    {
        return retraction + $"/// <summary>\n" + 
               retraction + $"/// 类型: <see cref=\"{uiNodeInfo.TypeName}\"/>, 路径: {parent}{uiNodeInfo.OriginName}\n" + 
               retraction + $"/// </summary>\n" + 
               retraction + $"public class {uiNodeInfo.ClassName} : IUiNode<{uiNodeInfo.TypeName}, {uiNodeInfo.ClassName}>\n" +
               retraction + $"{{\n" +
               GeneratePropertyListClassCode("Instance.", parent, uiNodeInfo, retraction + "    ") + 
               retraction + $"    public {uiNodeInfo.ClassName}({uiNodeInfo.TypeName} node) : base(node) {{  }}\n" +
               retraction + $"    public override {uiNodeInfo.ClassName} Clone() => new (({uiNodeInfo.TypeName})Instance.Duplicate());\n" +
               retraction + $"}}\n\n";
    }

    private static string GeneratePropertyListClassCode(string target, string parent, UiNodeInfo uiNodeInfo, string retraction)
    {
        var str = "";
        if (uiNodeInfo.Children != null)
        {
            for (var i = 0; i < uiNodeInfo.Children.Count; i++)
            {
                var item = uiNodeInfo.Children[i];
                str += GeneratePropertyCode(target, parent, item, retraction);
            }
        }

        return str;
    }
    
    private static string GeneratePropertyCode(string target, string parent, UiNodeInfo uiNodeInfo, string retraction)
    {
        return retraction + $"/// <summary>\n" + 
               retraction + $"/// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref=\"{uiNodeInfo.TypeName}\"/>, 节点路径: {parent}{uiNodeInfo.OriginName}\n" + 
               retraction + $"/// </summary>\n" + 
               retraction + $"public {uiNodeInfo.ClassName} {uiNodeInfo.Name}\n" +
               retraction + $"{{\n" + 
               retraction + $"    get\n" + 
               retraction + $"    {{\n" + 
               retraction + $"        if (_{uiNodeInfo.Name} == null) _{uiNodeInfo.Name} = new {uiNodeInfo.ClassName}({target}GetNodeOrNull<{uiNodeInfo.TypeName}>(\"{uiNodeInfo.OriginName}\"));\n" + 
               retraction + $"        return _{uiNodeInfo.Name};\n" + 
               retraction + $"    }}\n" + 
               retraction + $"}}\n" +
               retraction + $"private {uiNodeInfo.ClassName} _{uiNodeInfo.Name};\n\n";
    }
    
    /// <summary>
    /// 递归解析节点, 并生成 UiNodeInfo 数据
    /// </summary>
    private static UiNodeInfo EachNode(Node node)
    {
        var originName = Regex.Replace(node.Name, "[^\\w_]", "");
        var uiNode = new UiNodeInfo("L_" + originName, originName, "UiNode" + (_nodeIndex++) + "_" + originName, node.GetType().FullName);

        var childCount = node.GetChildCount();
        if (childCount > 0)
        {
            for (var i = 0; i < childCount; i++)
            {
                var children = node.GetChild(i);
                if (children != null)
                {
                    if (uiNode.Children == null)
                    {
                        uiNode.Children = new List<UiNodeInfo>();
                    }

                    uiNode.Children.Add(EachNode(children));
                }
            }
        }

        return uiNode;
    }

    /// <summary>
    /// 在编辑器下递归解析节点, 由于编辑器下绑定用户脚本的节点无法直接判断节点类型, 那么就只能获取节点的脚本然后解析名称和命名空间
    /// </summary>
    private static UiNodeInfo EachNodeFromEditor(Node node)
    {
        UiNodeInfo uiNode;
        //原名称
        var originName = Regex.Replace(node.Name, "[^\\w_]", "");
        //字段名称
        var fieldName = "L_" + originName;
        //类定义该图层的类名
        var className = "UiNode" + (_nodeIndex++) + "_" + originName;

        var script = node.GetScript().As<CSharpScript>();
        if (script == null) //无脚本, 说明是内置节点
        {
            uiNode = new UiNodeInfo(fieldName, originName, className, node.GetType().FullName);
        }
        else //存在脚本
        {
            var index = script.ResourcePath.LastIndexOf("/", StringComparison.Ordinal);
            //文件名称
            var fileName = script.ResourcePath.Substring(index + 1, script.ResourcePath.Length - index - 3 - 1);
            //在源代码中寻找命名空间
            var match = Regex.Match(script.SourceCode, "(?<=namespace\\s+)[\\w\\.]+");
            if (match.Success) //存在命名空间
            {
                uiNode = new UiNodeInfo(fieldName, originName, className, match.Value + "." + fileName);
            }
            else //不存在命名空间
            {
                uiNode = new UiNodeInfo(fieldName, originName, className, fileName);
            }
        }
        
        var childCount = node.GetChildCount();
        if (childCount > 0)
        {
            for (var i = 0; i < childCount; i++)
            {
                var children = node.GetChild(i);
                if (children != null)
                {
                    if (uiNode.Children == null)
                    {
                        uiNode.Children = new List<UiNodeInfo>();
                    }

                    uiNode.Children.Add(EachNodeFromEditor(children));
                }
            }
        }
        
        return uiNode;
    }

    private class UiNodeInfo
    {
        public string Name;
        public string OriginName;
        public string ClassName;
        public string TypeName;
        public List<UiNodeInfo> Children;

        public UiNodeInfo(string name, string originName, string className, string typeName)
        {
            Name = name;
            OriginName = originName;
            ClassName = className;
            TypeName = typeName;
        }
    }
    
}