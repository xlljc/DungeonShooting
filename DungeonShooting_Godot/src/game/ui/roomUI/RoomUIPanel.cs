using Godot;

namespace UI;

/// <summary>
/// 房间中的ui
/// </summary>
public partial class RoomUIPanel : RoomUI
{
    /// <summary>
    /// 当前血量
    /// </summary>
    public int Hp { get; private set; }
    /// <summary>
    /// 最大血量
    /// </summary>
    public int MaxHp { get; private set; }
    /// <summary>
    /// 当前护盾值
    /// </summary>
    public int Shield { get; private set; }
    /// <summary>
    /// 最大护盾值
    /// </summary>
    public int MaxShield { get; private set; }
    
    /// <summary>
    /// 互动提示组件
    /// </summary>
    public InteractiveTipBar InteractiveTipBar { get; private set; }
    /// <summary>
    /// 换弹进度组件
    /// </summary>
    public ReloadBar ReloadBar { get; private set; }
    
    public override void OnOpen(params object[] args)
    {
        
    }

    public override void OnClose()
    {
        
    }
    
    public override void _EnterTree()
    {
        InteractiveTipBar = GetNode<InteractiveTipBar>("ViewNode/InteractiveTipBar");
        InteractiveTipBar.Visible = false;

        ReloadBar = GetNode<ReloadBar>("ViewNode/ReloadBar");
        ReloadBar.Visible = false;
    }

    public override void _Ready()
    {
        //Generator.UiGenerator.GenerateUi(this, "src/game/ui/roomUI/RoomUI.cs");
        
        //将 GlobalNode 节点下的 ui 节点放入全局坐标中
        var globalNode = GetNode("GlobalNode");
        var root = GameApplication.Instance.GlobalNodeRoot;
        var count = globalNode.GetChildCount();
        for (int i = count - 1; i >= 0; i--)
        {
            var node = globalNode.GetChild(i);
            globalNode.RemoveChild(node);
            root.CallDeferred("add_child", node);
        }
        globalNode.CallDeferred("queue_free");
        
        //将 ViewNode 节点放到 SubViewport 下
        var viewNode = GetNode("ViewNode");
        var viewport = GameApplication.Instance.SubViewport;
        count = viewNode.GetChildCount();
        for (int i = count - 1; i >= 0; i--)
        {
            var node = viewNode.GetChild(i);
            viewNode.RemoveChild(node);
            viewport.CallDeferred("add_child", node);
        }
        viewNode.CallDeferred("queue_free");
    }

    /// <summary>
    /// 设置最大血量
    /// </summary>
    public void SetMaxHp(int maxHp)
    {
        MaxHp = Mathf.Max(maxHp, 0);
        L_Control.L_HealthBar.L_HpSlot.Instance.Size = new Vector2(maxHp + 3, L_Control.L_HealthBar.L_HpSlot.Instance.Size.Y);
        if (Hp > maxHp)
        {
            SetHp(maxHp);
        }
    }

    /// <summary>
    /// 设置最大护盾值
    /// </summary>
    public void SetMaxShield(int maxShield)
    {
        MaxShield = Mathf.Max(maxShield, 0);
        L_Control.L_HealthBar.L_ShieldSlot.Instance.Size = new Vector2(maxShield + 2, L_Control.L_HealthBar.L_ShieldSlot.Instance.Size.Y);
        if (Shield > MaxShield)
        {
            SetShield(maxShield);
        }
    }

    /// <summary>
    /// 设置当前血量
    /// </summary>
    public void SetHp(int hp)
    {
        Hp = Mathf.Clamp(hp, 0, MaxHp);
        L_Control.L_HealthBar.L_HpSlot.Instance.Size = new Vector2(hp, L_Control.L_HealthBar.L_HpSlot.Instance.Size.Y);
    }

    /// <summary>
    /// 设置护盾值
    /// </summary>
    public void SetShield(int shield)
    {
        Shield = Mathf.Clamp(shield, 0, MaxShield);
        L_Control.L_HealthBar.L_ShieldSlot.Instance.Size = new Vector2(shield, L_Control.L_HealthBar.L_ShieldSlot.Instance.Size.Y);
    }

    /// <summary>
    /// 玩家受到伤害
    /// </summary>
    public void Hit(int num)
    {

    }

    /// <summary>
    /// 设置显示在 ui 上的枪的纹理
    /// </summary>
    /// <param name="gun">纹理</param>
    public void SetGunTexture(Texture2D gun)
    {
        if (gun != null)
        {
            L_Control.L_GunBar.L_GunSprite.Instance.Texture = gun;
            L_Control.L_GunBar.L_GunSprite.Instance.Visible = true;
            L_Control.L_GunBar.L_BulletText.Instance.Visible = true;
        }
        else
        {
            L_Control.L_GunBar.L_GunSprite.Instance.Visible = false;
            L_Control.L_GunBar.L_BulletText.Instance.Visible = false;
        }
    }

    /// <summary>
    /// 设置弹药数据
    /// </summary>
    /// <param name="curr">当前弹夹弹药量</param>
    /// <param name="total">剩余弹药总数</param>
    public void SetAmmunition(int curr, int total)
    {
        L_Control.L_GunBar.L_BulletText.Instance.Text = curr + " / " + total;
    }
    
}