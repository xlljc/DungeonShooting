
using Godot;

/// <summary>
/// 针对武器生成位置的标记
/// </summary>
[Tool]
public partial class WeaponMark : ActivityMark
{
    /// <summary>
    /// 当前弹夹弹药量, 如果值小于0, 则使用默认弹药量
    /// </summary>
    [Export]
    public int CurrAmmon = -1;

    /// <summary>
    /// 备用弹药容量, 如果值小于0, 则使用默认弹药量
    /// </summary>
    [Export]
    public int ResidueAmmo = -1;
    
    public override void _Ready()
    {
        DrawColor = Colors.Green;
        Type = ActivityIdPrefix.ActivityPrefixType.Weapon;
        Layer = RoomLayerEnum.NormalLayer;
    }

    public override void Doing(RoomInfo roomInfo)
    {
        //创建武器
        var instance = ActivityObject.Create<Weapon>(GetItemId());
        if (CurrAmmon >= 0)
        {
            instance.SetCurrAmmo(CurrAmmon);   
        }

        if (ResidueAmmo >= 0)
        {
            instance.SetResidueAmmo(ResidueAmmo);   
        }
        instance.PutDown(GlobalPosition, Layer);
        
        Visible = false;
    }
}