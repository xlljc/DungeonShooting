using Godot;

public class Hit : AnimatedSprite
{

    public override void _Ready()
    {
        Frame = 0;
        Playing = true;
    }

    /// <summary>
    /// 动画结束, 销毁
    /// </summary>
	private void _on_Hit_animation_finished()
    {
        QueueFree();
    }
}