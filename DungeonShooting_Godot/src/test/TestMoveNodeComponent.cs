
using Godot;

public class TestMoveNodeComponent : NodeComponent<TestPlayer, KinematicBody2D>
{
    public TestMoveNodeComponent(KinematicBody2D inst) : base(inst)
    {
        
    }

    public override void Process(float delta)
    {
        
    }

    public override void PhysicsProcess(float delta)
    {
        var axis = Input.GetAxis("move_left", "move_right");
        if (axis != 0f)
        {
            Position += new Vector2(axis * 10 * delta, 0);
        }
    }

    public override void OnDestroy()
    {
        
    }
}