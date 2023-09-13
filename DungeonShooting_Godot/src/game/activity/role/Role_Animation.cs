
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
        MountLookTarget = false;
        var r = MountPoint.RotationDegrees;
        var p1 = MountPoint.Position;
        var p2 = p1 + new Vector2(6, 0).Rotated(Mathf.DegToRad(r - 60));
        var p3 = p1 + new Vector2(6, 0).Rotated(Mathf.DegToRad(r + 60));
        
        var tween = CreateTween();
        tween.SetParallel();
        
        tween.TweenProperty(MountPoint, "rotation_degrees", r - 60, 0.15);
        tween.TweenProperty(MountPoint, "position", p2, 0.15);
        tween.Chain();
        
        tween.TweenCallback(Callable.From(() =>
        {
            MountPoint.RotationDegrees = r + 60;
            MountPoint.Position = p3;
            //创建屏幕抖动
            if (Face == FaceDirection.Right)
            {
                GameCamera.Main.DirectionalShake(Vector2.FromAngle(Mathf.DegToRad(r - 90)) * 5);
            }
            else
            {
                GameCamera.Main.DirectionalShake(Vector2.FromAngle(Mathf.DegToRad(270 - r)) * 5);
            }
        }));
        tween.Chain();
        
        tween.TweenProperty(MountPoint, "rotation_degrees", r, 0.3);
        tween.TweenProperty(MountPoint, "position", p1, 0.3);
        tween.Chain();
        
        tween.TweenCallback(Callable.From(() =>
        {
            MountLookTarget = true;
            finish();
        }));
        tween.Play();
    }
}