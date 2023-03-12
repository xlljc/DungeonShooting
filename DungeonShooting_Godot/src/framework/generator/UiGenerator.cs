
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
    public static void GenerateUi(Control control, string path)
    {
        _nodeIndex = 0;
        var uiNode = EachNode(control);
        var code = GenerateClassCode(uiNode);
        File.WriteAllText(path, code);
    }

    private static string GenerateClassCode(UiNode uiNode)
    {
        return $"namespace UI;\n\n" +
               $"/// <summary>\n" + 
               $"/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失\n" + 
               $"/// </summary>\n" + 
               $"public abstract partial class {uiNode.OriginName} : UiBase\n" +
               $"{{\n" +
               GeneratePropertyListClassCode("", uiNode.OriginName + ".", uiNode, "    ") +
               $"\n\n" +
               GenerateAllChildrenClassCode(uiNode.OriginName + ".", uiNode, "    ") +
               $"}}\n";
    }

    private static string GenerateAllChildrenClassCode(string parent, UiNode uiNode, string retraction)
    {
        var str = "";
        if (uiNode.Children != null)
        {
            for (var i = 0; i < uiNode.Children.Count; i++)
            {
                var item = uiNode.Children[i];
                str += GenerateAllChildrenClassCode(parent + item.OriginName + ".", item, retraction);
                str += GenerateChildrenClassCode(parent, item, retraction);
            }
        }

        return str;
    }
    
    private static string GenerateChildrenClassCode(string parent, UiNode uiNode, string retraction)
    {
        return retraction + $"/// <summary>\n" + 
               retraction + $"/// 类型: <see cref=\"{uiNode.TypeName}\"/>, 路径: {parent}{uiNode.OriginName}\n" + 
               retraction + $"/// </summary>\n" + 
               retraction + $"public class {uiNode.ClassName}\n" +
               retraction + $"{{\n" +
               retraction + $"    /// <summary>\n" +
               retraction + $"    /// Ui节点实例, 节点类型: <see cref=\"{uiNode.TypeName}\"/>, 节点路径: {parent}{uiNode.OriginName}\n" +
               retraction + $"    /// </summary>\n" +
               retraction + $"    public {uiNode.TypeName} Instance {{ get; }}\n\n" +
               GeneratePropertyListClassCode("Instance.", parent, uiNode, retraction + "    ") + 
               retraction + $"    public {uiNode.ClassName}({uiNode.TypeName} node) => Instance = node;\n" +
               retraction + $"    public {uiNode.ClassName} Clone() => new (({uiNode.TypeName})Instance.Duplicate());\n" +
               retraction + $"}}\n\n";
    }

    private static string GeneratePropertyListClassCode(string target, string parent, UiNode uiNode, string retraction)
    {
        var str = "";
        if (uiNode.Children != null)
        {
            for (var i = 0; i < uiNode.Children.Count; i++)
            {
                var item = uiNode.Children[i];
                str += GeneratePropertyCode(target, parent, item, retraction);
            }
        }

        return str;
    }
    
    private static string GeneratePropertyCode(string target, string parent, UiNode uiNode, string retraction)
    {
        return retraction + $"/// <summary>\n" + 
               retraction + $"/// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref=\"{uiNode.TypeName}\"/>, 节点路径: {parent}{uiNode.OriginName}\n" + 
               retraction + $"/// </summary>\n" + 
               retraction + $"public {uiNode.ClassName} {uiNode.Name}\n" +
               retraction + $"{{\n" + 
               retraction + $"    get\n" + 
               retraction + $"    {{\n" + 
               retraction + $"        if (_{uiNode.Name} == null) _{uiNode.Name} = new {uiNode.ClassName}({target}GetNode<{uiNode.TypeName}>(\"{uiNode.OriginName}\"));\n" + 
               retraction + $"        return _{uiNode.Name};\n" + 
               retraction + $"    }}\n" + 
               retraction + $"}}\n" +
               retraction + $"private {uiNode.ClassName} _{uiNode.Name};\n\n";
    }
    
    private static UiNode EachNode(Node node)
    {
        var name = Regex.Replace(node.Name, "[^\\w_]", "");
        var uiNode = new UiNode("L_" + name, name, "UiNode" + (_nodeIndex++) + "_" + name, node.GetType().FullName);

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
                        uiNode.Children = new List<UiNode>();
                    }

                    uiNode.Children.Add(EachNode(children));
                }
            }
        }

        return uiNode;
    }

    private class UiNode
    {
        public string Name;
        public string OriginName;
        public string ClassName;
        public string TypeName;
        public List<UiNode> Children;

        public UiNode(string name, string originName, string className, string typeName)
        {
            Name = name;
            OriginName = originName;
            ClassName = className;
            TypeName = typeName;
        }
    }
    
}