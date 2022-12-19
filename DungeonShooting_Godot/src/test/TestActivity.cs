using Godot;

public class TestActivity : ActivityObject
{
	public TestActivity() : base(ResourcePath.prefab_test_TestActivity_tscn)
	{
		var externalForce = MoveController.AddForce("move");
		externalForce.Velocity = new Vector2(0, 60);
	}
}