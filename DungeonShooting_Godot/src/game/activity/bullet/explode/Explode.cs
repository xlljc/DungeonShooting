
using Godot;

/// <summary>
/// 爆炸
/// </summary>
public partial class Explode : Area2D, IDestroy
{
    public bool IsDestroyed { get; private set; }
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
    }
    
    public override void _Ready()
    {
        GameCamera.Main.CreateShake(new Vector2(6, 6), 0.7f, true);
        GetNode<AnimationPlayer>("AnimationPlayer").Play(AnimatorNames.Play);
        
        this.CallDelayInNode(2, () =>
        {
            Destroy();
        });
    }
}