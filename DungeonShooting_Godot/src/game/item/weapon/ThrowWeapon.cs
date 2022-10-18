
using Godot;

public class ThrowWeapon : ThrowComponent
{
    //是否第一次结束
    private bool fristOver = true;

    public override void StartThrow(Vector2 size, Vector2 start, float startHeight, float direction, float xSpeed, float ySpeed,
        float rotate)
    {
        KinematicBody.ZIndex = 2;
        base.StartThrow(size, start, startHeight, direction, xSpeed, ySpeed, rotate);
    }

    protected override void OnOver()
    {
        if (fristOver)
        {
            fristOver = false;
            if (ActivityObject is Weapon gun)
            {
                gun._FallToGround();
            }
        }
        //如果落地高度不够低, 再抛一次
        if (StartYSpeed > 1)
        {
            base.StartThrow(Size, GlobalPosition, 0, Direction, XSpeed * 0.8f, StartYSpeed * 0.5f, RotateSpeed * 0.5f);
            fristOver = true;
        }
        else //结束
        {
            base.OnOver();
        }
    }
    protected override void OnMaxHeight(float height)
    {
        KinematicBody.ZIndex = 0;
    }
}