
using System;
using Godot;
using Vector2 = Godot.Vector2;

public partial class Role
{
    /// <summary>
    /// 播放近战攻击动画
    /// </summary>
    public virtual void PlayAnimation_MeleeAttack(Action finish)
    {
        var r = MountPoint.RotationDegrees;
        //var gp = MountPoint.GlobalPosition;
        var p1 = MountPoint.Position;
        var p2 = p1 + new Vector2(6, 0).Rotated(Mathf.DegToRad(r - 60));
        var p3 = p1 + new Vector2(6, 0).Rotated(Mathf.DegToRad(r + 60));
        
        var tween = CreateTween();
        tween.SetParallel();
        
        tween.TweenProperty(MountPoint, "rotation_degrees", r - 60, 0.12);
        tween.TweenProperty(MountPoint, "position", p2, 0.12);
        tween.TweenProperty(MountPoint, "position", p2, 0.12);
        tween.Chain();

        tween.TweenCallback(Callable.From(() =>
        {
            MountPoint.RotationDegrees = r + 60;
            MountPoint.Position = p3;
            //重新计算武器阴影位置
            var activeItem = WeaponPack.ActiveItem;
            activeItem.CalcShadowTransform();
            //创建屏幕抖动
            if (Face == FaceDirection.Right)
            {
                //GameCamera.Main.DirectionalShake(Vector2.FromAngle(Mathf.DegToRad(r - 90)) * 5);
                GameCamera.Main.DirectionalShake(Vector2.FromAngle(Mathf.DegToRad(r - 180)) * 6);
            }
            else
            {
                //GameCamera.Main.DirectionalShake(Vector2.FromAngle(Mathf.DegToRad(270 - r)) * 5);
                GameCamera.Main.DirectionalShake(Vector2.FromAngle(Mathf.DegToRad(-r)) * 6);
            }
            //播放特效
            var sprite = ResourceManager.LoadAndInstantiate<AutoDestroySprite>(ResourcePath.prefab_effect_weapon_MeleeAttack1_tscn);
            var localFirePosition = activeItem.GetLocalFirePosition();
            localFirePosition.X *= 0.85f;
            sprite.Position = p1 + localFirePosition.Rotated(Mathf.DegToRad(r));
            sprite.RotationDegrees = r;
            AddChild(sprite);
        }));
        tween.Chain();
        
        tween.TweenProperty(MountPoint, "rotation_degrees", r, 0.2);
        tween.TweenProperty(MountPoint, "position", p1, 0.2);
        tween.Chain();
        
        tween.TweenCallback(Callable.From(() =>
        {
            finish();
        }));
        tween.Play();
    }
}