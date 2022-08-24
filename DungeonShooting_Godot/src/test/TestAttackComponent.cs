
using Godot;

public class TestAttackComponent : Component<TestPlayer>
{
    public override void Process(float delta)
    {
        if (Input.IsActionPressed("fire"))
        {
            GD.Print("点击了开火");
        }
    }
}