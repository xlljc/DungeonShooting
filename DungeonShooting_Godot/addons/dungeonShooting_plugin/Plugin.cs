#if TOOLS
using System;
using Generator;
using Godot;
using UI.EditorTools;

namespace Plugin
{
    [Tool]
    public partial class Plugin : EditorPlugin
    {
        /// <summary>
        /// 自定义节点类型数据
        /// </summary>
        private class CustomTypeInfo
        {
            public CustomTypeInfo(string name, string parentName, string scriptPath, string iconPath)
            {
                Name = name;
                ParentName = parentName;
                ScriptPath = scriptPath;
                IconPath = iconPath;
            }

            public string Name;
            public string ParentName;
            public string ScriptPath;
            public string IconPath;
        }
        
        /// <summary>
        /// 当前插件实例
        /// </summary>
        public static Plugin Instance { get; private set; }

        //工具面板
        private EditorToolsPanel _editorTools;

        //ui监听器
        private NodeMonitor _uiMonitor;

        //自定义节点
        private CustomTypeInfo[] _customTypeInfos = new CustomTypeInfo[]
        {
            new CustomTypeInfo(
                "ActivityObjectTemplate",
                "Node",
                "res://src/framework/activity/ActivityObjectTemplate.cs",
                "res://addons/dungeonShooting_plugin/ActivityObject.svg"
            ),
            new CustomTypeInfo(
                "DungeonRoomTemplate",
                "TileMap",
                "res://src/framework/activity/ActivityObjectTemplate.cs",
                "res://addons/dungeonShooting_plugin/Map.svg"
            ),
            new CustomTypeInfo(
                "ActivityMark",
                "Node2D",
                "res://src/framework/map/mark/ActivityMark.cs",
                "res://addons/dungeonShooting_plugin/Mark.svg"
            ),
            new CustomTypeInfo(
                "EnemyMark",
                "Node2D",
                "res://src/framework/map/mark/EnemyMark.cs",
                "res://addons/dungeonShooting_plugin/Mark.svg"
            ),
            new CustomTypeInfo(
                "WeaponMark",
                "Node2D",
                "res://src/framework/map/mark/WeaponMark.cs",
                "res://addons/dungeonShooting_plugin/Mark.svg"
            ),
        };
        
        public override void _Process(double delta)
        {
            Instance = this;
            
            //GD.Print("_uiMonitor == null: " + (_uiMonitor == null));
            if (_uiMonitor != null)
            {
                _uiMonitor.Process((float) delta);
            }
            // else
            // {
            //     _uiMonitor = new NodeMonitor();
            //     OnSceneChanged(GetEditorInterface().GetEditedSceneRoot());
            // }
        }

        public override void _EnterTree()
        {
            Instance = this;

            if (_customTypeInfos != null)
            {
                //注册自定义节点
                foreach (var customTypeInfo in _customTypeInfos)
                {
                    try
                    {
                        var script = GD.Load<Script>(customTypeInfo.ScriptPath);
                        var icon = GD.Load<Texture2D>(customTypeInfo.IconPath);
                        AddCustomType(customTypeInfo.Name, customTypeInfo.ParentName, script, icon);
                    }
                    catch (Exception e)
                    {
                        GD.PrintErr(e.ToString());
                    }
                }
            }

            _editorTools = GD.Load<PackedScene>(ResourcePath.prefab_ui_EditorTools_tscn).Instantiate<EditorToolsPanel>();
            var editorMainScreen = GetEditorInterface().GetEditorMainScreen();
            editorMainScreen.AddChild(_editorTools);

            try
            {
                _editorTools.OnCreateUi();
            }
            catch (Exception e)
            {
                GD.PrintErr(e.ToString());
            }

            try
            {
                _editorTools.OnShowUi();
            }
            catch (Exception e)
            {
                GD.PrintErr(e.ToString());
            }
            
            
            _MakeVisible(false);
            
            //场景切换事件
            SceneChanged += OnSceneChanged;

            _uiMonitor = new NodeMonitor();
            _uiMonitor.SceneNodeChangeEvent += GenerateUiCode;
        }

        public override void _ExitTree()
        {
            //移除自定义节点
            if (_customTypeInfos != null)
            {
                foreach (var customTypeInfo in _customTypeInfos)
                {
                    try
                    {
                        RemoveCustomType(customTypeInfo.Name);
                    }
                    catch (Exception e)
                    {
                        GD.PrintErr(e.ToString());
                    }
                }
            }

            if (_editorTools != null)
            {
                try
                {
                    _editorTools.OnHideUi();
                }
                catch (Exception e)
                {
                    GD.PrintErr(e.ToString());
                }

                try
                {
                    _editorTools.OnDisposeUi();
                }
                catch (Exception e)
                {
                    GD.PrintErr(e.ToString());
                }

                _editorTools.Free();
                _editorTools = null;
            }

            SceneChanged -= OnSceneChanged;

            if (_uiMonitor != null)
            {
                _uiMonitor.SceneNodeChangeEvent -= GenerateUiCode;
                _uiMonitor = null;
            }
        }

        public override bool _HasMainScreen()
        {
            return true;
        }

        public override Texture2D _GetPluginIcon()
        {
            return GD.Load<Texture2D>("res://addons/dungeonShooting_plugin/Tool.svg");
        }

        public override string _GetPluginName()
        {
            return "Tools";
        }

        public override void _MakeVisible(bool visible)
        {
            if (_editorTools != null)
            {
                _editorTools.Visible = visible;
            }
        }
        
        
        /// <summary>
        /// 检查节点是否为UI节点
        /// </summary>
        public bool CheckIsUi(Node node)
        {
            var resourcePath = node.GetScript().As<CSharpScript>()?.ResourcePath;
            if (resourcePath == null)
            {
                return false;
            }

            if (resourcePath.StartsWith("res://src/game/ui/") && resourcePath.EndsWith("Panel.cs"))
            {
                var index = resourcePath.LastIndexOf("/", StringComparison.Ordinal);
                var uiName = resourcePath.Substring(index + 1, resourcePath.Length - index - 8 - 1);
                var codePath = "res://src/game/ui/" + uiName.FirstToLower() + "/" + uiName + "Panel.cs";
                if (ResourceLoader.Exists(codePath))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 执行生成ui代码操作
        /// </summary>
        public void GenerateUiCode(Node node)
        {
            UiGenerator.GenerateUiCodeFromEditor(node);
        }

        /// <summary>
        /// 切换场景
        /// </summary>
        private void OnSceneChanged(Node node)
        {
            if (_uiMonitor != null)
            {
                _uiMonitor.ChangeCurrentNode(null);
                if (CheckIsUi(node))
                {
                    _uiMonitor.ChangeCurrentNode(node);
                }
            }
        }

    }
}
#endif
