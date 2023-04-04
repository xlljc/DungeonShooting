using Godot;

[RegisterActivity(ActivityIdPrefix.Test + "0001", ResourcePath.prefab_test_TestActivity_tscn)]
public partial class TestActivity : ActivityObject
{
	public override void OnInit()
	{
		EnableVerticalMotion = false;
	}

	protected override void Process(float delta)
	{
		if (Input.IsActionJustPressed("fire"))
		{
			Altitude = 100;
		}

		if (Input.IsActionJustPressed("interactive"))
		{
			EnableVerticalMotion = !EnableVerticalMotion;
		}
	}

	protected override void OnFallToGround()
	{
		GD.Print("OnFallToGround");
	}

	protected override void OnFirstFallToGround()
	{
		GD.Print("OnFirstFallToGround");
	}

	protected override void OnThrowStart()
	{
		GD.Print("OnThrowStart");
	}

	protected override void OnThrowMaxHeight(float height)
	{
		GD.Print("OnThrowMaxHeight: " + height);
	}

	protected override void OnThrowOver()
	{
		GD.Print("OnThrowOver");
	}
}