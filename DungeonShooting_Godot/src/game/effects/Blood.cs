using Godot;

/// <summary>
/// 血液溅射效果
/// </summary>
public partial class Blood : CpuParticles2D
{
	private float _timer;
	
	public override void _Ready()
	{
		Emitting = true;
		ReadyStop();
	}

	public override void _Process(double delta)
	{
		_timer += (float)delta;
		if (_timer > 15f)
		{
			if (_timer > 60f)
			{
				QueueFree();
			}
			else
			{
				var color = Modulate;
				color.A = Mathf.Lerp(1, 0, (_timer - 15f) / 45f);
				Modulate = color;
			}
		}
	}

	private async void ReadyStop()
	{
		var timer = GetTree().CreateTimer(Lifetime - 0.05f);
		await ToSignal(timer, "timeout");
		Emitting = false;
		SetPhysicsProcess(false);
		SetProcessInput(false);
		SetProcessInternal(false);
		SetProcessUnhandledInput(false);
		SetProcessUnhandledKeyInput(false);
	}
}