

using Godot;

public partial class ActivityObject
{
    private void _InitNodeInEditor()
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
            else if (Plugin.Plugin.Instance.GetEditorInterface().GetEditedSceneRoot() == this)
            {
                owner = this;
            }
            else
            {
                owner = parent;
            }

            var sprite = GetNodeOrNull<Sprite2D>("ShadowSprite");
            //创建Shadow
            if (sprite == null)
            {
                sprite = new Sprite2D();
                sprite.Name = "ShadowSprite";
                sprite.ZIndex = -1;
                var material =
                    ResourceManager.Load<ShaderMaterial>(ResourcePath.resource_material_Blend_tres, false);
                material.SetShaderParameter("blend", new Color(0, 0, 0, 0.47058824F));
                material.SetShaderParameter("schedule", 1);
                sprite.Material = material;
                AddChild(sprite);
                sprite.Owner = owner;
            }
            else if (sprite.Material == null)
            {
                var material =
                    ResourceManager.Load<ShaderMaterial>(ResourcePath.resource_material_Blend_tres, false);
                material.SetShaderParameter("blend", new Color(0, 0, 0, 0.47058824F));
                material.SetShaderParameter("schedule", 1);
                sprite.Material = material;
            }

            var animatedSprite = GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite");
            //创建 Sprite2D
            if (animatedSprite == null)
            {
                animatedSprite = new AnimatedSprite2D();
                animatedSprite.Name = "AnimatedSprite";
                var material =
                    ResourceManager.Load<ShaderMaterial>(ResourcePath.resource_material_Blend_tres, false);
                material.SetShaderParameter("blend", new Color(1, 1, 1, 1));
                material.SetShaderParameter("schedule", 0);
                animatedSprite.Material = material;
                AddChild(animatedSprite);
                animatedSprite.Owner = owner;
            }
            else if (animatedSprite.Material == null)
            {
                var material =
                    ResourceManager.Load<ShaderMaterial>(ResourcePath.resource_material_Blend_tres, false);
                material.SetShaderParameter("blend", new Color(1, 1, 1, 1));
                material.SetShaderParameter("schedule", 0);
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
        }
    }

    private Node GetOwnerInEditor()
    {
        return null;
    }
}