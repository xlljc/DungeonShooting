using Godot;

public class ThrowWeapon : ThrowNode
{

    private bool fristOver = true;

    public override void _Ready()
    {
        base._Ready();
        ZIndex = 2;
    }

    public override void StartThrow(Vector2 size, Vector2 start, float startHeight, float direction, float xSpeed, float ySpeed, float rotate, Node2D mount)
    {
        base.StartThrow(size, start, startHeight, direction, xSpeed, ySpeed, rotate, mount);
        fristOver = true;
    }

    protected override void OnOver()
    {
        if (fristOver)
        {
            fristOver = false;
            if (Mount is Weapon gun)
            {
                gun._FallToGround();
            }
        }
        //如果落地高度不够低, 再抛一次
        if (StartYSpeed > 1)
        {
            StartThrow(Size, GlobalPosition, 0, Direction, XSpeed * 0.8f, StartYSpeed * 0.5f, RotateSpeed * 0.5f, null);
        }
        else //结束
        {
            base.OnOver();
        }
    }
    protected override void OnMaxHeight(float height)
    {
        ZIndex = 0;
    }
}