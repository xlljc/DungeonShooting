using Godot;

public class ThrowGun : ThrowNode
{

    public override void _Ready()
    {
        base._Ready();
        ZIndex = 2;
    }

    protected override void OnOver()
    {
        //如果落地高度不够低, 再抛一次
        if (StartYSpeed > 1)
        {
            InitThrow(Size, GlobalPosition, 0, Direction, XSpeed * 0.8f, StartYSpeed * 0.5f, RotateSpeed * 0.5f, null);
        }
    }

    protected override void OnMaxHeight(float height)
    {
        ZIndex = 0;
    }
}