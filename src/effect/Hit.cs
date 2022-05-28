using Godot;

public class Hit : AnimatedSprite
{
    /// <summary>
    /// 动画结束, 销毁
    /// </summary>
	private void _on_Hit_animation_finished()
    {
        QueueFree();
    }
}