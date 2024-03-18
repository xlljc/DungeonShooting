
using Godot;

[EffectFragment("AreaTrigger", "")]
public class Eff_AreaTrigger : EffectFragment
{
    private Area2D _areaNode;
    private CollisionShape2D _shapeNode;
    private CircleShape2D _shape;
    
    public override void Ready()
    {
        _areaNode = new Area2D();
        _shapeNode = new CollisionShape2D();
        _shape = new CircleShape2D();
        _shapeNode.Shape = _shape;
        
        _areaNode.AddChild(_shapeNode);
        AddChild(_areaNode);
    }

    public override void OnDestroy()
    {
        _areaNode.QueueFree();
        _shapeNode.QueueFree();
    }

    public override void OnUse()
    {
        
    }

    public override void OnPickUpItem()
    {
        RemoveChild(_areaNode);
        Role.AddChild(_areaNode);
    }

    public override void OnRemoveItem()
    {
        Role.RemoveChild(_areaNode);
        AddChild(_areaNode);
    }
}