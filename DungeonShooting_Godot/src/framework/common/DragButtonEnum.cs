
using System;

/// <summary>
/// 用于拖拽的鼠标键位
/// </summary>
[Flags]
public enum DragButtonEnum
{
    /// <summary>
    /// 鼠标左键
    /// </summary>
    Left = 0b1,
    /// <summary>
    /// 鼠标右键
    /// </summary>
    Right = 0b10,
    /// <summary>
    /// 鼠标中键
    /// </summary>
    Middle = 0b100,
}