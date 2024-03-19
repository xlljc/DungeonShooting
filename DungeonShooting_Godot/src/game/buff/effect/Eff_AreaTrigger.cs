
using System.Collections.Generic;
using Godot;

[EffectFragment("AreaTrigger", 
    "触发附近地上的武器开火, " +
    "参数1为最大作用半径, ")]
public class Eff_AreaTrigger : EffectFragment
{
    private Prop5003Area _areaNode;
    private List<Weapon> _weaponList = new List<Weapon>();
    private float _time = 0;
    
    private int _radius = 250;

    public override void InitParam(float arg1)
    {
        _radius = (int)arg1;
    }

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
        _areaNode.PlayEffect(0, _radius, Master.Attribute.Duration);
    }

    public override void OnUsingFinish()
    {
        foreach (var weapon in _weaponList)
        {
            weapon.ClearTriggerRole();
        }
        _weaponList.Clear();
    }

    public override void Process(float delta)
    {
        if (!Master.IsUsing)
        {
            _time = 0;
            return;
        }
        _time += delta;
        var flag = false;
        if (_time >= 0.5f)
        {
            flag = true;
            _time %= 0.5f;
        }
        for (var i = 0; i < _weaponList.Count; i++)
        {
            var weapon = _weaponList[i];
            if (weapon.Master != null)
            {
                _weaponList.RemoveAt(i--);
                weapon.ClearTriggerRole();
                continue;
            }

            if (flag)
            {
                var weaponBase = weapon.GetUseAttribute(Role);
                if (!weaponBase.ContinuousShoot)
                {
                    if (!weaponBase.LooseShoot)
                    {
                        continue;
                    }
                    else if (!weapon.IsCharging || weapon.IsChargeFinish())
                    {
                        continue;
                    }
                }
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
            if (_weaponList.Remove(weapon))
            {
                weapon.ClearTriggerRole();
            }
        }
    }
}