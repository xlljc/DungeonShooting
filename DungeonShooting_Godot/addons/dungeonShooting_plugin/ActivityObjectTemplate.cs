using Godot;

namespace Plugin
{
    /// <summary>
    /// ActivityObject 节点模板对象
    /// </summary>
    [Tool]
    public class ActivityObjectTemplate : Node
    {
        /// <summary>
        /// 当前物体所属物理层
        /// </summary>
        [Export(PropertyHint.Layers2dPhysics)]
        public uint CollisionLayer;
        
        /// <summary>
        /// 当前物体扫描的物理层
        /// </summary>
        [Export(PropertyHint.Layers2dPhysics)]
        public uint CollisionMask;

        public override void _Ready()
        {
            // 在工具模式下创建的 template 节点自动创建对应的必要子节点
            if (Engine.EditorHint)
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
                    else if (Plugin.Instance.GetEditorInterface().GetEditedSceneRoot() == this)
                    {
                        owner = this;
                    }
                    else
                    {
                        owner = parent;
                    }

                    //创建 Sprite
                    if (GetNodeOrNull("AnimatedSprite") == null)
                    {
                        var sp = new AnimatedSprite();
                        sp.Name = "AnimatedSprite";
                        AddChild(sp);
                        sp.Owner = owner;
                    }

                    //创建Shadow
                    if (GetNodeOrNull("ShadowSprite") == null)
                    {
                        var sd = new Sprite();
                        sd.Name = "ShadowSprite";
                        sd.Material = ResourceManager.ShadowMaterial;
                        AddChild(sd);
                        sd.Owner = owner;
                    }

                    //创建Collision
                    if (GetNodeOrNull("Collision") == null)
                    {
                        var co = new CollisionShape2D();
                        co.Name = "Collision";
                        AddChild(co);
                        co.Owner = owner;
                    }
                }
            }
        }
    }
}