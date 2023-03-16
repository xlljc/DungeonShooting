#if TOOLS
using System;
using Generator;
using Godot;

namespace Plugin
{
    [Tool]
    public partial class Plugin : EditorPlugin
    {
        public const string UiResourcePath = "";
        
        public static Plugin Instance => _instance;
        private static Plugin _instance;

        private Control dock;

        //ui监听器
        private NodeMonitor _uiMonitor;

        public override void _Process(double delta)
        {
            _instance = this;
            if (_uiMonitor != null)
            {
                _uiMonitor.Process((float) delta);
            }
        }

        public override void _EnterTree()
        {
            _instance = this;
            var script = GD.Load<Script>("res://src/framework/activity/ActivityObjectTemplate.cs");
            var texture = GD.Load<Texture2D>("res://addons/dungeonShooting_plugin/ActivityObject.svg");
            AddCustomType("ActivityObjectTemplate", "Node", script, texture);

            var script2 = GD.Load<Script>("res://src/framework/map/DungeonRoomTemplate.cs");
            var texture2 = GD.Load<Texture2D>("res://addons/dungeonShooting_plugin/Map.svg");
            AddCustomType("DungeonRoomTemplate", "TileMap", script2, texture2);
            
            var script3 = GD.Load<Script>("res://src/framework/map/mark/ActivityMark.cs");
            var texture3 = GD.Load<Texture2D>("res://addons/dungeonShooting_plugin/Mark.svg");
            AddCustomType("ActivityMark", "Node2D", script3, texture3);
            
            var script4 = GD.Load<Script>("res://src/framework/map/mark/EnemyMark.cs");
            AddCustomType("EnemyMark", "Node2D", script4, texture3);
            
            var script5 = GD.Load<Script>("res://src/framework/map/mark/WeaponMark.cs");
            AddCustomType("WeaponMark", "Node2D", script5, texture3);
            
            dock = GD.Load<PackedScene>("res://addons/dungeonShooting_plugin/EditorTools.tscn").Instantiate<Control>();
            //AddControlToDock(DockSlot.LeftUr, dock);
            
            //AddControlToContainer();
            var editorMainScreen = GetEditorInterface().GetEditorMainScreen();
            editorMainScreen.AddChild(dock);
            _MakeVisible(false);

            SceneChanged += OnSceneChanged;

            _uiMonitor = new NodeMonitor();
            _uiMonitor.SceneNodeChangeEvent += OnUiSceneChange;
        }

        public override void _ExitTree()
        {
            RemoveCustomType("ActivityObjectTemplate");
            RemoveCustomType("DungeonRoomTemplate");
            RemoveCustomType("ActivityMark");
            RemoveCustomType("EnemyMark");
            RemoveCustomType("WeaponMark");

            try
            {
                if (dock != null)
                {
                    //RemoveControlFromDocks(dock);
                    dock.Free();
                    dock = null;
                }
                SceneChanged -= OnSceneChanged;
                _uiMonitor.SceneNodeChangeEvent -= OnUiSceneChange;
                _uiMonitor = null;
            }
            catch (Exception e)
            {
                GD.PrintErr(e.ToString());
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
            if (dock != null)
            {
                dock.Visible = visible;
            }
        }

        /// <summary>
        /// 切换场景
        /// </summary>
        private void OnSceneChanged(Node node)
        {
            if (CheckIsUi(node))
            {
                _uiMonitor.ChangeCurrentNode(node);
            }
        }

        private bool CheckIsUi(Node node)
        {
            var resourcePath = (node.GetScript().Obj as CSharpScript)?.ResourcePath;
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

        private void OnUiSceneChange(Node node)
        {
            var name = node.Name.ToString();
            UiGenerator.GenerateUiFromEditor(node,  GameConfig.UiCodeDir + name.FirstToLower() + "/" + name + ".cs");
        }
    }
}
#endif
