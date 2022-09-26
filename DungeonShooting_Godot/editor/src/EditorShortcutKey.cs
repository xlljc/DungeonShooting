using System;
using Godot;

namespace DScript.GodotEditor
{
    /// <summary>
    /// 用在静态函数上, 当按下指定键位后调用函数, 仅在 DScript.GodotEditor 命名空间下生效
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class EditorShortcutKey : Attribute
    {
        /// <summary>
        /// 绑定的快捷键
        /// </summary>
        public KeyList Key;

        /// <summary>
        /// 是否检测 Ctrl
        /// </summary>
        public bool Ctrl = false;

        /// <summary>
        /// 是否检测 Shift
        /// </summary>
        public bool Shift = false;

        /// <summary>
        /// 是否检测 Alt
        /// </summary>
        public bool Alt = false;

        public EditorShortcutKey(KeyList key)
        {
            Key = key;
        }
    }
}