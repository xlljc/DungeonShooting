
using System;
using System.Collections.Generic;
using System.Text.Json;
using Config;
using Godot;

/// <summary>
/// 通用被动道具实体类
/// </summary>
[Tool]
public partial class BuffProp : PropActivity
{
    /// <summary>
    /// 配置数据
    /// </summary>
    public ExcelConfig.BuffPropBase Attribute { get; private set; }
    
    //被动属性
    private readonly List<BuffFragment> _buffFragment = new List<BuffFragment>();

    public override void OnInit()
    {
        base.OnInit();
        var buffAttribute = GetBuffAttribute(ActivityBase.Id);
        Attribute = buffAttribute;
        //初始化buff属性
        if (buffAttribute.Buff != null)
        {
            foreach (var keyValuePair in buffAttribute.Buff)
            {
                var buffInfo = PropFragmentRegister.BuffFragmentInfos[keyValuePair.Key];
                var item = keyValuePair.Value;
                var buff = (BuffFragment)AddComponent(buffInfo.Type);
                buff.InitParam(item);
                _buffFragment.Add(buff);
            }
        }

        //显示纹理
        if (!string.IsNullOrEmpty(ActivityBase.Icon))
        {
            SetDefaultTexture(ResourceManager.LoadTexture2D(ActivityBase.Icon));
        }
    }

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
    public void AddBuffFragment<T>(JsonElement[] arg) where T : BuffFragment, new()
    {
        var fragment = AddComponent<T>();
        _buffFragment.Add(fragment);
        fragment.InitParam(arg);
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
    
    
    private static bool _init = false;
    private static Dictionary<string, ExcelConfig.BuffPropBase> _buffAttributeMap =
        new Dictionary<string, ExcelConfig.BuffPropBase>();

    /// <summary>
    /// 初始化 buff 属性数据
    /// </summary>
    public static void InitBuffAttribute()
    {
        if (_init)
        {
            return;
        }

        _init = true;
        foreach (var buffAttr in ExcelConfig.BuffPropBase_List)
        {
            if (buffAttr.Activity != null)
            {
                if (!_buffAttributeMap.TryAdd(buffAttr.Activity.Id, buffAttr))
                {
                    Debug.LogError("发现重复注册的 buff 属性: " + buffAttr.Id);
                }
            }
        }
    }
    
    /// <summary>
    /// 根据 ActivityBase.Id 获取对应 buff 的属性数据
    /// </summary>
    public static ExcelConfig.BuffPropBase GetBuffAttribute(string itemId)
    {
        if (itemId == null)
        {
            return null;
        }
        if (_buffAttributeMap.TryGetValue(itemId, out var attr))
        {
            return attr;
        }

        throw new Exception($"buff'{itemId}'没有在 BuffPropBase 表中配置属性数据!");
    }
}