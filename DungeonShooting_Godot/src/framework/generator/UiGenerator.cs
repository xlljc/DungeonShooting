
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Godot;

namespace Generator;

public static class UiGenerator
{
    private static int _nodeIndex = 0;

    public static void GenerateUi(Control control)
    {
        _nodeIndex = 0;
        var uiNode = EachNode(control);
        var code = GenerateClassCode(uiNode);
        GD.Print("code: \n" + code);
    }

    private static string GenerateClassCode(UiNode uiNode)
    {
        return $"namespace UI;\n\n" +
               $"public abstract partial class {uiNode.Name} : UiBase\n" +
               $"{{\n" +
               GeneratePropertyListClassCode("", uiNode.Name + ".", uiNode, "    ") +
               $"\n\n" +
               GenerateAllChildrenClassCode(uiNode.Name + ".", uiNode, "    ") +
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
                str += GenerateAllChildrenClassCode(parent + item.Name + ".", item, retraction);
                str += GenerateChildrenClassCode(parent, item, retraction);
            }
        }

        return str;
    }
    
    private static string GenerateChildrenClassCode(string parent, UiNode uiNode, string retraction)
    {
        return retraction + $"/// <summary>\n" + 
               retraction + $"/// 类型: <see cref=\"{uiNode.TypeName}\"/>, 路径: {parent}{uiNode.Name}\n" + 
               retraction + $"/// </summary>\n" + 
               retraction + $"public class {uiNode.ClassName}\n" +
               retraction + $"{{\n" +
               retraction + $"    /// <summary>\n" +
               retraction + $"    /// Ui节点实例, 节点类型: <see cref=\"{uiNode.TypeName}\"/>, 节点路径: {parent}{uiNode.Name}\n" +
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
               retraction + $"/// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref=\"{uiNode.TypeName}\"/>, 节点路径: {parent}{uiNode.Name}\n" + 
               retraction + $"/// </summary>\n" + 
               retraction + $"public {uiNode.ClassName} {uiNode.Name}\n" +
               retraction + $"{{\n" + 
               retraction + $"    get\n" + 
               retraction + $"    {{\n" + 
               retraction + $"        if (_{uiNode.Name} == null) _{uiNode.Name} = new {uiNode.ClassName}({target}GetNode<{uiNode.TypeName}>(\"{uiNode.Name}\"));\n" + 
               retraction + $"        return _{uiNode.Name};\n" + 
               retraction + $"    }}\n" + 
               retraction + $"}}\n" +
               retraction + $"private {uiNode.ClassName} _{uiNode.Name};\n\n";
    }
    
    private static UiNode EachNode(Control control)
    {
        var name = Regex.Replace(control.Name, "[^\\w_]", "");
        var uiNode = new UiNode(name, "UiNode" + (_nodeIndex++) + "_" + name, control.GetType().FullName);

        var childCount = control.GetChildCount();
        if (childCount > 0)
        {
            for (var i = 0; i < childCount; i++)
            {
                var children = control.GetChildOrNull<Control>(i);
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
        public string ClassName;
        public string TypeName;
        public List<UiNode> Children;

        public UiNode(string name, string className, string typeName)
        {
            Name = name;
            ClassName = className;
            TypeName = typeName;
        }
    }
    
}