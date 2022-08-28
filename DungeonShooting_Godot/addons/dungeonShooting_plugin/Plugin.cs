#if TOOLS
using Godot;

namespace Plugin
{
    [Tool]
    public class Plugin : EditorPlugin
    {
        public static Plugin Instance => _instance;
        private static Plugin _instance;

        public override void _Process(float delta)
        {
            _instance = this;
        }

        public override void _EnterTree()
        {
            _instance = this;
            var script = GD.Load<Script>("res://addons/dungeonShooting_plugin/ActivityObjectTemplate.cs");
            var texture = GD.Load<Texture>("res://addons/dungeonShooting_plugin/ActivityObject.svg");
            AddCustomType("ActivityObjectTemplate", "Node", script, texture);
        }

        public override void _ExitTree()
        {
            RemoveCustomType("ActivityObjectTemplate");
        }

        /*public override bool Handles(Object @object)
        {
            if (@object is Node node)
            {
                node.
                GD.Print("node: " + (node.GetScript() == activityObjectTemplateScript));
                /*GD.Print("---------------------- 1: " + objectTemplate.Name);
                var sp = new Sprite();
                sp.Name = "Sprite";
                objectTemplate.AddChild(sp);
                sp.Owner = objectTemplate.Owner;#1#
            }
            return base.Handles(@object);
        }*/
    }

#endif
}
