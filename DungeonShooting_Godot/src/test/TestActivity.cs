using Godot;

[RegisterActivity(ActivityIdPrefix.Test + "1", ResourcePath.prefab_test_TestActivity_tscn)]
public partial class TestActivity : ActivityObject
{
	public override void OnInit()
	{
		var externalForce = MoveController.AddConstantForce("move");
		externalForce.Velocity = new Vector2(0, 60);
	}
}