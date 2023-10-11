using Godot;

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
		Debug.Log("OnFallToGround");
	}

	protected override void OnFirstFallToGround()
	{
		Debug.Log("OnFirstFallToGround");
	}

	protected override void OnThrowStart()
	{
		Debug.Log("OnThrowStart");
	}

	protected override void OnThrowMaxHeight(float height)
	{
		Debug.Log("OnThrowMaxHeight: " + height);
	}

	protected override void OnThrowOver()
	{
		Debug.Log("OnThrowOver");
	}
}