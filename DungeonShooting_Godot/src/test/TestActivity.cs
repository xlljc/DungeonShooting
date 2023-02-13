using Godot;

public partial class TestActivity : ActivityObject
{
	public TestActivity() : base(ResourcePath.prefab_test_TestActivity_tscn)
	{
		var externalForce = MoveController.AddConstantForce("move");
		externalForce.Velocity = new Vector2(0, 60);
	}
}