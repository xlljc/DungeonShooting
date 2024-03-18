
using System.Collections.Generic;
using Godot;

[EffectFragment("AreaTrigger", "")]
public class Eff_AreaTrigger : EffectFragment
{
    private Prop5003Area _areaNode;
    private List<Weapon> _weaponList = new List<Weapon>();
    
    public override void Ready()
    {
        _areaNode = ResourceManager.LoadAndInstantiate<Prop5003Area>(ResourcePath.prefab_prop_activeComp_Prop5003_Area_tscn);
        _areaNode.BodyEntered += OnBodyEntered;
        _areaNode.BodyExited += OnBodyExited;
        AddChild(_areaNode);
    }

    public override void OnDestroy()
    {
        _areaNode.QueueFree();
    }

    public override void OnUse()
    {
        _areaNode.PlayEffect(0, 250, 6);
    }

    public override void Process(float delta)
    {
        for (var i = 0; i < _weaponList.Count; i++)
        {
            var weapon = _weaponList[i];
            if (weapon.Master != null)
            {
                _weaponList.RemoveAt(i--);
                continue;
            }
            weapon.Trigger(Role, false);
        }
    }

    public override void OnPickUpItem()
    {
        RemoveChild(_areaNode);
        Role.AnimatedSprite.AddChild(_areaNode);
    }

    public override void OnRemoveItem()
    {
        Role.AnimatedSprite.RemoveChild(_areaNode);
        AddChild(_areaNode);
    }

    private void OnBodyEntered(Node2D node)
    {
        if (node is Weapon weapon && weapon.Master == null)
        {
            if (!_weaponList.Contains(weapon))
            {
                _weaponList.Add(weapon);
            }
        }
    }
    
    private void OnBodyExited(Node2D node)
    {
        if (node is Weapon weapon && weapon.Master == null)
        {
            _weaponList.Remove(weapon);
        }
    }
}