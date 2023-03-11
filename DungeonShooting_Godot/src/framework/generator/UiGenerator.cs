
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
    }

    private static UiNode EachNode(Control control)
    {
        var name = Regex.Replace(control.Name, "[^\\w_]", "");
        var uiNode = new UiNode(name, "UiNode" + (++_nodeIndex) + "_" + name, control.GetType().FullName);

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