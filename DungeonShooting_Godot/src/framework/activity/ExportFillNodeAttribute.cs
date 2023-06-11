
using System;

/// <summary>
/// 标记类型为Node的属性, 表示当前属性如果没有赋值, 则在编辑器中自动创建子节点, 必须搭配 [Export] 使用!
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ExportFillNodeAttribute : Attribute
{
}