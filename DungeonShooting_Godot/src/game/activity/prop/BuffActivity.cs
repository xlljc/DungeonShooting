
using System.Collections.Generic;
using Godot;

/// <summary>
/// 通用被动道具实体类
/// </summary>
[Tool]
public partial class BuffActivity : PropActivity
{
    //被动属性
    private readonly List<BuffFragment> _buffFragment = new List<BuffFragment>();
    
    public override void OnPickUpItem()
    {
        foreach (var buffFragment in _buffFragment)
        {
            buffFragment.OnPickUpItem();
        }
    }

    public override void OnRemoveItem()
    {
        foreach (var buffFragment in _buffFragment)
        {
            buffFragment.OnRemoveItem();
        }
    }

    /// <summary>
    /// 添加被动属性
    /// </summary>
    public void AddBuffFragment<T>() where T : BuffFragment, new()
    {
        var fragment = AddComponent<T>();
        _buffFragment.Add(fragment);
        if (Master != null)
        {
            fragment.OnPickUpItem();
        }
    }
    
    /// <summary>
    /// 添加被动属性
    /// </summary>
    public void AddBuffFragment<T>(float arg1) where T : BuffFragment, new()
    {
        var fragment = AddComponent<T>();
        _buffFragment.Add(fragment);
        fragment.InitParam(arg1);
        if (Master != null)
        {
            fragment.OnPickUpItem();
        }
    }
    
    /// <summary>
    /// 添加被动属性
    /// </summary>
    public void AddBuffFragment<T>(float arg1, float arg2) where T : BuffFragment, new()
    {
        var fragment = AddComponent<T>();
        _buffFragment.Add(fragment);
        fragment.InitParam(arg1, arg2);
        if (Master != null)
        {
            fragment.OnPickUpItem();
        }
    }
    
    /// <summary>
    /// 添加被动属性
    /// </summary>
    public void AddBuffFragment<T>(float arg1, float arg2, float arg3) where T : BuffFragment, new()
    {
        var fragment = AddComponent<T>();
        _buffFragment.Add(fragment);
        fragment.InitParam(arg1, arg2, arg3);
        if (Master != null)
        {
            fragment.OnPickUpItem();
        }
    }
    
    /// <summary>
    /// 添加被动属性
    /// </summary>
    public void AddBuffFragment<T>(float arg1, float arg2, float arg3, float arg4) where T : BuffFragment, new()
    {
        var fragment = AddComponent<T>();
        _buffFragment.Add(fragment);
        fragment.InitParam(arg1, arg2, arg3, arg4);
        if (Master != null)
        {
            fragment.OnPickUpItem();
        }
    }

    public override void Interactive(ActivityObject master)
    {
        if (master is Player role)
        {
            Pickup();
            role.PickUpBuffProp(this);
        }
    }

    public override CheckInteractiveResult CheckInteractive(ActivityObject master)
    {
        if (master is Player)
        {
            return new CheckInteractiveResult(this, true, CheckInteractiveResult.InteractiveType.PickUp);
        }
        return base.CheckInteractive(master);
    }
}