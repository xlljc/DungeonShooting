
using Godot;

/// <summary>
/// 针对敌人生成位置的标记
/// </summary>
[Tool]
public partial class EnemyMark : ActivityMark
{
    /// <summary>
    /// 脸默认朝向
    /// </summary>
    public enum FaceDirectionValueEnum
    {
        /// <summary>
        /// 随机
        /// </summary>
        Random,
        /// <summary>
        /// 左边
        /// </summary>
        Left,
        /// <summary>
        /// 右边
        /// </summary>
        Right
    }
    
    /// <summary>
    /// 武器1 id, id会自动加上武器前缀
    /// </summary>
    [Export(PropertyHint.Expression), ActivityExpression]
    public string Weapon1Id;
    /// <summary>
    /// 武器2 id, id会自动加上武器前缀
    /// </summary>
    [Export(PropertyHint.Expression), ActivityExpression]
    public string Weapon2Id;
    /// <summary>
    /// 武器3 id, id会自动加上武器前缀
    /// </summary>
    [Export(PropertyHint.Expression), ActivityExpression]
    public string Weapon3Id;
    /// <summary>
    /// 武器4 id, id会自动加上武器前缀
    /// </summary>
    [Export(PropertyHint.Expression), ActivityExpression]
    public string Weapon4Id;
    /// <summary>
    /// 脸默认的朝向
    /// </summary>
    [Export]
    public FaceDirectionValueEnum FaceDirection = FaceDirectionValueEnum.Random;

    public override void _Ready()
    {
        Type = ActivityIdPrefix.ActivityPrefixType.Enemy;
        Layer = RoomLayerEnum.YSortLayer;
    }

    public override void Doing(ActivityObject activityObject, ActivityExpressionData expressionData, RoomInfo roomInfo)
    {
        //创建敌人
        var instance = (Enemy)activityObject;
        var pos = instance.Position;
        
        //脸的朝向
        if (FaceDirection == FaceDirectionValueEnum.Random)
        {
            instance.Face = Utils.RandomBoolean() ? global::FaceDirection.Left : global::FaceDirection.Right;
        }
        else if (FaceDirection == FaceDirectionValueEnum.Left)
        {
            instance.Face = global::FaceDirection.Left;
        }
        else
        {
            instance.Face = global::FaceDirection.Right;
        }

        if (!string.IsNullOrWhiteSpace(Weapon1Id))
            CreateWeapon(instance, pos, nameof(Weapon1Id), -1);
        if (!string.IsNullOrWhiteSpace(Weapon2Id))
            CreateWeapon(instance, pos, nameof(Weapon2Id), -1);
        if (!string.IsNullOrWhiteSpace(Weapon3Id))
            CreateWeapon(instance, pos, nameof(Weapon3Id), -1);
        if (!string.IsNullOrWhiteSpace(Weapon4Id))
            CreateWeapon(instance, pos, nameof(Weapon4Id), -1);
        
        var effect1 = ResourceManager.LoadAndInstantiate<Effect1>(ResourcePath.prefab_effect_Effect1_tscn);
        effect1.Position = instance.Position;
        effect1.AddToActivityRoot(RoomLayerEnum.NormalLayer);
    }

    //生成武器
    private void CreateWeapon(Enemy instance, Vector2 pos, string fieldName, int ammon)
    {
        var weapon = (Weapon)CreateActivityObjectFromExpression(ActivityIdPrefix.ActivityPrefixType.Weapon, fieldName);
        //设置弹药量
        if (ammon >= 0)
        {
            weapon.SetTotalAmmo(ammon);
        }
        //如果不能放下， 则直接扔地上
        if (!instance.PickUpWeapon(weapon))
        {
            weapon.PutDown(pos, RoomLayerEnum.NormalLayer);
        }
    }
}