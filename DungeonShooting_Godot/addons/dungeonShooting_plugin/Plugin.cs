#if TOOLS
using Godot;

namespace Plugin
{
    [Tool]
    public partial class Plugin : EditorPlugin
    {
        public static Plugin Instance => _instance;
        private static Plugin _instance;

        private Control dock;

        public override void _Process(double delta)
        {
            _instance = this;
        }

        public override void _EnterTree()
        {
            _instance = this;
            var script = GD.Load<Script>("res://src/framework/activity/ActivityObjectTemplate.cs");
            var texture = GD.Load<Texture2D>("res://addons/dungeonShooting_plugin/ActivityObject.svg");
            AddCustomType("ActivityObjectTemplate", "Node", script, texture);
            
            dock = GD.Load<PackedScene>("res://addons/dungeonShooting_plugin/Automation.tscn").Instantiate<Control>();
            AddControlToDock(DockSlot.LeftUr, dock);
        }

        public override void _ExitTree()
        {
            RemoveCustomType("ActivityObjectTemplate");
            RemoveControlFromDocks(dock);
            dock.Free();
        }

        /*public override bool Handles(Object @object)
        {
            if (@object is Node node)
            {
                node.
                GD.Print("node: " + (node.GetScript() == activityObjectTemplateScript));
                /*GD.Print("---------------------- 1: " + objectTemplate.Name);
                var sp = new Sprite2D();
                sp.Name = "Sprite2D";
                objectTemplate.AddChild(sp);
                sp.Owner = objectTemplate.Owner;#1#
            }
            return base.Handles(@object);
        }*/
    }

}
#endif
