using Godot;

/// <summary>
/// 弹壳
/// </summary>
public class Shell : ThrowNode
{
    protected override void OnInit()
    {

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
            //等待被销毁
            AwaitDestroy();
        }
    }

    private async void AwaitDestroy()
    {
        CollisionShape.Disabled = true;
        //20秒后销毁
        await ToSignal(GetTree().CreateTimer(20), "timeout");
        QueueFree();
    }
}