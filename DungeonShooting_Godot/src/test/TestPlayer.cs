using Godot;

public class TestPlayer : Sprite
{
	public GameObject<TestPlayer> GameObject { get; private set; }

	public override void _Ready()
	{
		GameObject = new GameObject<TestPlayer>(this);
		var move = new TestMoveNodeComponent(ResourceManager.Load<PackedScene>("res://prefab/test/MoveComponent.tscn").Instance<KinematicBody2D>());
		GameObject.AddComponent(move);
		GameObject.AddComponent(new TestAttackComponent());
	}
}