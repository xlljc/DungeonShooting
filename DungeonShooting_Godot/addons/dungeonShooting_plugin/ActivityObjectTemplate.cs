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
        /// 是否放入 ySort 节点下
        /// </summary>
        [Export] public bool UseYSort;

        /// <summary>
        /// 物体初始缩放
        /// </summary>
        [Export] public Vector2 Scale = Vector2.One;
        
        /// <summary>
        /// 当前物体所属物理层
        /// </summary>
        [Export(PropertyHint.Layers2dPhysics)] public uint CollisionLayer;

        /// <summary>
        /// 当前物体扫描的物理层
        /// </summary>
        [Export(PropertyHint.Layers2dPhysics)] public uint CollisionMask;

        /// <summary>
        /// 当前物体渲染层级
        /// </summary>
        [Export] public int ZIndex;

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

                    var sprite = GetNodeOrNull<Sprite>("ShadowSprite");
                    //创建Shadow
                    if (sprite == null)
                    {
                        sprite = new Sprite();
                        sprite.Name = "ShadowSprite";
                        var material =
                            ResourceManager.Load<ShaderMaterial>(ResourcePath.resource_materlal_Blend_tres, false);
                        material.SetShaderParam("blend", new Color(0, 0, 0, 0.47058824F));
                        material.SetShaderParam("schedule", 1);
                        sprite.Material = material;
                        AddChild(sprite);
                        sprite.Owner = owner;
                    }
                    else if (sprite.Material == null)
                    {
                        var material =
                            ResourceManager.Load<ShaderMaterial>(ResourcePath.resource_materlal_Blend_tres, false);
                        material.SetShaderParam("blend", new Color(0, 0, 0, 0.47058824F));
                        material.SetShaderParam("schedule", 1);
                        sprite.Material = material;
                    }

                    var animatedSprite = GetNodeOrNull<AnimatedSprite>("AnimatedSprite");
                    //创建 Sprite
                    if (animatedSprite == null)
                    {
                        animatedSprite = new AnimatedSprite();
                        animatedSprite.Name = "AnimatedSprite";
                        var material =
                            ResourceManager.Load<ShaderMaterial>(ResourcePath.resource_materlal_Blend_tres, false);
                        material.SetShaderParam("blend", new Color(1, 1, 1, 1));
                        material.SetShaderParam("schedule", 0);
                        animatedSprite.Material = material;
                        AddChild(animatedSprite);
                        animatedSprite.Owner = owner;
                    }
                    else if (animatedSprite.Material == null)
                    {
                        var material =
                            ResourceManager.Load<ShaderMaterial>(ResourcePath.resource_materlal_Blend_tres, false);
                        material.SetShaderParam("blend", new Color(1, 1, 1, 1));
                        material.SetShaderParam("schedule", 0);
                        animatedSprite.Material = material;
                    }

                    //创建Collision
                    if (GetNodeOrNull("Collision") == null)
                    {
                        var co = new CollisionShape2D();
                        co.Name = "Collision";
                        AddChild(co);
                        co.Owner = owner;
                    }

                    //创建AnimationPlayer
                    if (GetNodeOrNull("AnimationPlayer") == null)
                    {
                        var ap = new AnimationPlayer();
                        ap.Name = "AnimationPlayer";
                        ap.AddAnimation("RESET",
                            ResourceManager.Load<Animation>(
                                "res://addons/dungeonShooting_plugin/ActivityObjectReset.tres", false));
                        ap.AddAnimation("hit",
                            ResourceManager.Load<Animation>(
                                "res://addons/dungeonShooting_plugin/ActivityObjectHit.tres", false));
                        AddChild(ap);
                        ap.Owner = owner;
                    }
                }
            }
        }
    }
}