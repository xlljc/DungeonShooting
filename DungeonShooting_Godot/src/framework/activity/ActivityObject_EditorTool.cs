
using System;
using System.Collections.Generic;
using System.Reflection;
using Godot;
using UI.EditorTools;

public partial class ActivityObject
{
    /// <summary>
    /// 该函数只会在编辑器中调用, 用于处理被 [ExportFill] 标记属性节点, 当在编辑器中切换页签或者创建 ActivityObject 时会调用该函数
    /// </summary>
    /// <param name="propertyName">属性名称</param>
    /// <param name="node">节点实例</param>
    /// <param name="isJustCreated">是否刚刚创建</param>
    protected virtual void OnExamineExportFillNode(string propertyName, Node node, bool isJustCreated)
    {
        switch (propertyName)
        {
            case "ShadowSprite":
            {
                var sprite = (Sprite2D)node;
                if (isJustCreated)
                {
                    sprite.ZIndex = -1;
                }

                if (sprite.Material == null)
                {
                    var material =
                        ResourceManager.Load<ShaderMaterial>(ResourcePath.resource_material_Blend_tres, false);
                    material.ResourceLocalToScene = true;
                    material.SetShaderParameter("blend", new Color(0, 0, 0, 0.47058824F));
                    material.SetShaderParameter("schedule", 1);
                    material.SetShaderParameter("modulate", new Color(1, 1, 1, 1));
                    sprite.Material = material;
                }
            }
                break;
            case "AnimatedSprite":
            {
                var animatedSprite = (AnimatedSprite2D)node;
                if (animatedSprite.Material == null)
                {
                    var material =
                        ResourceManager.Load<ShaderMaterial>(ResourcePath.resource_material_Blend_tres, false);
                    material.ResourceLocalToScene = true;
                    material.SetShaderParameter("blend", new Color(1, 1, 1, 1));
                    material.SetShaderParameter("schedule", 0);
                    material.SetShaderParameter("modulate", new Color(1, 1, 1, 1));
                    animatedSprite.Material = material;
                }
            }
                break;
            case "Collision":
            {

            }
                break;
        }
    }

#if TOOLS
    private void _InitNodeInEditor()
    {
        var parent = GetParent();
        if (parent != null)
        {
            //寻找 owner
            Node owner;
            if (parent.Owner != null)
            {
                owner = parent.Owner;
            }
            else if (Plugin.Plugin.Instance.GetEditorInterface().GetEditedSceneRoot() == this)
            {
                owner = this;
            }
            else
            {
                owner = parent;
            }

            var type = GetType();
            var propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var propertyInfoList = new List<PropertyInfo>();
            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.GetCustomAttributes(typeof(ExportFillNodeAttribute), false).Length > 0)
                {
                    if (propertyInfo.GetCustomAttributes(typeof(ExportAttribute), false).Length == 0)
                    {
                        EditorToolsPanel.ShowConfirmInEditor("警告", $"'{type.FullName}'中字段'{propertyInfo.Name}'使用了[ExportAutoFill],\n但是并没有加上[Export], 请补上!");
                        return;
                    }

                    if (propertyInfo.PropertyType.IsAssignableTo(typeof(Node)))
                    {
                        if (propertyInfo.SetMethod == null)
                        {
                            EditorToolsPanel.ShowConfirmInEditor("警告", $"请为'{type.FullName}'中的'{propertyInfo.Name}'属性设置set访问器, 或者将set服务器设置成public!");
                            return;
                        }
                        propertyInfoList.Add(propertyInfo);
                    }
                }
            }

            var tempList = new List<PropertyInfo>();
            Type tempType = null;
            var index = -1;
            for (int i = propertyInfoList.Count - 1; i >= 0; i--)
            {
                var item = propertyInfoList[i];
                if (tempType != item.DeclaringType || i == 0)
                {
                    if (tempType == null)
                    {
                        index = i;
                    }
                    else
                    {
                        int j;
                        if (i == 0)
                        {
                            j = i;
                        }
                        else
                        {
                            j = i + 1;
                        }
                        for (; j <= index; j++)
                        {
                            tempList.Add(propertyInfoList[j]);
                        }

                        index = i;
                    }
                    tempType = item.DeclaringType;
                }
            }
            
            foreach (var propertyInfo in tempList)
            {
                var value = propertyInfo.GetValue(this);
                if (value == null || ((Node)value).GetParent() == null)
                {
                    var node = _FindNodeInChild(this, propertyInfo.Name, propertyInfo.PropertyType);
                    if (node == null)
                    {
                        node = (Node)Activator.CreateInstance(propertyInfo.PropertyType);
                        AddChild(node);
                        node.Name = propertyInfo.Name;
                        node.Owner = owner;
                        
                        OnExamineExportFillNode(propertyInfo.Name, node, true);
                    }
                    else
                    {
                        OnExamineExportFillNode(propertyInfo.Name, node, false);
                    }
                    propertyInfo.SetValue(this, node);
                }
                else
                {
                    OnExamineExportFillNode(propertyInfo.Name, (Node)value, false);
                }
            }
        }
    }

    private Node _FindNodeInChild(Node node, string name, Type type)
    {
        var childCount = node.GetChildCount();
        for (int i = 0; i < childCount; i++)
        {
            var child = node.GetChild(i);
            if (child.Name == name && child.GetType().IsAssignableTo(type))
            {
                return child;
            }
            else
            {
                var result = _FindNodeInChild(child, name, type);
                if (result != null)
                {
                    return result;
                }
            }
        }

        return null;
    }
#endif
}