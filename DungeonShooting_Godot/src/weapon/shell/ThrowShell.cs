using Godot;

/// <summary>
/// 弹壳
/// </summary>
public class ThrowShell : ThrowNode
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
        else
        {
            base.OnOver();
            //等待被销毁
            AwaitDestroy();
        }
    }

    private async void AwaitDestroy()
    {
        CollisionShape.Disabled = true;
        //60秒后销毁
        await ToSignal(GetTree().CreateTimer(60), "timeout");
        QueueFree();
    }

    protected override void OnMaxHeight(float height)
    {
        ZIndex = 0;
    }
}