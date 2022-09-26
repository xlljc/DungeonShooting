using System;
using System.Collections.Generic;
using System.Reflection;
using Godot;

namespace DScript.GodotEditor
{
    /// <summary>
    /// 编辑器类
    /// </summary>
    public class Editor : Control
    {

        private class ShortcutKeyData
        {
            public int key;
            public bool ctrl;
            public bool shift;
            public bool alt;
            public MethodInfo methodInfo;
            public bool isPressed;
        }

        /// <summary>
        /// 获取编辑器实例
        /// </summary>
        public static Editor Instance => _instance;

        private static Editor _instance;

        private List<ShortcutKeyData> _shortcutKeyMethods = new List<ShortcutKeyData>();

        public Editor()
        {
            _instance = this;
        }

        public override void _Ready()
        {
            ScannerAssembly();
        }

        public override void _Process(float delta)
        {
            if (Input.IsKeyPressed((int)KeyList.Control))
            {

            }
        }

        public override void _Input(InputEvent @event)
        {
            if (!Visible)
            {
                return;
            }

            if (@event is InputEventKey eventKey)
            {
                uint key = eventKey.Scancode;
                //检测快捷键
                if (key != (int)KeyList.Control && key != (int)KeyList.Shift && key != (int)KeyList.Alt)
                {
                    for (int i = 0; i < _shortcutKeyMethods.Count; i++)
                    {
                        var item = _shortcutKeyMethods[i];
                        var flag = key == item.key && eventKey.Pressed && item.ctrl == eventKey.Control &&
                                   item.shift == eventKey.Shift && item.alt == eventKey.Alt;
                        if (flag)
                        {
                            //触发快捷键调用
                            if (!item.isPressed)
                            {
                                item.isPressed = true;
                                item.methodInfo.Invoke(null, new object[0]);
                            }
                        }
                        else
                        {
                            item.isPressed = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 扫描程序集, 并完成注册标记
        /// </summary>
        private void ScannerAssembly()
        {
            var types = GetType().Assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.Namespace == null || !type.Namespace.StartsWith("DScript.GodotEditor")) continue;
                MethodInfo[] methods =
                    type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                foreach (var method in methods)
                {
                    Attribute mAttribute;
                    //快捷键注册
                    if ((mAttribute = Attribute.GetCustomAttribute(method, typeof(EditorShortcutKey), false)) !=
                        null)
                    {
                        var att = (EditorShortcutKey)mAttribute;
                        var data = new ShortcutKeyData();
                        data.key = (int)att.Key;
                        data.methodInfo = method;
                        data.shift = att.Shift;
                        data.ctrl = att.Ctrl;
                        data.alt = att.Alt;
                        _shortcutKeyMethods.Add(data);
                    }
                }
            }
        }
    }
}