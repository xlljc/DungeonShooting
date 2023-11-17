
using System;
using Godot;
using Vector2 = Godot.Vector2;

public static class RoleAnimation
{
    /// <summary>
    /// 播放近战攻击动画
    /// </summary>
    public static void PlayAnimation_MeleeAttack(this AdvancedRole role, Action finish)
    {
        var r = role.MountPoint.RotationDegrees;
        //var gp = MountPoint.GlobalPosition;
        var p1 = role.MountPoint.Position;
        var p2 = p1 + new Vector2(6, 0).Rotated(Mathf.DegToRad(r - role.MeleeAttackAngle / 2f));
        var p3 = p1 + new Vector2(6, 0).Rotated(Mathf.DegToRad(r + role.MeleeAttackAngle / 2f));
        
        var tween = role.CreateTween();
        tween.SetParallel();
        
        tween.TweenProperty(role.MountPoint, "rotation_degrees", r - role.MeleeAttackAngle / 2f, 0.1);
        tween.TweenProperty(role.MountPoint, "position", p2, 0.1);
        tween.TweenProperty(role.MountPoint, "position", p2, 0.1);
        tween.Chain();

        tween.TweenCallback(Callable.From(() =>
        {
            role.MountPoint.RotationDegrees = r + role.MeleeAttackAngle / 2f;
            role.MountPoint.Position = p3;
            //重新计算武器阴影位置
            var activeItem = role.WeaponPack.ActiveItem;
            activeItem.CalcShadowTransform();
            //创建屏幕抖动
            if (role.Face == FaceDirection.Right)
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
            var effect = ObjectManager.GetPoolItem<IEffect>(ResourcePath.prefab_effect_weapon_MeleeAttack1_tscn);
            var sprite = (Node2D)effect;
            var localFirePosition = activeItem.GetLocalFirePosition() - activeItem.GripPoint.Position;
            localFirePosition *= 0.9f;
            sprite.Position = p1 + localFirePosition.Rotated(Mathf.DegToRad(r));
            sprite.RotationDegrees = r;
            role.AddChild(sprite);
            effect.PlayEffect();

            //启用近战碰撞区域
            role.MeleeAttackCollision.Disabled = false;
        }));
        tween.Chain();
        
        tween.TweenInterval(0.1f);
        tween.Chain();

        tween.TweenCallback(Callable.From(() =>
        {
            //关闭近战碰撞区域
            role.MeleeAttackCollision.Disabled = true;
        }));
        tween.TweenProperty(role.MountPoint, "rotation_degrees", r, 0.2);
        tween.TweenProperty(role.MountPoint, "position", p1, 0.2);
        tween.Chain();
        
        tween.TweenCallback(Callable.From(() =>
        {
            finish();
        }));
        tween.Play();
    }
    
    /// <summary>
    /// 播放近战攻击动画
    /// </summary>
    public static void PlayAnimation_MeleeAttack(this AdvancedEnemy role, Action finish)
    {
        var r = role.MountPoint.RotationDegrees;
        //var gp = MountPoint.GlobalPosition;
        var p1 = role.MountPoint.Position;
        var p2 = p1 + new Vector2(6, 0).Rotated(Mathf.DegToRad(r - role.MeleeAttackAngle / 2f));
        var p3 = p1 + new Vector2(6, 0).Rotated(Mathf.DegToRad(r + role.MeleeAttackAngle / 2f));
        
        var tween = role.CreateTween();
        tween.SetParallel();
        
        tween.TweenProperty(role.MountPoint, "rotation_degrees", r - role.MeleeAttackAngle / 2f, 0.1);
        tween.TweenProperty(role.MountPoint, "position", p2, 0.1);
        tween.TweenProperty(role.MountPoint, "position", p2, 0.1);
        tween.Chain();

        tween.TweenCallback(Callable.From(() =>
        {
            role.MountPoint.RotationDegrees = r + role.MeleeAttackAngle / 2f;
            role.MountPoint.Position = p3;
            //重新计算武器阴影位置
            var activeItem = role.WeaponPack.ActiveItem;
            activeItem.CalcShadowTransform();
            //创建屏幕抖动
            if (role.Face == FaceDirection.Right)
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
            var effect = ObjectManager.GetPoolItem<IEffect>(ResourcePath.prefab_effect_weapon_MeleeAttack1_tscn);
            var sprite = (Node2D)effect;
            var localFirePosition = activeItem.GetLocalFirePosition() - activeItem.GripPoint.Position;
            localFirePosition *= 0.9f;
            sprite.Position = p1 + localFirePosition.Rotated(Mathf.DegToRad(r));
            sprite.RotationDegrees = r;
            role.AddChild(sprite);
            effect.PlayEffect();

            //启用近战碰撞区域
            role.MeleeAttackCollision.Disabled = false;
        }));
        tween.Chain();
        
        tween.TweenInterval(0.1f);
        tween.Chain();

        tween.TweenCallback(Callable.From(() =>
        {
            //关闭近战碰撞区域
            role.MeleeAttackCollision.Disabled = true;
        }));
        tween.TweenProperty(role.MountPoint, "rotation_degrees", r, 0.2);
        tween.TweenProperty(role.MountPoint, "position", p1, 0.2);
        tween.Chain();
        
        tween.TweenCallback(Callable.From(() =>
        {
            finish();
        }));
        tween.Play();
    }
}