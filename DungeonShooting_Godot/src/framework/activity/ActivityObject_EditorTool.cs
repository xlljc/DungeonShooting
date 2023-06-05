

using System;
using System.Collections.Generic;
using System.Reflection;
using Godot;
using UI.EditorTools;

public partial class ActivityObject
{
    /// <summary>
    /// 该函数只会在编辑器中调用, 用于自定义处理被 [ExportFill] 标记后自动创建的节点
    /// </summary>
    /// <param name="propertyName">属性名称</param>
    /// <param name="node">节点实例</param>
    protected virtual void OnExportFillNode(string propertyName, Node node)
    {
        switch (propertyName)
        {
            case "ShadowSprite":
            {
                var sprite = (Sprite2D)node;
                sprite.ZIndex = -1;
                var material =
                    ResourceManager.Load<ShaderMaterial>(ResourcePath.resource_material_Blend_tres, false);
                material.SetShaderParameter("blend", new Color(0, 0, 0, 0.47058824F));
                material.SetShaderParameter("schedule", 1);
                sprite.Material = material;
            }
                break;
            case "AnimatedSprite":
            {
                var animatedSprite = (AnimatedSprite2D)node;
                var material =
                    ResourceManager.Load<ShaderMaterial>(ResourcePath.resource_material_Blend_tres, false);
                material.SetShaderParameter("blend", new Color(1, 1, 1, 1));
                material.SetShaderParameter("schedule", 0);
                animatedSprite.Material = material;
            }
                break;
            case "Collision":
            {

            }
                break;
        }
    }

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
            foreach (var propertyInfo in propertyInfoList)
            {
                var value = propertyInfo.GetValue(this);
                if (value == null || ((Node)value).GetParent() == null)
                {
                    var node = GetNodeOrNull(propertyInfo.Name);
                    if (node == null)
                    {
                        node = (Node)Activator.CreateInstance(propertyInfo.PropertyType);
                        AddChild(node);
                        node.Name = propertyInfo.Name;
                        node.Owner = owner;
                        //自定义处理导出的节点
                        OnExportFillNode(propertyInfo.Name, node);
                    }
                    propertyInfo.SetValue(this, node);
                }
            }
        }
    }
}